using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] player;
    public GameObject[] enemy;
    public static CharacterManager instance { get; private set; }
    public List<Character> characters;
    private static List<Enemy> enemys;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            characters = new List<Character>();
            characters.Clear();
            for (int i = 0; i < player.Length; i++)
            {
                characters.Add(player[i].GetComponent<Character>());
                characters[i].RESET_ATTRIBUTE();
            }
            enemys = new List<Enemy>();
            enemys.Clear();
            for (int i = 0; i < enemy.Length; i++)
            {
                enemys.Add(player[i].GetComponent<Enemy>());

            }
            DontDestroyOnLoad(this);
        }          
    }

    #region Character tracking

    // private const string CHARACTER_ID_PREFIX = "Character";
    //private static Dictionary<string, Character> characters = new Dictionary<string, Character>();

    //public static void RegisterCharacter(string _ID, Character _character)
    //{
    //    string _characterID = _ID;
    //    characters.Add(_ID, _character);
    //    _character.transform.name = _characterID;
    //}

    //public static void UnRegisterCharacter(string _characterID)
    //{
    //    characters.Remove(_characterID);
    //}


    #endregion
 
    public  Character GetCharacter(int _characterID)
    {
        return characters[_characterID];
    }
    public static Enemy GetEnemy(int _enemyID)
    {
        return enemys[_enemyID];
    }
    #region Get
    //Character
    public  int GetAttributePoint(int id) { return characters[id].attributePoint; }
    public  int GetLevel(int id) { return characters[id].Level; }
    public  int GetExp(int id) { return characters[id].XP; }
    public  int GetExpRequried(int id) { return characters[id].XP_required; }

    //Attribute

    public float GetMaxHealth(int id) { return characters[id].attribute.MaxHealth; }

    public float GetArmor(int id) { return characters[id].attribute.Armor; }
    public float GetMana(int id) { return characters[id].attribute.Mana; }
    public float GetDamage(int id) { return characters[id].attribute.Damage; }
    public float GetSpeed(int id) { return characters[id].attribute.Speed; }
    #endregion

    #region Set
    //Character
    public void SetAttributePoint(int id, int val) { characters[id].attributePoint = val; }
    public void SetLevel(int id, int val) { characters[id].Level = val; }
    public void SetExp(int id, int val) { characters[id].XP = val; }
    public void SetExpRequired(int id, int val) { characters[id].XP_required = val; }

    //Attribute
    public void SetMaxHealth(int id, float val) { characters[id].attribute.MaxHealth = val; }
    public void SetArmor(int id, float val) { characters[id].attribute.Armor = val; }
    public void SetMana(int id, float val) { characters[id].attribute.Mana = val; }
    public void SetDamage(int id, float val) { characters[id].attribute.Damage = val; }
    public void SetSpeed(int id, float val) { characters[id].attribute.Speed = val; }
    #endregion

    #region Add
    public void AddMaxHealth(int id, float val) { characters[id].attribute.MaxHealth += val; }
    public void AddArmor(int id, float val) { characters[id].attribute.Armor += val; }
    public void AddMana(int id, float val) { characters[id].attribute.Mana += val; }
    public void AddDamage(int id, float val) { characters[id].attribute.Damage += val; }
    public void AddSpeed(int id, float val) { characters[id].attribute.Speed += val; }
    #endregion



    //#region Attribute Getter
    //public static float GetHP(int id) { return characters[id].HP; }
    //public static float GetMaxHealth(int id) { return characters[id].attribute.MaxHealth; }
    //public static float GetArmor(int id) { return characters[id].attribute.Armor; }
    //public static float GetElementalWard(int id) { return characters[id].attribute.ElementalWard; }
    //public static float GetAfflictionResistance(int id) { return characters[id].attribute.AfflictionResistance; }

    //public static float GetMaxResource(int id) { return characters[id].attribute.MaxResource; }
    //public static float GetResourceRegeneration(int id) { return characters[id].attribute.ResourceRegeneration; }
    //public static float GetResourceCostReduction(int id) { return characters[id].attribute.ResourceCostReduction; }

    //public static float GetArmorBreak(int id) { return characters[id].attribute.ArmorBreak; }
    //public static float GetPhysicalDamage(int id) { return characters[id].attribute.PhysicalDamage; }
    //public static float GetSovereignDamage(int id) { return characters[id].attribute.SovereignDamage; }

    //public static float GetCriticalChance(int id) { return characters[id].attribute.CriticalChance; }
    //public static float GetCriticalDamage(int id) { return characters[id].attribute.CriticalDamage; }
    //public static float GetAttackSpeed(int id) { return characters[id].attribute.AttackSpeed; }

    //public static float GetElemetalDamage(int id) { return characters[id].attribute.ElemetalDamage; }
    //public static float GetWardBreak(int id) { return characters[id].attribute.WardBreak; }
    //public static float GetCastingSpeed(int id) { return characters[id].attribute.CastingSpeed; }

    //#endregion
    //#region Attribute Point Getter
    ////public static float AD_GetMaxHealth(int id) { return characters[id].AD.point.MaxHealth; }

    //#endregion

    //#region Setter
    //public static void SetAttributePoint(int id, int val) { characters[id].attributePoint = val; }
    //public static void SetLevel(int id, int val) { characters[id].Level = val; }
    //public static void SetExp(int id, int val) { characters[id].XP = val; }
    //public static void SetExpRequired(int id, int val) { characters[id].XP_required = val; }


    //public static void SetHP(int id, float val) { characters[id].HP = val; }
    //public static void SetMaxHealth(int id, float val) { characters[id].attribute.MaxHealth = val; }
    //public static void SetArmor(int id, float val) { characters[id].attribute.Armor = val; }
    //public static void SetElementalWard(int id, float val) { characters[id].attribute.ElementalWard = val; }
    //public static void SetAfflictionResistance(int id, float val) { characters[id].attribute.AfflictionResistance = val; }

    //public static void SetMaxResource(int id, float val) { characters[id].attribute.MaxResource = val; }
    //public static void SetResourceRegeneration(int id, float val) { characters[id].attribute.ResourceRegeneration = val; }
    //public static void SetResourceCostReduction(int id, float val) { characters[id].attribute.ResourceCostReduction = val; }


    //public static void SetArmorBreak(int id, float val) { characters[id].attribute.ArmorBreak = val; }
    //public static void SetPhysicalDamage(int id, float val) { characters[id].attribute.PhysicalDamage = val; }
    //public static void SetSovereignDamage(int id, float val) { characters[id].attribute.SovereignDamage = val; }

    //public static void SetCriticalChance(int id, float val) { characters[id].attribute.CriticalChance = val; }
    //public static void SetCriticalDamage(int id, float val) { characters[id].attribute.CriticalDamage = val; }
    //public static void SetAttackSpeed(int id, float val) { characters[id].attribute.AttackSpeed = val; }

    //public static void SetElemetalDamage(int id, float val) { characters[id].attribute.ElemetalDamage = val; }
    //public static void SetWardBreak(int id, float val) { characters[id].attribute.WardBreak = val; }
    //public static void SetCastingSpeed(int id, float val) { characters[id].attribute.CastingSpeed = val; }

    //#endregion

    //public static void AD_SetMaxHealth(int id, float val) { characters[id].AD.point.MaxHealth = val; }
}
