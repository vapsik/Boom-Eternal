using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
// actually should be called GeneralLevelSequence
public class FourthLevelSequence : MonoBehaviour
{ 
    [SerializeField] Transform[] setsOfEnemies;
    [SerializeField] DoorRotate[] doors;
    [SerializeField] Transform lastDoor;
    [SerializeField] float lastDoorDetectionRadius = 1f;
    // Exclusive to Third Scene:
    [SerializeField] Transform wayPoint1;
    
    void Update ()
    {
        if(GlobalReferences.thePlayer.transform.position.x > wayPoint1.position.x){
            doors[0].open = true;
        }

        for(int i = 0; i < setsOfEnemies.Length; i++ ){
            if(setsOfEnemies[i].childCount == 0){
                doors[i+1].open = true;
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
