using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
// actually should be called GeneralLevelSequence
public class SecondLevelSequence : MonoBehaviour
{ 
    [SerializeField] Transform[] setsOfEnemies;
    [SerializeField] DoorRotate[] doors;
    [SerializeField] Transform lastDoor;
    [SerializeField] float lastDoorDetectionRadius = 1f;
    // Exclusive to Second Scene:
    float initialIntensity = 0.85f, finalIntensity = 0.5f;
    Light2D globalLight; Color finalColor;
    
    // Start() currently only exclusive to Second Scene:
    void Start(){
        globalLight = GlobalReferences.currentSceneLight.GetComponent<Light2D>();
        finalColor = globalLight.color;
        globalLight.color = Color.white;
        globalLight.intensity = initialIntensity;
    }
    void Update ()
    {
        for(int i = 0; i < setsOfEnemies.Length; i++ ){
            if(setsOfEnemies[i].childCount == 0){
                doors[i].open = true;
            }
            else {
                GlobalReferences.enemiesLeft = setsOfEnemies[i].childCount;
                break;
            }
        }
        // light fade exlcusive to Second Scene:
        if(globalLight.intensity > finalIntensity){
            globalLight.intensity -= Time.deltaTime * 0.1f;
        }
        if(globalLight.color.r > finalColor.r){
            float r = globalLight.color.r;
            r -= Time.deltaTime * 0.1f;
            globalLight.color = new Color(r, globalLight.color.g, globalLight.color.b, 1f);
        }
        if(globalLight.color.g > finalColor.r){
            float g = globalLight.color.r;
            g -= Time.deltaTime * 0.1f;
            globalLight.color = new Color(globalLight.color.r, g, globalLight.color.b, 1f);
        }
        if(globalLight.color.b > finalColor.b){
            float b = globalLight.color.r;
            b -= Time.deltaTime * 0.1f;
            globalLight.color = new Color(globalLight.color.r, globalLight.color.g, b, 1f);
        }
        //last door action:
        if((lastDoor.position - GlobalReferences.thePlayer.transform.position).magnitude <= lastDoorDetectionRadius){
            GetComponent<SceneReferencer>().LoadNextScene();
        }
        Debug.DrawRay(lastDoor.position, Vector3.down*lastDoorDetectionRadius, Color.blue);
    }
}
