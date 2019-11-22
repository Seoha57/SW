using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public Interactable focus;

    public float Gold;
    public float Gem;
    public CharacterManager characterManager;
    public Inventory inventory;
    public int SelectedID=0;

    public bool Warrior = true;
    public bool Archer = false;
    public bool Wizard = false;

    private void Update()
    {
        // If we press left mouse
        if (Input.GetButtonDown("Fire1"))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null)
                {
                    SetFocus(interactable);

                    interactable.Interact();

                }
            }
        }
        // If we press right mouse
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                Interactable interactable = hit.collider.GetComponent<Interactable>();

                RemoveFocus();
            }
        }
    }

    public float GetGold()
    {
        return Gold;
    }

    public void AddGold()
    {
        Gold += 100;
    }
    public void SetGold(int val)
    {
        Gold += val;
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    void RemoveFocus()
    {
        focus = null;
    }
    public void SelectCharacter1()
    {
        SelectedID = 0;
    }
    public void SelectCharacter2()
    {
        SelectedID = 1;
    }
    public void SelectCharacter3()
    {
        SelectedID = 2;
    }
    public void CharacterReset()
    {

        CharacterManager.instance.GetCharacter(SelectedID).RESET_ATTRIBUTE();
    }

    public void ADD_HP()
    {
        CharacterManager.instance.AddMaxHealth(SelectedID, 10);
    }
    public void ADD_Mana()
    {
        CharacterManager.instance.AddMana(SelectedID, 1);
    }
    public void ADD_Damage()
    {
        CharacterManager.instance.AddDamage(SelectedID, 1);
    }
    public void ADD_Armor()
    {
        CharacterManager.instance.AddArmor(SelectedID, 1);
    }
    public void ADD_Speed()
    {
        CharacterManager.instance.AddSpeed(SelectedID, 1);
    }
}
