using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelSequence : MonoBehaviour
{
    [SerializeField] Transform lastDoor;
    [SerializeField] Transform[] setsOfEnemies;
    [SerializeField] DoorRotate[] doors;
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
        if((lastDoor.position - GlobalReferences.thePlayer.transform.position).magnitude <= 1f){
            GetComponent<SceneReferencer>().LoadNextScene();
        }
    }
}
