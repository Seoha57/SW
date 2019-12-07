using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectMode : MonoBehaviour
{

    public GameObject selectMode;
    public FlexibleUIButton UIbutton;
    public ButtonSceneChange BSC;
    public Image Mode;
    public Sprite Mode1;
    public Sprite Mode2;
    public Sprite Mode3;
    public int mode = 0;

    // Start is called before the first frame update
    void Start()
    {
        selectMode.gameObject.SetActive(false);
        UIbutton = GetComponent<FlexibleUIButton>();
        Mode.sprite = Mode1;
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
        //UIbutton.buttontype = FlexibleUIButton.ButtonType.Type2;
        Mode.sprite = Mode1;
        selectMode.gameObject.SetActive(false);
        mode = 2;
        BSC.SceneName = "Dungeon1_1";
    }
    public void SelectedMode2()
    {
        //Dungeon1
        // UIbutton.buttontype = FlexibleUIButton.ButtonType.Type3;
        Mode.sprite = Mode2;
        selectMode.gameObject.SetActive(false);
        mode = 3;
        BSC.SceneName = "Dungeon2_1";

    }
    public void SelectedMode3()
    {
        //Dungeon2
        //UIbutton.buttontype = FlexibleUIButton.ButtonType.Type4;
        Mode.sprite = Mode3;
        selectMode.gameObject.SetActive(false);
        mode = 4;
        BSC.SceneName = "Dungeon3_1";

    }
}
