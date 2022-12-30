using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;
public class SceneReferencer : MonoBehaviour
// ma pole veel kindel selles, mida veel võiks see skript teha ja kas see võiks jääda esimeselt levelilt viitajaks või igal stseenil olla kasulik
{
    [SerializeField] GameObject ammoDropPrefab, healthKitPrefab;
    [SerializeField] GameObject[] listOfEnemyPrefabs;
    [SerializeField] bool iterateLights = false, lightsOff = true;
    [SerializeField] GameObject[] playerBulletPrefabs, enemyBulletPrefabs;
    [SerializeField] GameObject globalLight;
    [SerializeField] Tilemap[] floorWallCeilingTiles;
    void Awake(){
        GlobalReferences.listOfEnemyPrefabs = listOfEnemyPrefabs;
        GlobalReferences.floorWallCeilingTiles= floorWallCeilingTiles;
        // tegelt jäägu samad prefabid: kõigile stseenidele:
        if(SceneManager.GetActiveScene().name == "FirstLevel"){
            GlobalReferences.ammoDropPrefab = ammoDropPrefab;
            GlobalReferences.healthKitPrefab = healthKitPrefab;
            GlobalReferences.playerBulletPrefabs = playerBulletPrefabs;
            GlobalReferences.enemyBulletPrefabs = enemyBulletPrefabs;

            // toon ka level sequence skriptidest bulletcount ja hp modifikatsioonid siia
            GlobalReferences.hp = GlobalReferences.maxHP;
            //GlobalReferences.maxHP = 15;
            GlobalReferences.bulletCount = GlobalReferences.maxBulletCount;
            //GlobalReferences.maxBulletCount = 20;
        }
        // for debugging:
        else{
            GlobalReferences.ammoDropPrefab = GlobalReferences.ammoDropPrefab == null ? ammoDropPrefab : GlobalReferences.ammoDropPrefab;
            GlobalReferences.healthKitPrefab = GlobalReferences.healthKitPrefab == null ? healthKitPrefab : GlobalReferences.healthKitPrefab;
            GlobalReferences.playerBulletPrefabs = GlobalReferences.playerBulletPrefabs == null ? playerBulletPrefabs : GlobalReferences.playerBulletPrefabs;
            GlobalReferences.enemyBulletPrefabs = GlobalReferences.enemyBulletPrefabs == null ? enemyBulletPrefabs : GlobalReferences.enemyBulletPrefabs;

            // toon ka level sequence skriptidest bulletcount ja hp modifikatsioonid siia
            GlobalReferences.hp = GlobalReferences.hp == 0 ? 15 : GlobalReferences.hp;
            GlobalReferences.bulletCount = GlobalReferences.bulletCount == 0 ? GlobalReferences.maxBulletCount : GlobalReferences.bulletCount;
        }
        GlobalReferences.currentSceneLight = globalLight;
    }
    void Update(){
        if(GlobalReferences.hp <= 0)
            RestartScene();
    }
    void Start(){
        if(iterateLights){
            if(lightsOff){
                foreach(var el in GlobalReferences.listOfEnemyPrefabs){
                    el.GetComponent<Light2D>().enabled = false;
                }
                foreach(var el in GlobalReferences.playerBulletPrefabs){
                    el.GetComponent<Light2D>().intensity = 0.05f;
                }
            }
            else{
                foreach(var el in GlobalReferences.listOfEnemyPrefabs){
                    el.GetComponent<Light2D>().enabled = true;
                }
                foreach(var el in GlobalReferences.playerBulletPrefabs){
                    el.GetComponent<Light2D>().intensity = 0.5f;
                }
            }
        }
        
    }
    public void LoadNextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
