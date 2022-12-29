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
    
    void Awake(){
        //for debugging:
        GlobalReferences.hp = GlobalReferences.hp == 0 ? 15 : GlobalReferences.hp;
        GlobalReferences.bulletCount = GlobalReferences.bulletCount == 0 ? 20 : GlobalReferences.bulletCount;
        GlobalReferences.maxBulletCount = GlobalReferences.maxBulletCount == 0 ? 20 : GlobalReferences.maxBulletCount;
        GlobalReferences.maxHP = GlobalReferences.maxHP == 0 ? 15 : GlobalReferences.maxHP;
    }
    void Update ()
    {
        for(int i = 0; i < setsOfEnemies.Length; i++ ){
            if(setsOfEnemies[i].childCount == 0){
                doors[i].open = true;
            }
            else {
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
