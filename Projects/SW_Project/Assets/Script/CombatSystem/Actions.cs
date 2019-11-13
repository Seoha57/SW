using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actions : MonoBehaviour
{
    ActionHelper actionHelper;
    CombatSysMgr instance;

    public int comboCount = 3;
    private static int tempComboCount = 0;
    private static float GCD = 4.0f;
    private static float GCDTimer = 0;
    private static float[] OGCD;
    private static float[] OGCDTimer;

    public static float[] EnemyAttackCooltime;
    private static float[] EnemyAttackTimer;

    private static float damage = 0;
    private static float recoveryValue = 0;

    bool playerAlive = true;
    bool enemyAlive = true;

    static bool isFirst = true;
    static int enemyDieCount;

    public GameObject[] player;
    public GameObject[] enemy;

    private void OnEnable()
    {
        actionHelper = GetComponent<ActionHelper>();
        instance = CombatSysMgr.instance;

        playerAlive = true;
        enemyAlive = true;

        if (isFirst)
        {
            EnemyAttackCooltime = new float[enemy.Length];
            EnemyAttackTimer = new float[enemy.Length];

            OGCDTimer = new float[2];
            OGCD = new float[4];
            OGCD[0] = 3.0f;
            OGCD[1] = 5.0f;
            isFirst = false;

            for(int i = 0; i < enemy.Length; ++i)
            {
                EnemyAttackCooltime[i] = 5.0f + Random.Range(-2, 2);
            }
        }
        enemyDieCount = 0;

        AddActionsToDictionary();
    }

    private void OnDisable()
    {
        tempComboCount = 0;
        GCDTimer = GCD;
        for (int i = 0; i < OGCDTimer.Length; ++i)
        {
            OGCDTimer[i] = OGCD[i];
        }

        for (int i = 0; i < enemy.Length; ++i)
        {
            EnemyAttackTimer[i] = EnemyAttackCooltime[i];
        }
        enemyDieCount = 0;
    }

    void AddActionsToDictionary()
    {
        /*Action Event list*/
        ActionEvent check = null;
        instance.actionEventDic.TryGetValue("TakePhysicalDamage", out check);
        if (check == null)
            instance.actionEventDic.Add("TakePhysicalDamage", new ActionEvent());

        instance.actionEventDic.TryGetValue("OGCD1_Init", out check);
        if (check == null)
            CombatSysMgr.instance.actionEventDic.Add("OGCD1_Init", new ActionEvent());

        instance.actionEventDic.TryGetValue("OGCD2_Init", out check);
        if (check == null)
            instance.actionEventDic.Add("OGCD2_Init", new ActionEvent());

        instance.actionEventDic.TryGetValue("GCDInitialize", out check);
        if (check == null)
            instance.actionEventDic.Add("GCDInitialize", new ActionEvent());

        instance.actionEventDic.TryGetValue("SelfHeal", out check);
        if (check == null)
            instance.actionEventDic.Add("SelfHeal", new ActionEvent());

        instance.actionEventDic.TryGetValue("PlayerIsAlive", out check);
        if (check == null)
            instance.actionEventDic.Add("PlayerIsAlive", new ActionEvent());

        instance.actionEventDic.TryGetValue("PlayerIsDead", out check);
        if (check == null)
            instance.actionEventDic.Add("PlayerIsDead", new ActionEvent());

        instance.actionEventDic.TryGetValue("EnemyIsAlive", out check);
        if (check == null)
            instance.actionEventDic.Add("EnemyIsAlive", new ActionEvent());

        instance.actionEventDic.TryGetValue("EnemyIsDead", out check);
        if (check == null)
            instance.actionEventDic.Add("EnemyIsDead", new ActionEvent());

        instance.actionEventDic.TryGetValue("TargetIsChanged", out check);
        if (check == null)
            instance.actionEventDic.Add("TargetIsChanged", new ActionEvent());

        instance.actionEventDic.TryGetValue("AllEnemyIsDead", out check);
        if (check == null)
            instance.actionEventDic.Add("AllEnemyIsDead", new ActionEvent());

        /*Action list*/
        Action holder = null;
        instance.actionDic.TryGetValue("DaggerStrike", out holder);
        if (holder == null)
        {
            holder = new Action();
            holder.AEfList = new List<ActionEffect>();
            holder.AEfList.Add(DealPhysicalDamage);
            holder.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("DaggerStrike", holder);
        }

        Action autoAttack = null;
        instance.actionDic.TryGetValue("AutoAttack", out autoAttack);
        if (autoAttack == null)
        {
            autoAttack = new Action();
            autoAttack.AEfList = new List<ActionEffect>();
            autoAttack.AEfList.Add(DealPhysicalComboDamage);
            autoAttack.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("AutoAttack", autoAttack);
        }

        Action recovery = null;
        instance.actionDic.TryGetValue("SelfRecovery", out recovery);
        if (recovery == null)
        {
            recovery = new Action();
            recovery.AEfList = new List<ActionEffect>();
            recovery.AEfList.Add(SelfHeal);
            recovery.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("SelfRecovery", recovery);
        }

        /*Listen*/
        ActionEvent hit = new ActionEvent();
        if (instance.actionEventDic.TryGetValue("EnemyIsDead", out hit))
        {
            hit.AddListener(EnemyCheck);
        }
        ActionEvent findNewTarget = new ActionEvent();
        if (instance.actionEventDic.TryGetValue("EnemyIsDead", out hit))
        {
            hit.AddListener(ChangeTarget);
        }
        ActionEvent targetChange = new ActionEvent();
        if (instance.actionEventDic.TryGetValue("TargetIsChanged", out hit))
        {
            hit.AddListener(ShowTarget);
        }
    }

    private void Start()
    {
        GCD = actionHelper.GCDMod(player[0].GetComponent<Entity>(), GCD);

        for (int i = 0; i < OGCDTimer.Length; ++i)
        {
            OGCDTimer[i] = OGCD[i];
        }
        GCDTimer = GCD;

        for (int i = 0; i < enemy.Length; ++i)
        {
            EnemyAttackTimer[i] = EnemyAttackCooltime[i];
        }

        player[0].GetComponent<Entity>().target.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (OGCDTimer[0] > 0)
            OGCDTimer[0] -= Time.deltaTime;
        if (OGCDTimer[1] > 0)
            OGCDTimer[1] -= Time.deltaTime;

        GCDTimer -= Time.deltaTime;
        for (int i = 0; i < enemy.Length; ++i)
        {
            EnemyAttackTimer[i] -= Time.deltaTime;

            if (EnemyAttackTimer[i] < 0 && (enemyAlive && playerAlive))
            {
                instance.TriggerAction("DaggerStrike", enemy[i]);
                EnemyAttackTimer[i] = EnemyAttackCooltime[i];
            }
        }

        if (GCDTimer < 0 && (enemyAlive && playerAlive))
        {
            instance.TriggerAction("AutoAttack", player[0]);
        }
    }

    void DealPhysicalDamage(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);
        if (e.ID == 0)
        {
            if (OGCDTimer[0] <= 0 && (playerAlive && enemyAlive))
            {
                instance.TriggerActionEvent("TakePhysicalDamage", e);
                instance.TriggerActionEvent("OGCD1_Init", e);
                OGCDTimer[0] = OGCD[0];
            }
        }
        else if (e.ID == 10)
        {
            if (EnemyAttackTimer[0] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                instance.TriggerActionEvent("TakePhysicalDamage", e);
            }
        }
        else if (e.ID == 11)
        {
            if (EnemyAttackTimer[1] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                instance.TriggerActionEvent("TakePhysicalDamage", e);
            }
        }
        else if (e.ID == 12)
        {
            if (EnemyAttackTimer[2] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                instance.TriggerActionEvent("TakePhysicalDamage", e);
            }
        }
    }

    void DealPhysicalComboDamage(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);

        if (GCDTimer <= 0 && (playerAlive && enemyAlive))
        {
            if (tempComboCount < comboCount)
            {
                damage = damage + (damage * (0.2f * tempComboCount));
                ++tempComboCount;
            }
            else
            {
                tempComboCount = 0;
            }
            instance.TriggerActionEvent("TakePhysicalDamage", e);
            instance.TriggerActionEvent("GCDInitialize", e);
            GCDTimer = GCD;
        }
    }

    void SelfHeal(Entity e)
    {
        recoveryValue = 5.0f;
        if (OGCDTimer[1] <= 0 && playerAlive && enemyAlive)
        {
            instance.TriggerActionEvent("SelfHeal", e);
            instance.TriggerActionEvent("OGCD2_Init", e);
            OGCDTimer[1] = OGCD[1];
        }
    }

    void TargetHPCheck(Entity e)
    {
        //if (e.ID == 0 && !e.target.activeSelf)
        //{
        //    enemyAlive = false;
        //    instance.TriggerActionEvent("EnemyIsDead", e);
        //}
        //if (e.ID == 0 && e.gameObject.activeSelf)
        //{
        //    playerAlive = true;
        //    instance.TriggerActionEvent("PlayerIsAlive", e);
        //}
        //if (e.ID == 10 && !e.target.activeSelf)
        //{
        //    playerAlive = false;
        //    instance.TriggerActionEvent("PlayerIsDead", e);
        //}
        //if (e.ID == 10 && e.gameObject.activeSelf)
        //{
        //    enemyAlive = true;
        //    instance.TriggerActionEvent("EnemyIsAlive", e);
        //}
        if(e.target.GetComponent<Entity>().ID == 0 && !e.target.activeSelf)
        {
            playerAlive = false;
            instance.TriggerActionEvent("PlayerIsDead", e);
        }
        else if(e.target.GetComponent<Entity>().ID >= 10 && !e.target.activeSelf)
        {
            instance.TriggerActionEvent("EnemyIsDead", e);
        }
    }

    void EnemyCheck(Entity e)
    {
        ++enemyDieCount;
        if(enemyDieCount == enemy.Length)
        {
            enemyAlive = false;
            instance.TriggerActionEvent("AllEnemyIsDead", e);
        }
    }

    void ChangeTarget(Entity e)
    {
        if(enemyDieCount != enemy.Length)
        {
            for(int i = 0; i < enemy.Length; ++i)
            {
                if (enemy[i].activeSelf)
                {
                    e.target = enemy[i];
                    instance.TriggerActionEvent("TargetIsChanged", e);
                    break;
                }
            }
        }
    }

    void ShowTarget(Entity e)
    {
        e.target.transform.GetChild(0).gameObject.SetActive(true);
    }

    public static float GetPhysicalDamage()
    {
        return damage;
    }

    public static float GetRecoveryValue()
    {
        return recoveryValue;
    }

    public static float GetOGCDDuration(int skillNum)
    {
        return OGCD[skillNum];
    }

    public static float GetOGCDTimer(int num)
    {
        return OGCDTimer[num];
    }

    public static float GetGCDDuration()
    {
        return GCD;
    }

    public static float GetGCDTimer()
    {
        return GCDTimer;
    }
}