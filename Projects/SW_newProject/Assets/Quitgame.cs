using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitgame : MonoBehaviour
{
    public GameObject quitwindow;
    // Start is called before the first frame update
    void Start()
    {
        quitwindow.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            quitwindow.gameObject.SetActive(true);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Cancel()
    {
        quitwindow.gameObject.SetActive(false);
    }

}
