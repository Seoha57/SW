using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actions : MonoBehaviour
{
    ActionHelper actionHelper;

    public int comboCount = 3;
    private static int tempComboCount = 0;
    private static float GCD = 4.0f;
    private static float GCDTimer = 0;
    private static float[,] OGCD;
    private static float[] OGCDTimer;

    public static float EnemyAttackCooltime = 3.0f;
    private static float EnemyAttackTimer = 0;

    private static float damage = 0;
    private static float recoveryValue = 0;

    bool playerAlive = true;
    bool enemyAlive = true;

    static bool isFirst = true;

    public GameObject[] player;
    public GameObject[] enemy;

    private void OnEnable()
    {
        actionHelper = GetComponent<ActionHelper>();

        EnemyAttackTimer = EnemyAttackCooltime;
        if (isFirst)
        {
            OGCDTimer = new float[2];
            OGCD = new float[3, 4];
            OGCD[0, 0] = 3.0f;
            OGCD[0, 1] = 5.0f;
            isFirst = false;
        }

        AddActionsToDictionary();
    }

    private void OnDisable()
    {
        tempComboCount = 0;
        EnemyAttackTimer = EnemyAttackCooltime;
        GCDTimer = GCD;
        for (int i = 0; i < OGCDTimer.Length; ++i)
        {
            OGCDTimer[i] = OGCD[0, i];
        }
    }

    void AddActionsToDictionary()
    {
        ActionEvent check = null;
        CombatSysMgr.actionEventDic.TryGetValue("TakePhysicalDamage", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("TakePhysicalDamage", new ActionEvent());

        CombatSysMgr.actionEventDic.TryGetValue("OGCD1_Init", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("OGCD1_Init", new ActionEvent());

        CombatSysMgr.actionEventDic.TryGetValue("OGCD2_Init", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("OGCD2_Init", new ActionEvent());

        CombatSysMgr.actionEventDic.TryGetValue("GCDInitialize", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("GCDInitialize", new ActionEvent());

        CombatSysMgr.actionEventDic.TryGetValue("SelfHeal", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("SelfHeal", new ActionEvent());

        CombatSysMgr.actionEventDic.TryGetValue("PlayerIsAlive", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("PlayerIsAlive", new ActionEvent());

        CombatSysMgr.actionEventDic.TryGetValue("PlayerIsDead", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("PlayerIsDead", new ActionEvent());

        CombatSysMgr.actionEventDic.TryGetValue("EnemyIsAlive", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("EnemyIsAlive", new ActionEvent());

        CombatSysMgr.actionEventDic.TryGetValue("EnemyIsDead", out check);
        if (check == null)
            CombatSysMgr.actionEventDic.Add("EnemyIsDead", new ActionEvent());


        Action holder = null;
        CombatSysMgr.actionDic.TryGetValue("DaggerStrike", out holder);
        if (holder == null)
        {
            holder = new Action();
            holder.AEfList = new List<ActionEffect>();
            holder.AEfList.Add(DealPhysicalDamage);
            holder.AEfList.Add(TargetHPCheck);
            CombatSysMgr.actionDic.Add("DaggerStrike", holder);
        }

        Action autoAttack = null;
        CombatSysMgr.actionDic.TryGetValue("AutoAttack", out autoAttack);
        if (autoAttack == null)
        {
            autoAttack = new Action();
            autoAttack.AEfList = new List<ActionEffect>();
            autoAttack.AEfList.Add(DealPhysicalComboDamage);
            autoAttack.AEfList.Add(TargetHPCheck);
            CombatSysMgr.actionDic.Add("AutoAttack", autoAttack);
        }

        Action recovery = null;
        CombatSysMgr.actionDic.TryGetValue("SelfRecovery", out recovery);
        if (recovery == null)
        {
            recovery = new Action();
            recovery.AEfList = new List<ActionEffect>();
            recovery.AEfList.Add(SelfHeal);
            recovery.AEfList.Add(TargetHPCheck);
            CombatSysMgr.actionDic.Add("SelfRecovery", recovery);
        }
    }

    private void Start()
    {
        GCD = actionHelper.GCDMod(player[0].GetComponent<Entity>(), GCD);

        for (int i = 0; i < OGCDTimer.Length; ++i)
        {
            OGCDTimer[i] = OGCD[0, i];
        }
        GCDTimer = GCD;
    }

    private void Update()
    {
        if (OGCDTimer[0] > 0)
            OGCDTimer[0] -= Time.deltaTime;
        if (OGCDTimer[1] > 0)
            OGCDTimer[1] -= Time.deltaTime;

        GCDTimer -= Time.deltaTime;
        EnemyAttackTimer -= Time.deltaTime;

        if (GCDTimer < 0 && (enemyAlive && playerAlive))
        {
            CombatSysMgr.TriggerAction("AutoAttack", player[0]);
            Debug.Log(GCD);
        }

        if (EnemyAttackTimer < 0 && (enemyAlive && playerAlive))
        {
            CombatSysMgr.TriggerAction("DaggerStrike", enemy[0]);
            EnemyAttackTimer = EnemyAttackCooltime;
        }
    }

    public static float GetPhysicalDamage()
    {
        return damage;
    }

    public static float GetRecoveryValue()
    {
        return recoveryValue;
    }

    public static float GetOGCD1Duration(int userNum, int skillNum)
    {
        return OGCD[userNum, skillNum];
    }

    public static float GetOGCD1Timer(int num)
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

    void DealPhysicalDamage(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);
        if (e.ID == 0)
        {
            if (OGCDTimer[0] <= 0 && (playerAlive && enemyAlive))
            {
                CombatSysMgr.TriggerActionEvent("TakePhysicalDamage", e);
                CombatSysMgr.TriggerActionEvent("OGCD1_Init", e);
                OGCDTimer[0] = OGCD[0, 0];
            }
        }
        else if (e.ID == 10)
        {
            if (EnemyAttackTimer <= 0 && (playerAlive && enemyAlive))
            {
                CombatSysMgr.TriggerActionEvent("TakePhysicalDamage", e);
                EnemyAttackTimer = EnemyAttackCooltime;
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
            CombatSysMgr.TriggerActionEvent("TakePhysicalDamage", e);
            CombatSysMgr.TriggerActionEvent("GCDInitialize", e);
            GCDTimer = GCD;
        }
    }

    void SelfHeal(Entity e)
    {
        recoveryValue = 5.0f;
        if (OGCDTimer[1] <= 0 && playerAlive && enemyAlive)
        {
            CombatSysMgr.TriggerActionEvent("SelfHeal", e);
            CombatSysMgr.TriggerActionEvent("OGCD2_Init", e);
            OGCDTimer[1] = OGCD[0, 1];
        }
    }

    void TargetHPCheck(Entity e)
    {
        if (e.ID == 0 && !e.target.activeSelf)
        {
            enemyAlive = false;
            CombatSysMgr.TriggerActionEvent("EnemyIsDead", e);
        }
        if (e.ID == 0 && e.gameObject.activeSelf)
        {
            playerAlive = true;
            CombatSysMgr.TriggerActionEvent("PlayerIsAlive", e);
        }
        if (e.ID == 10 && !e.target.activeSelf)
        {
            playerAlive = false;
            CombatSysMgr.TriggerActionEvent("PlayerIsDead", e);
        }
        if (e.ID == 10 && e.gameObject.activeSelf)
        {
            enemyAlive = true;
            CombatSysMgr.TriggerActionEvent("EnemyIsAlive", e);
        }
    }
}