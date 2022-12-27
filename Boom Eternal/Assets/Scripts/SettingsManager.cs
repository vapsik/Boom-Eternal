using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider sensSlider, volumeSlider;

    void Awake(){
        //et algsed näidud ühtiksid slideri näiduga:
        sensSlider.value = GlobalReferences.Settings.sensitivity;
        volumeSlider.value = GlobalReferences.Settings.volume;
    }
    void Update(){
        GlobalReferences.Settings.sensitivity = sensSlider.value;
        GlobalReferences.Settings.volume = volumeSlider.value;
    }
}
