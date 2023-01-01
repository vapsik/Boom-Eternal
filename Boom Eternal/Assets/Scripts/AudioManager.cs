using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Dictionary<string, Sound> soundsDict = new Dictionary<string, Sound>();
    public float masterVolume = 1f;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (GlobalReferences.audioManager == null)
        {
            GlobalReferences.audioManager = this;
        }
        else
        {
            Destroy(gameObject);
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
        PlayMusic();
    }

    private void PlayMusic()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu2")
        {
            playSound("menuMusic");
        }
        else
        {
            AudioSource menuMusicSource = soundsDict["menuMusic"].audioSource;
            menuMusicSource.Stop();

            Sound gameMusic = soundsDict["gameMusic1"];
            if (!gameMusic.audioSource.isPlaying)
            {
                gameMusic.audioSource.loop = true;
                playSound(gameMusic.name);
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
        sound.audioSource.volume = sound.volume * volumeChange * GlobalReferences.Settings.volume;
        sound.audioSource.pitch = sound.pitch * pitchChange;
        sound.audioSource.Play();
    }

}
