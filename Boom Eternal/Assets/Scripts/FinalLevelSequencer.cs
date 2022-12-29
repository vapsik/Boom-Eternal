using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelSequencer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake(){
        //for debugging:
        GlobalReferences.hp = GlobalReferences.hp == 0 ? 15 : GlobalReferences.hp;
        GlobalReferences.bulletCount = GlobalReferences.bulletCount == 0 ? 20 : GlobalReferences.bulletCount;
        GlobalReferences.maxBulletCount = GlobalReferences.maxBulletCount == 0 ? 20 : GlobalReferences.maxBulletCount;
        GlobalReferences.maxHP = GlobalReferences.maxHP == 0 ? 15 : GlobalReferences.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
