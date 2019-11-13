using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMode : MonoBehaviour
{

    public GameObject selectMode;
    public FlexibleUIButton UIbutton;
    // Start is called before the first frame update
    void Start()
    {
        selectMode.gameObject.SetActive(false);
        UIbutton = GetComponent<FlexibleUIButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectModeClick()
    {
        selectMode.gameObject.SetActive(true);
    }

    public void SelectedMode1()
    {
        //Tutorial
        UIbutton.buttontype = FlexibleUIButton.ButtonType.Type1;
        selectMode.gameObject.SetActive(false);
    }
    public void SelectedMode2()
    {
        //Dungeon1
        UIbutton.buttontype = FlexibleUIButton.ButtonType.Type2;
        selectMode.gameObject.SetActive(false);

    }
    public void SelectedMode3()
    {
        //Dungeon2
        UIbutton.buttontype = FlexibleUIButton.ButtonType.Type3;
        selectMode.gameObject.SetActive(false);

    }
}
