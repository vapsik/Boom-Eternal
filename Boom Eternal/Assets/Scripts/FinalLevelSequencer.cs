using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelSequencer : MonoBehaviour
{

    //kui n vastast on tapetud tekib boss, aga jätkub random spawnimine (väiksema rate'iga), sest meie boss nii intensiivne ka pole;
    [SerializeField] int n = 20;
    [HideInInspector] public int killCount = 0;
   
    [SerializeField] GameObject enemySpawnerObject;
    EnemySpawner enemySpawner;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Transform startPoint;
    bool started = false, bossFightHasStarted = false;
    public static bool bossIsAlive = false;

    [SerializeField] Transform christmasTreesParent;
    void Start()
    {
        enemySpawner = enemySpawnerObject.GetComponent<EnemySpawner>();
        enemySpawner.spawnRandomly = false;
    }
    void Update()
    {   // optimaalsem kui &&?
        if(!started){
            if(GlobalReferences.thePlayer.transform.position.y > startPoint.position.y){
                started = true;
                bossPrefab.SetActive(false);
            }
        }
        if(started){
            enemySpawner.spawnRandomly = true;
            killCount = enemySpawner.numberOfSpawned 
            - enemySpawnerObject.transform.childCount;
            if(killCount >= n && !bossFightHasStarted){
                enemySpawner.minSpawnInterval = 3f;
                enemySpawner.maxSpawnInterval = 4f;

                bossPrefab.SetActive(true);
                bossFightHasStarted = true;
                bossIsAlive = true;
                  
            }
            if(bossFightHasStarted){
                if(bossPrefab == null){
                    bossIsAlive = false;
                }
            }
        }
        GlobalReferences.enemiesLeft = killCount;

        foreach(Transform T in christmasTreesParent){
            if(T.childCount == 0){
                ChristmasTree c = T.gameObject.GetComponent<ChristmasTree>();
                c.started = c.pleaseCheck == true ? true : c.started;
            }
        }
    }
}
