using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class TorchDistanceOptimizer : MonoBehaviour
{
    bool close = false, closeLastFrame = true;
    void Update()
    {
        if((transform.position - GlobalReferences.thePlayer.transform.position).magnitude > 12f){
            close = false;
        }
        else {
            close = true;
        }
        if(closeLastFrame != close)
            SwitchOnOrOff();
        closeLastFrame = close;
    }
    void SwitchOnOrOff(){
        foreach(Transform T in transform){
            T.gameObject.SetActive(!T.gameObject.activeSelf);
        }
    }
}
