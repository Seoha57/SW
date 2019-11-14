using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    AudioSource BGM;
    public AudioClip[] BGMSound;
    public string[] BGMName;

    string prevScene;
    string currScene;

    private static SoundManager SM;
    public static SoundManager instance
    {
        get
        {
            if(!SM)
            {
                GameObject obj = new GameObject();
                obj.hideFlags = HideFlags.HideAndDontSave;
                SM = obj.AddComponent<SoundManager>() as SoundManager;
            }
            return SM;
        }
    }

    private void Awake()
    {
        int numSoundManager = FindObjectsOfType<SoundManager>().Length;
        if (numSoundManager != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        BGM = GetComponent<AudioSource>();
        BGM.clip = BGMSound[0];
        BGM.volume = 0.1f;
        prevScene = SceneManager.GetActiveScene().name;
        PlayBGM();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != prevScene)
        {
            currScene = SceneManager.GetActiveScene().name;
            if (currScene == "Stage1" || currScene == "Stage2")
            {
                ChangeBGM(1);
                BGM.volume = 0.5f;
            }
            else
            {
                if (BGM.clip != BGMSound[0])
                {
                    ChangeBGM(0);
                    BGM.volume = 0.1f;
                }
            }
        }
        prevScene = SceneManager.GetActiveScene().name;
    }

    public void PlayBGM()
    {
        if (!BGM.isPlaying)
            BGM.Play();
    }

    public void StopBGM()
    {
        BGM.Stop();
    }

    public void ChooseBGM(int index)
    {
        BGM.clip = BGMSound[index];
    }

    void ChangeBGM(string BGM_name)
    {
        BGM.Stop();

        BGM.Play();
    }
    void ChangeBGM(int index)
    {
        BGM.Stop();

        BGM.clip = BGMSound[index];

        BGM.Play();
    }
}
