﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public Interactable focus;

    public float Gold;
    public CharacterManager characterManager;
    public Inventory inventory;
    public int SelectedID=0;

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

}
