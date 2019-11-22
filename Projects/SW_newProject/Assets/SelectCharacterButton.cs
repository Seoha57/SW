using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterButton : MonoBehaviour
{
    public User user;
    public GameObject Archer;
    public GameObject Wizard;

    // Start is called before the first frame update
    void Start()
    {
        if (user.Archer)
            Archer.SetActive(true);
        else
            Archer.SetActive(false);

        if (user.Wizard)
            Wizard.SetActive(true);
        else
            Wizard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
