using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
// actually should be called GeneralLevelSequence
public class ThirdLevelSequence : MonoBehaviour
{ 
    [SerializeField] Transform[] setsOfEnemies;
    [SerializeField] DoorRotate[] doors;
    [SerializeField] Transform lastDoor;
    [SerializeField] float lastDoorDetectionRadius = 1f;
    /*// Exclusive to Second Scene:
    float initialIntensity = 0.85f, finalIntensity = 0.5f;
    Light2D globalLight; Color finalColor;*/
    
    void Update ()
    {
        for(int i = 0; i < setsOfEnemies.Length; i++ ){
            if(setsOfEnemies[i].childCount == 0){
                doors[i].open = true;
                if(i == setsOfEnemies.Length - 1)
                    GlobalReferences.enemiesLeft = 0;
            }
            else {
                GlobalReferences.enemiesLeft = setsOfEnemies[i].childCount;
                break;
            }
        }
        if((GlobalReferences.thePlayer.transform.position - lastDoor.transform.position).magnitude < lastDoorDetectionRadius)
            GetComponent<SceneReferencer>().LoadNextScene();
        Debug.DrawRay(lastDoor.position, Vector3.down*lastDoorDetectionRadius, Color.blue);

        if(GlobalReferences.hp <= 0)
            GetComponent<SceneReferencer>().RestartScene();
    }
}
