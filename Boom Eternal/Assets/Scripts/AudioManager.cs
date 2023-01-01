using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Dictionary<string, Sound> soundsDict = new Dictionary<string, Sound>();

    void Awake()
    {
        Debug.Log(10);
        DontDestroyOnLoad(this);
        if (GlobalReferences.audioManager == null)
        {
            Debug.Log(20);
            GlobalReferences.audioManager = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log(30);
            GlobalReferences.audioManager.PlayMusic();        
            return;
        }

        foreach(Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.playOnAwake = false;
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            soundsDict.Add(sound.name, sound);
        }
        Debug.Log(40);
        PlayMusic();
    }

    private void PlayMusic()
    {
        Debug.Log(50);
        if (SceneManager.GetActiveScene().name == "StartMenu2")
        {
            Debug.Log(60);
            playSound("menuMusic");
        }
        else
        {
            Debug.Log(70);
            AudioSource menuMusicSource = soundsDict["menuMusic"].audioSource;
            menuMusicSource.Stop();

            AudioSource gameMusicSource = soundsDict["gameMusic1"].audioSource;
            if (!gameMusicSource.isPlaying)
            {
                Debug.Log(80);
                gameMusicSource.loop = true;
                gameMusicSource.Play();
            }           
        }
    }

    public void playSound(string name)
    {
        playSound(name, 1, 1);
    }

    public void playSound(string name, float volumeChange, float pitchChange)
    {
        Sound sound = soundsDict[name];
        sound.audioSource.volume = sound.volume * volumeChange;
        sound.audioSource.pitch = sound.pitch * pitchChange;
        sound.audioSource.Play();
    }

}
