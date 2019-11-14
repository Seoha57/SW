using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    AudioSource BGM;
    public AudioClip[] BGMSound;
    public string[] BGMName;

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
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        BGM = GetComponent<AudioSource>();
        BGM.clip = BGMSound[0];
        BGM.volume = 0.1f;
    }

    private void Update()
    {
        PlayBGM();
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
}
