using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Dictionary<string, Sound> soundsDict = new Dictionary<string, Sound>();

    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroyOnLoad(this);
        GlobalReferences.audioManager = this;
        foreach(Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.playOnAwake = false;
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            soundsDict.Add(sound.name, sound);
        }
        foreach(string key in soundsDict.Keys)
        {
            Debug.Log(key);
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
