using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public User user;
    public GameObject UnlockArcherImage;
    public GameObject UnlockWizardImage;

    public GameObject LockArcherImage;
    public GameObject LockWizardImage;

    public GameObject ArcherParticle;
    public GameObject WizardParticle;

    public GameObject GoldList;
    public GameObject GemList;
    public GameObject ItemList;
    public GameObject LegendsList;


    private void Start()
    {
        if (user.Archer)
            UnlockArcher();
        else
            LockArcher();

        if (user.Wizard)
            UnlockWizard();
        else
            LockWizard();

        OnClickGoldMenu();


    }
    private void Update()
    {
        
    }

    public void UnlockArcher()
    {
        if (user.Gold < 10000)
            return;
        UnlockArcherImage.SetActive(true);
        LockArcherImage.SetActive(false);
        ArcherParticle.SetActive(true);
        user.Archer = true;
        user.Gold -= 10000;

    }
    public void UnlockWizard()
    {
        if (user.Gold < 10000)
            return;
        UnlockWizardImage.SetActive(true);
        LockWizardImage.SetActive(false);
        WizardParticle.SetActive(true);
        user.Wizard = true;
        user.Gold -= 10000;
    }

    public void LockArcher()
    {
        UnlockArcherImage.SetActive(false);
        LockArcherImage.SetActive(true);
        ArcherParticle.SetActive(false);
        user.Archer = false;
    }
    public void LockWizard()
    {
        UnlockWizardImage.SetActive(false);
        LockWizardImage.SetActive(true);
        WizardParticle.SetActive(false);
        user.Wizard = false;
    }
  

    public void OnClickGoldMenu()
    {
        GoldList.SetActive(true);
        GemList.SetActive(false);
        ItemList.SetActive(false);
        LegendsList.SetActive(false);
    }

    public void OnClickGemMenu()
    {
        GoldList.SetActive(false);
        GemList.SetActive(true);
        ItemList.SetActive(false);
        LegendsList.SetActive(false);
    }
    public void OnClickItemMenu()
    {
        GoldList.SetActive(false);
        GemList.SetActive(false);
        ItemList.SetActive(true);
        LegendsList.SetActive(false);
    }
    public void OnClickLegendsMenu()
    {
        GoldList.SetActive(false);
        GemList.SetActive(false);
        ItemList.SetActive(false);
        LegendsList.SetActive(true);
    }
}
