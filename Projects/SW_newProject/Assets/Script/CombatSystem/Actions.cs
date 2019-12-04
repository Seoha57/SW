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
        GCDTimer = 0;
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

        /**/
        instance.actionEventDic.TryGetValue("UseFire", out check);
        if (check == null)
            instance.actionEventDic.Add("UseFire", new ActionEvent());

        instance.actionEventDic.TryGetValue("UseThunder", out check);
        if (check == null)
            instance.actionEventDic.Add("UseThunder", new ActionEvent());
        /**/


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
        instance.actionDic.TryGetValue("Arrow Shot", out arrowShot);
        if (arrowShot == null)
        {
            arrowShot = new Action();
            arrowShot.AEfList = new List<ActionEffect>();
            arrowShot.AEfList.Add(DealBasicDamage);
            arrowShot.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("Arrow Shot", arrowShot);
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
        instance.actionDic.TryGetValue("Combo Attack", out comboAttack);
        if (comboAttack == null)
        {
            comboAttack = new Action();
            comboAttack.AEfList = new List<ActionEffect>();
            comboAttack.AEfList.Add(DealComboDamage);
            comboAttack.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("Combo Attack", comboAttack);
        }
        Action multiShot = null;
        instance.actionDic.TryGetValue("Multi Shot", out multiShot);
        if (multiShot == null)
        {
            multiShot = new Action();
            multiShot.AEfList = new List<ActionEffect>();
            multiShot.AEfList.Add(DealMultipleShot);
            multiShot.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("Multi Shot", multiShot);
        }
        Action thunderBolt = null;
        instance.actionDic.TryGetValue("Thunder Bolt", out thunderBolt);
        if (thunderBolt == null)
        {
            thunderBolt = new Action();
            thunderBolt.AEfList = new List<ActionEffect>();
            thunderBolt.AEfList.Add(DealThunderBolt);
            thunderBolt.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("Thunder Bolt", thunderBolt);
        }

        /*Third Skill*/
        Action recovery = null;
        instance.actionDic.TryGetValue("Self Recovery", out recovery);
        if (recovery == null)
        {
            recovery = new Action();
            recovery.AEfList = new List<ActionEffect>();
            recovery.AEfList.Add(SelfHeal);
            recovery.AEfList.Add(TargetHPCheck);
            instance.actionDic.Add("Self Recovery", recovery);
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

        if(comboAttacking)
        {
            GCDTimer -= Time.deltaTime;
            if(GCDTimer < 0)
            {
                instance.TriggerAction("Combo Attack", player[0]);
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
                if(e.ID == 2)
                {
                    instance.TriggerActionEvent("UseFire", e);
                }

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
                EnemyAttackTimer[0] = EnemyAttackCooltime[0];
                instance.TriggerActionEvent("TakeDamage", e);
            }
        }
        else if (e.ID == 11)
        {
            if (EnemyAttackTimer[1] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                EnemyAttackTimer[1] = EnemyAttackCooltime[1];
                instance.TriggerActionEvent("TakeDamage", e);
            }
        }
        else if (e.ID == 12)
        {
            if (EnemyAttackTimer[2] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                EnemyAttackTimer[2] = EnemyAttackCooltime[2];
                instance.TriggerActionEvent("TakeDamage", e);
            }
        }
        else if (e.ID == 13)
        {
            if (EnemyAttackTimer[3] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                EnemyAttackTimer[3] = EnemyAttackCooltime[3];
                instance.TriggerActionEvent("TakeDamage", e);
            }
        }
        else if (e.ID == 14)
        {
            if (EnemyAttackTimer[4] <= 0 && (playerAlive && e.gameObject.activeSelf))
            {
                EnemyAttackTimer[4] = EnemyAttackCooltime[4];
                instance.TriggerActionEvent("TakeDamage", e);
            }
        }
    }

    void DealComboDamage(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);

        if (playerAlive && enemyAlive && OGCDTimer[1] <= 0 && GCDTimer <= 0)
        {
            if (tempComboCount < comboCount)
            {
                if(comboAttacking == false)
                {
                    instance.TriggerActionEvent("UseSkill", e);
                    instance.TriggerActionEvent("ComboAttackStart", e);
                }
                comboAttacking = true;
                damage = damage + (damage * (0.2f * tempComboCount));
                ++tempComboCount;
                instance.TriggerActionEvent("TakeDamage", e);
                GCDTimer = GCD;
            }
            else
            {
                tempComboCount = 0;
                comboAttacking = false;
                OGCDTimer[1] = OGCD[1];
                GCDTimer = 0;
                instance.TriggerActionEvent("OGCD2_Init", e);
                instance.TriggerActionEvent("ComboAttackFinish", e);
            }
        }
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

            switch (enemy.Length)
            {
                case 1:
                    instance.TriggerActionEvent("TakeDamage", e);
                    instance.TriggerActionEvent("TakeDamage", e);
                    break;

                case 2:
                    {
                        instance.TriggerActionEvent("TakeDamage", e);
                        GameObject temp = e.target;
                        if (temp == enemy[0])
                        {
                            e.target = enemy[1];
                            instance.TriggerActionEvent("TakeDamage", e);
                        }
                        else if (temp == enemy[1])
                        {
                            e.target = enemy[0];
                            instance.TriggerActionEvent("TakeDamage", e);
                        }
                        e.target = temp;
                        break;
                    }
                default:
                    {
                        GameObject temp = e.target;
                        int enemy_num = 0;
                        for(int i = 0; i < enemy.Length; ++i)
                        {
                            if(enemy[i].GetComponent<Entity>().ID == e.target.GetComponent<Entity>().ID)
                            {
                                enemy_num = i;
                                break;
                            }
                        }

                        int next = LookLeft(enemy_num);
                        e.target = enemy[next];
                        instance.TriggerActionEvent("TakeDamage", e);

                        next = LookRight(enemy_num);
                        e.target = enemy[next];
                        instance.TriggerActionEvent("TakeDamage", e);

                        e.target = temp;
                        break;
                    }
            }
            OGCDTimer[1] = OGCD[1];
            instance.TriggerActionEvent("OGCD2_Init", e);
        }
    }

    void DealThunderBolt(Entity e)
    {
        damage = actionHelper.DamageMod(e, AttackType.PHYSICAL);
        damage *= 2.0f;
        if (OGCDTimer[1] <= 0 && playerAlive && enemyAlive)
        {
            OGCDTimer[1] = OGCD[1];
            instance.TriggerActionEvent("UseThunder", e);
            instance.TriggerActionEvent("UseSkill", e);
            instance.TriggerActionEvent("TakeDamage", e);
            instance.TriggerActionEvent("OGCD2_Init", e);
        }
    }

    void SelfHeal(Entity e)
    {
        recoveryValue = 5.0f;
        if (!comboAttacking)
        {
            if (OGCDTimer[2] <= 0 && playerAlive && enemyAlive)
            {
                OGCDTimer[2] = OGCD[2];
                instance.TriggerActionEvent("UseSkill", e);
                instance.TriggerActionEvent("SelfHeal", e);
                instance.TriggerActionEvent("OGCD3_Init", e);
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

    int LookLeft(int ePos)
    {
        int next = ePos;
        if (ePos == 0) return 0;
        for (int i = ePos - 1; i >= 0; --i)
        {
            if (enemy[i].activeSelf)
            {
                next = i;
                break;
            }
        }

        return next;
    }

    int LookRight(int ePos)
    {
        int next = ePos;

        if (ePos == enemy.Length - 1) return ePos;

        for (int i = ePos + 1; i < enemy.Length; ++i)
        {
            if (enemy[i].activeSelf)
            {
                next = i;
                break;
            }
        }

        return next;
    }

    #region Get Functions
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
    #endregion
}