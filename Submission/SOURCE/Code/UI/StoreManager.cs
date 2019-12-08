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
    public GameObject CharactersList;


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
    public void BuyWizard()
    {
        if (user.Gold < 10000)
            return;
        UnlockWizard();
        user.Gold -= 10000;
    }
    public void BuyArcher()
    {
        if (user.Gold < 10000)
            return;
        UnlockArcher();
        user.Gold -= 10000;
    }
    public void UnlockArcher()
    {
 
        UnlockArcherImage.SetActive(true);
        LockArcherImage.SetActive(false);
        ArcherParticle.SetActive(true);
        user.Archer = true;
  

    }
    public void UnlockWizard()
    {

        UnlockWizardImage.SetActive(true);
        LockWizardImage.SetActive(false);
        WizardParticle.SetActive(true);
        user.Wizard = true;

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
        CharactersList.SetActive(false);
    }

    public void OnClickGemMenu()
    {
        GoldList.SetActive(false);
        GemList.SetActive(true);
        ItemList.SetActive(false);
        LegendsList.SetActive(false);
        CharactersList.SetActive(false);
    }
    public void OnClickItemMenu()
    {
        GoldList.SetActive(false);
        GemList.SetActive(false);
        ItemList.SetActive(true);
        LegendsList.SetActive(false);
        CharactersList.SetActive(false);
    }
    public void OnClickLegendsMenu()
    {
        GoldList.SetActive(false);
        GemList.SetActive(false);
        ItemList.SetActive(false);
        LegendsList.SetActive(true);
        CharactersList.SetActive(true);
    }
}
