using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttributeDistribution))]
[RequireComponent(typeof(Attributes))]
public class Character : Entity
{
    public AttributeDistribution AD;

    [Header("Name")]
    public string Name;
    [Header("Attribute Point")]
    public int attributePoint = 0;
    
    [Header("LEVEL")]
    public int Level =1;
    public int XP = 0;
    public int XP_required = 100;

    [Header("Flat point")]
    public float FlatPoint;

    [Header("Item List")]
    public Item[] items;

    //Attributes
    private float init_MaxHealth = 100f;
    private float init_Armor = 10f;
    private float init_Mana = 10f;
    private float init_Damage = 10f;
    private float init_Speed = 10f;

    CharacterManager CM;

    private void Start()
    {
        
        //int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        //items = new Item[numSlots];
        //CharacterManager.GetCharacter(ID).items = new Item[numSlots];
        CM = CharacterManager.instance;
        Setting();
        if (AD == null)
            AD = transform.GetComponent<AttributeDistribution>();

        ActionEvent Physical = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("TakePhysicalDamage", out Physical))
        {
            Physical.AddListener(TakeDamage);
        }
        ActionEvent heal = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("SelfHeal", out Physical))
        {
            Physical.AddListener(SelfRecovery);
        }
        ActionEvent reset = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("PlayerIsDead", out Physical))
        {
            Physical.AddListener(ResetCharacter);
        }

    }
    private void Update()
    {
        Levelup();
    }

    void TakeDamage(Entity e)
    {
        if (e.target.GetComponent<Character>() == this)
        {
            this.HP -= Actions.GetPhysicalDamage();
            if (this.HP <= 0)
                this.gameObject.SetActive(false);
        }
    }

    void SelfRecovery(Entity e)
    {
        if (e.gameObject.GetComponent<Character>() == this)
        {
            if (this.HP < this.init_MaxHealth)
                this.HP += Actions.GetRecoveryValue();

            if (this.HP >= this.init_MaxHealth)
                this.HP = this.init_MaxHealth;
        }
    }

    void ResetCharacter(Entity e)
    {
        RESET_ATTRIBUTE();
    }

    public void OnAddButtonCON()
    {
      
        if (attributePoint <= 0)
            return;

        AD.CON++;
        attributePoint--;

        CharacterManager.instance.GetCharacter(ID).attributePoint = attributePoint;
        CharacterManager.instance.AddMaxHealth(ID, FlatPoint);
        Debug.Log("Added Con " + AD.CON);

        CharacterManager.instance.GetCharacter(ID).AD.CON = AD.CON;
    }

    public void OnAddButtonWIS()
    {
        if (attributePoint <= 0)
            return;


        AD.WIS++;
        attributePoint--;
        Debug.Log("Added WIS " + AD.WIS);
        CharacterManager.instance.GetCharacter(ID).attributePoint = attributePoint;
        CharacterManager.instance.AddMana(ID, FlatPoint);
        CharacterManager.instance.GetCharacter(ID).AD.WIS = AD.WIS;
    }

    public void OnAddButtonSTR()
    {
        if (attributePoint <= 0)
            return;

        AD.STR++;
        attributePoint--;
        Debug.Log("Added STR " + AD.STR);
        CharacterManager.instance.GetCharacter(ID).attributePoint = attributePoint;
        CharacterManager.instance.AddDamage(ID, FlatPoint);
        CharacterManager.instance.GetCharacter(ID).AD.STR = AD.STR;
    }

    public void OnAddButtonDEX()
    {
        if (attributePoint <= 0)
            return;

        AD.DEX++;
        attributePoint--;
        Debug.Log("Added DEX " + AD.DEX);
        CharacterManager.instance.GetCharacter(ID).attributePoint = attributePoint;
        CharacterManager.instance.AddSpeed(ID, FlatPoint);
        CharacterManager.instance.GetCharacter(ID).AD.DEX = AD.DEX;
    }
    public void OnAddButtonINT()
    {
        if (attributePoint <= 0)
            return;

        AD.INT++;
        attributePoint--;
        CharacterManager.instance.GetCharacter(ID).attributePoint = attributePoint;
        Debug.Log("Added INT " + AD.INT);
        CharacterManager.instance.AddArmor(ID, FlatPoint);
        CharacterManager.instance.GetCharacter(ID).AD.INT = AD.INT;
    }
    public void RESET_ATTRIBUTE()
    {
        CharacterManager.instance.SetAttributePoint(ID, 0);
        CharacterManager.instance.SetLevel(ID, 1);
        CharacterManager.instance.SetExp(ID, 0);
        CharacterManager.instance.SetExpRequired(ID, 100);
        CharacterManager.instance.SetMaxHealth(ID, 100);
        CharacterManager.instance.SetArmor(ID, 10);
        CharacterManager.instance.SetMana(ID, 10);
        CharacterManager.instance.SetDamage(ID, 10);
        CharacterManager.instance.SetSpeed(ID, 10);

        for (int i = 0; i < items.Length; i++)
        {
            CharacterManager.instance.GetCharacter(ID).items[i] = null;
        }

        Setting();

    }

    public void Equip(Item newitem)
    {

        int itemIndex = (int)newitem.equipSlot;
        Item oldItem = null;

        if (items[itemIndex] != null)
        {
            Debug.Log("Changed! ");
            oldItem = items[itemIndex];
            Inventory.instance.Add(oldItem);
        }

        items[itemIndex] = newitem;
        //EquipmentManager.instance.currentEquipment[itemIndex] = newitem;
        CharacterManager.instance.GetCharacter(ID).items[itemIndex] = newitem;


        CharacterManager.instance.AddMaxHealth(ID, newitem.MaxHealthModifier);
        CharacterManager.instance.AddArmor(ID, newitem.ArmorModifier);
        CharacterManager.instance.AddMana(ID, newitem.ManaModifier);
        CharacterManager.instance.AddDamage(ID, newitem.DamageModifier);
        CharacterManager.instance.AddSpeed(ID, newitem.SpeedModifier);


    }
    public void UnEquip(int itemIndex)
    {
        if (items[itemIndex] != null)
        {
            Item oldItem = items[itemIndex];
            Inventory.instance.Add(oldItem);
            CharacterManager.instance.AddMaxHealth(ID, -oldItem.MaxHealthModifier);
            CharacterManager.instance.AddArmor(ID, -oldItem.ArmorModifier);
            CharacterManager.instance.AddMana(ID, -oldItem.ManaModifier);
            CharacterManager.instance.AddDamage(ID, -oldItem.DamageModifier);
            CharacterManager.instance.AddSpeed(ID, -oldItem.SpeedModifier);

            items[itemIndex] = null;

            //EquipmentManager.instance.currentEquipment[itemIndex] = null;
        }
    }

    #region Simple ADD function
    public void AddMaxHealth()
    {
        Debug.Log("Add Max health!");
        float result = AD.AD_MaxHealth(init_MaxHealth);
        CharacterManager.instance.SetMaxHealth(ID, result);
    }
    public void AddArmor()
    {
        Debug.Log("Add Armor!");
        float result = AD.AD_Armor(init_Armor);
        CharacterManager.instance.SetArmor(ID, result);
    }
    public void AddMana()
    {
        Debug.Log("Add Mana!");
        float result = AD.AD_Mana(init_Mana);
        CharacterManager.instance.SetMana(ID, result);
    }
    public void AddDamage()
    {
        Debug.Log("Add Damage!");
        float result = AD.AD_Damage(init_Damage);
        CharacterManager.instance.SetDamage(ID, result);
    }
    public void AddSpeed()
    {
        Debug.Log("Add Speed!");
        float result = AD.AD_Speed(init_Speed);
        CharacterManager.instance.SetSpeed(ID, result);
    }

    #endregion

    public void AddAttributePoint()
    {
        Debug.Log("Added Attribute Point");
        attributePoint += 5;
        CharacterManager.instance.SetAttributePoint(ID, attributePoint);
    }
    public void AddEXP()
    {
        Debug.Log("Added Exp");
        XP += 100;
        CharacterManager.instance.SetExp(ID, XP);
    }

    private void Levelup()
    {
        if (XP >= XP_required)
        {
            Debug.Log(transform.name + " Level up!!!");
            Level++;
            CharacterManager.instance.SetLevel(ID, Level);

            XP = 0;
            CharacterManager.instance.SetExp(ID, XP);
            XP_required *= 2;
            CharacterManager.instance.SetExpRequired(ID, XP_required);

            AddAttributePoint();
        }

    }

    private void Setting()
    {
        attributePoint = CM.GetCharacter(ID).attributePoint;
        Level = CM.GetLevel(ID);
        XP = CM.GetExp(ID);
        XP_required = CM.GetExpRequried(ID);
        HP = CM.GetMaxHealth(ID);

        attribute.MaxHealth = CM.GetMaxHealth(ID);
        attribute.Armor = CM.GetArmor(ID);
        attribute.Mana = CM.GetMana(ID);
        attribute.Damage = CM.GetDamage(ID);
        attribute.Speed = CM.GetSpeed(ID);

        items = CM.GetCharacter(ID).items;

    }
}
