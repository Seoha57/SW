using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    //BGM
    AudioSource BGM;
    public AudioClip[] BGMSound;
    public string[] BGMName;

    //SFX
    public AudioSource[] soundEffectChannel;
    public AudioClip[] SampleSounds;
    public string[] SoundsNames;
    Dictionary<string, AudioClip> SFXs;
    const int MAX_NUMBER_OF_CHANNEL = 10;
    int numberOfChannel;
    int count = 0;

    string prevScene;
    string currScene;

    private static SoundManager SM;
    public static SoundManager instance
    {
        get
        {
            if(!SM)
            {
                SM = FindObjectOfType(typeof(SoundManager)) as SoundManager;
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
        //BGM
        BGM = GetComponent<AudioSource>();
        BGM.clip = BGMSound[0];
        BGM.volume = 0.1f;
        prevScene = SceneManager.GetActiveScene().name;
        PlayBGM();

        //SFX
        SFXs = new Dictionary<string, AudioClip>();
        numberOfChannel = soundEffectChannel.Length;
        AssignAudioClip(SampleSounds, SoundsNames);
        for (int i = 0; i < numberOfChannel; i++)
            soundEffectChannel[i].volume = 1.0f;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != prevScene)
        {
            currScene = SceneManager.GetActiveScene().name;
            if (currScene == "Stage1")
            {
                ChangeBGM(1);
                BGM.volume = 0.5f;
            }
            else if(currScene == "Stage2")
            {
                ChangeBGM(2);
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
    AudioSource FindEmptyChannel()
    {
        if (count >= MAX_NUMBER_OF_CHANNEL)
            count = 0;

        return soundEffectChannel[count++];
    }
    void AssignAudioClip(AudioClip[] audios, string[] audiosName)
    {
        for (int i = 0; i < audios.Length; i++)
            SFXs.Add(audiosName[i], audios[i]);
    }
    public void PlaySound(string soundName)
    {
        AudioClip output;
        SFXs.TryGetValue(soundName, out output);

        AudioSource audio = FindEmptyChannel();
        audio.clip = output;
        audio.Play();
    }
}
