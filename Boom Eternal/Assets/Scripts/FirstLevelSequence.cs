using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelSequence : MonoBehaviour
{
    public Transform firstEnemies, secondEnemies, lastDoor;
    [SerializeField] DoorRotate door1, door2;
    // initialize some values:
    void Awake(){
        GlobalReferences.hp = 15;
        GlobalReferences.maxHP = 15;
        GlobalReferences.bulletCount = 20;
        GlobalReferences.maxBulletCount = 20;
    }    
    void Update ()
    {
        if(firstEnemies.childCount == 0){
            door1.open = true;
            secondEnemies.gameObject.SetActive(true);
        }
        if(secondEnemies.childCount == 0){
            door2.open = true;
        }
        if((lastDoor.position - GlobalReferences.thePlayer.transform.position).magnitude <= 1f){
            GetComponent<SceneReferencer>().LoadNextScene();
        }
    }
}
