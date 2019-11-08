using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager instance { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            SceneManager.LoadScene(1);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SceneManager.LoadScene(0);
    }

    public void GoToCombatSystemScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToCharacterSetUpScene()
    {
        SceneManager.LoadScene(0);
    }
}
