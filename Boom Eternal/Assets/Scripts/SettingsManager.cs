using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider sensSlider, volumeSlider;
    [SerializeField] Transform audioManager;

    void Awake(){
        //et algsed näidud ühtiksid slideri näiduga:
        sensSlider.value = GlobalReferences.Settings.sensitivity;
        volumeSlider.value = GlobalReferences.Settings.volume;
        sensSlider.minValue = 500f;
        sensSlider.maxValue = 10000f;
        
    }
    public void OnChange(){
        GlobalReferences.Settings.sensitivity = sensSlider.value;
        GlobalReferences.Settings.volume = volumeSlider.value;

        AudioManager audioManager = GlobalReferences.audioManager;

        Sound menuMusic = audioManager.soundsDict["menuMusic"];
        menuMusic.audioSource.volume = GlobalReferences.Settings.volume * menuMusic.volume;

        Sound gameMusic = audioManager.soundsDict["gameMusic1"];
        menuMusic.audioSource.volume = GlobalReferences.Settings.volume * menuMusic.volume;
    }
    void Update(){
        //Debug.Log(GlobalReferences.Settings.sensitivity + " vs. " + sensSlider.value);
    }
}
