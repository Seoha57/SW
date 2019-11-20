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
    private static float GCD = 2.0f;
    private static float GCDTimer = 0;
    private static float[] OGCD;
    private static float[] OGCDTimer;

    public static float[] EnemyAttackCooltime;
    private static float[] EnemyAttackTimer;

    private static float damage = 0;
    private static float recoveryValue = 0;

    bool playerAlive = true;
    bool enemyAlive = true;

    static int enemyDieCount;
    bool comboAttacking = false;

    public GameObject[] player;
    public GameObject[] enemy;

    private void OnEnable()
    {
        actionHelper = GetComponent<ActionHelper>();
        instance = CombatSysMgr.instance;

        playerAlive = true;
        enemyAlive = true;

        EnemyAttackCooltime = new float[enemy.Length];
        EnemyAttackTimer = new float[enemy.Length];

        for (int i = 0; i < enemy.Length; ++i)
        {
            EnemyAttackCooltime[i] = 5.0f + Random.Range(-2, 2);
        }

        /*Temp CoolTime*/
        OGCDTimer = new float[3];
        OGCD = new float[3];
        OGCD[0] = 3.0f;
        OGCD[1] = 6.0f;
        OGCD[2] = 5.0f;

        actionHelper.CoolTimeMod(player[0].GetComponent<Entity>(), OGCD[0]);
        actionHelper.CoolTimeMod(player[0].GetComponent<Entity>(), OGCD[2]);
        actionHelper.CoolTimeMod(player[0].GetComponent<Entity>(), GCD);
        if (player[0].GetComponent<Entity>().ID == 0)
            OGCD[1] = GCD * 3;
        else
            actionHelper.CoolTimeMod(player[1].GetComponent<Entity>(), OGCD[1]);

        enemyDieCount = 0;

        AddActionsToDictionary();

        Time.timeScale = 1.0f;
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
        instance.actionEventDic.TryGetValue("TakeDamage", out check);
        if (check == null)
            instance.actionEventDic.Add("TakeDamage", new ActionEvent());

        instance.actionEventDic.TryGetValue("UseSkill", out check);
        if (check == null)
            instance.actionEventDic.Add("UseSkill", new ActionEvent());

        instance.actionEventDic.TryGetValue("ComboAttackStart", out check);
        if (check == null)
            instance.actionEventDic.Add("ComboAttackStart", new ActionEvent());

        instance.actionEventDic.TryGetValue("ComboAttackFinish", out check);
        if (check == null)
            instance.actionEventDic.Add("ComboAttackFinish", new ActionEvent());

        instance.actionEventDic.TryGetValue("OGCD1_Init", out check);
        if (check == null)
            CombatSysMgr.instance.actionEventDic.Add("OGCD1_Init", new ActionEvent());

        instance.actionEventDic.TryGetValue("OGCD2_Init", out check);
        if (check == null)
            instance.actionEventDic.Add("OGCD2_Init", new ActionEvent());

        instance.actionEventDic.TryGetValue("OGCD3_Init", out check);
        if (check == null)
            instance.actionEventDic.Add("OGCD3_Init", new ActionEvent());

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
        /*First skill*/
        Action holder = null;
        instance.actionDic.TryGetValue("Attack", out holder);
        if (holder == null)
        {
            holder = new Action();
            holder.AEfList = new List<ActionEffect>();
            holder.AEfList.Add(DealBasicDamage);
            holder.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("Attack", holder);
        }
        Action arrowShot = null;
        instance.actionDic.TryGetValue("ArrowShot", out arrowShot);
        if (arrowShot == null)
        {
            arrowShot = new Action();
            arrowShot.AEfList = new List<ActionEffect>();
            arrowShot.AEfList.Add(DealBasicDamage);
            arrowShot.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("ArrowShot", arrowShot);
        }
        Action fireball = null;
        instance.actionDic.TryGetValue("Fireball", out fireball);
        if (fireball == null)
        {
            fireball = new Action();
            fireball.AEfList = new List<ActionEffect>();
            fireball.AEfList.Add(DealBasicDamage);
            fireball.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("Fireball", fireball);
        }

        /*Second Skills*/
        Action comboAttack = null;
        instance.actionDic.TryGetValue("ComboAttack", out comboAttack);
        if (comboAttack == null)
        {
            comboAttack = new Action();
            comboAttack.AEfList = new List<ActionEffect>();
            comboAttack.AEfList.Add(DealComboDamage);
            comboAttack.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("ComboAttack", comboAttack);
        }
        Action multiShot = null;
        instance.actionDic.TryGetValue("MultiShot", out multiShot);
        if (multiShot == null)
        {
            multiShot = new Action();
            multiShot.AEfList = new List<ActionEffect>();
            multiShot.AEfList.Add(DealMultipleShot);
            multiShot.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("MultiShot", multiShot);
        }
        Action thunderBolt = null;
        instance.actionDic.TryGetValue("ThunderBolt", out thunderBolt);
        if (thunderBolt == null)
        {
            thunderBolt = new Action();
            thunderBolt.AEfList = new List<ActionEffect>();
            thunderBolt.AEfList.Add(DealThunderBolt);
            thunderBolt.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("ThunderBolt", thunderBolt);
        }

        /*Third Skill*/
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
        ActionEvent AE = new ActionEvent();
        if (instance.actionEventDic.TryGetValue("EnemyIsDead", out AE))
        {
            AE.AddListener(EnemyCheck);
        }
        if (instance.actionEventDic.TryGetValue("EnemyIsDead", out AE))
        {
            AE.AddListener(ChangeTarget);
        }
        if (instance.actionEventDic.TryGetValue("TargetIsChanged", out AE))
        {
            AE.AddListener(ShowTarget);
        }
    }

    private void Start()
    {
        for (int i = 0; i < OGCDTimer.Length; ++i)
        {
            OGCDTimer[i] = OGCD[i];
        }

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
        if (OGCDTimer[2] > 0)
            OGCDTimer[2] -= Time.deltaTime;

        for (int i = 0; i < enemy.Length; ++i)
        {
            EnemyAttackTimer[i] -= Time.deltaTime;

            if (EnemyAttackTimer[i] < 0 && (enemyAlive && playerAlive))
            {
                instance.TriggerAction("Attack", enemy[i]);
            }
        }
    }

    void DealBasicDamage(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);
        if (e.ID < 10 && !comboAttacking)
        {
            if (OGCDTimer[0] <= 0 && (playerAlive && enemyAlive))
            {
                instance.TriggerActionEvent("UseSkill", e);
                instance.TriggerActionEvent("TakeDamage", e);
                instance.TriggerActionEvent("OGCD1_Init", e);
                OGCDTimer[0] = OGCD[0];
            }
        }
        else if (e.ID == 10)
        {
            if (EnemyAttackTimer[0] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                instance.TriggerActionEvent("TakeDamage", e);
                EnemyAttackTimer[0] = EnemyAttackCooltime[0];
            }
        }
        else if (e.ID == 11)
        {
            if (EnemyAttackTimer[1] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                instance.TriggerActionEvent("TakeDamage", e);
                EnemyAttackTimer[1] = EnemyAttackCooltime[1];
            }
        }
        else if (e.ID == 12)
        {
            if (EnemyAttackTimer[2] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                instance.TriggerActionEvent("TakeDamage", e);
                EnemyAttackTimer[2] = EnemyAttackCooltime[2];
            }
        }
    }

    void DealComboDamage(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);

        if (playerAlive && enemyAlive && OGCDTimer[1] <= 0)
        {
            if (tempComboCount < comboCount)
            {
                comboAttacking = true;
                damage = damage + (damage * (0.2f * tempComboCount));
                ++tempComboCount;
                StartCoroutine(ComboAttackCool(GCD, e));
                instance.TriggerActionEvent("TakeDamage", e);
                instance.TriggerActionEvent("UseSkill", e);
                instance.TriggerActionEvent("ComboAttackStart", e);
            }
            else
            {
                tempComboCount = 0;
                comboAttacking = false;
                instance.TriggerActionEvent("OGCD2_Init", e);
                instance.TriggerActionEvent("ComboAttackFinish", e);
                OGCDTimer[1] = OGCD[1];
            }
        }
    }

    IEnumerator ComboAttackCool(float coolTime, Entity e)
    {
        yield return new WaitForSeconds(coolTime);
        DealComboDamage(e);
    }

    void DealMultipleShot(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);
        damage /= 2.0f;

        if (OGCDTimer[1] <= 0 && playerAlive && enemyAlive)
        {
            instance.TriggerActionEvent("UseSkill", e);
            instance.TriggerActionEvent("TakeDamage", e);
            new WaitForSeconds(1.5f);

            if (enemy.Length == 1)
            {
                instance.TriggerActionEvent("TakeDamage", e);
                instance.TriggerActionEvent("TakeDamage", e);
            }
            else if (enemy.Length == 2)
            {
                instance.TriggerActionEvent("TakeDamage", e);
                GameObject temp = e.target;
                if (temp == enemy[0])
                {
                    e.target = enemy[1];
                    instance.TriggerActionEvent("TakeDamage", e);
                }
                else if(temp == enemy[1])
                {
                    e.target = enemy[0];
                    instance.TriggerActionEvent("TakeDamage", e);
                }
                e.target = temp;
            }
            else
            {
                GameObject temp = e.target;
                if (e.target.GetComponent<Entity>().ID == 10)
                {
                    e.target = enemy[1];
                    instance.TriggerActionEvent("TakeDamage", e);
                    e.target = enemy[2];
                    instance.TriggerActionEvent("TakeDamage", e);
                    e.target = temp;
                }
                else if (e.target.GetComponent<Entity>().ID == (10 + enemy.Length - 1))
                {
                    e.target = enemy[enemy.Length - 2];
                    instance.TriggerActionEvent("TakeDamage", e);
                    e.target = enemy[enemy.Length - 3];
                    instance.TriggerActionEvent("TakeDamage", e);
                    e.target = temp;
                }
                else
                {
                    e.target = enemy[e.target.GetComponent<Entity>().ID - 10 + 1];
                    instance.TriggerActionEvent("TakeDamage", e);
                    e.target = enemy[e.target.GetComponent<Entity>().ID - 10 - 2];
                    instance.TriggerActionEvent("TakeDamage", e);
                    e.target = temp;
                }
            }
            instance.TriggerActionEvent("OGCD2_Init", e);
            OGCDTimer[1] = OGCD[1];
        }
    }

    void DealThunderBolt(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);
        damage *= 2.0f;
        if (OGCDTimer[1] <= 0 && playerAlive && enemyAlive)
        {
            instance.TriggerActionEvent("UseSkill", e);
            instance.TriggerActionEvent("TakeDamage", e);
            instance.TriggerActionEvent("OGCD2_Init", e);
            OGCDTimer[1] = OGCD[1];
        }
    }

    void SelfHeal(Entity e)
    {
        recoveryValue = 5.0f;
        if (!comboAttacking)
        {
            if (OGCDTimer[2] <= 0 && playerAlive && enemyAlive)
            {
                instance.TriggerActionEvent("UseSkill", e);
                instance.TriggerActionEvent("SelfHeal", e);
                instance.TriggerActionEvent("OGCD3_Init", e);
                OGCDTimer[2] = OGCD[2];
            }
        }
    }

    void TargetHPCheck(Entity e)
    {
        if(e.target.GetComponent<Entity>().ID < 10 && !e.target.activeSelf)
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
        for (int i = 0; i < enemy.Length; ++i)
        {
            if (!enemy[i].activeSelf)
            {
                enemyAlive = false;
            }
            else
            {
                enemyAlive = true;
                break;
            }
        }
        if(!enemyAlive)
            instance.TriggerActionEvent("AllEnemyIsDead", e);
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