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
    int hpAtStart, bulletCountAtStart, maxBulletCountAtStart;
    [SerializeField] bool SantaHasCome = false;
    [SerializeField] Transform christmasTree;
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
            GlobalReferences.hp = GlobalReferences.hp == 0 ? GlobalReferences.maxHP : GlobalReferences.hp;
            GlobalReferences.bulletCount = GlobalReferences.bulletCount == 0 ? GlobalReferences.maxBulletCount : GlobalReferences.bulletCount;
        }
        GlobalReferences.currentSceneLight = globalLight;
    }
    void Update(){
        if(GlobalReferences.hp <= 0)
            RestartScene();
    }
    void Start(){
        
        hpAtStart = GlobalReferences.hp;
        bulletCountAtStart = GlobalReferences.bulletCount;
        maxBulletCountAtStart = GlobalReferences.maxBulletCount;

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
        
        //
        if(SantaHasCome){
            // siia tuleb ammo dropide ja health kitide spawnimine
        int m = GlobalReferences.maxHP/2 - GlobalReferences.hp > 0 ?
         GlobalReferences.maxHP/2 - GlobalReferences.hp : 1;
        int n = GlobalReferences.maxBulletCount/2 - GlobalReferences.bulletCount > 0 ? 
         GlobalReferences.maxBulletCount/2 - GlobalReferences.bulletCount : 3;
        
        // hp increment (+=2, sest healthkit annab praegu 2 juurde)
        for(int i = 0; i < m; i+=2){
            Vector2 newPos = new Vector2(christmasTree.position.x
                + Random.Range(-1.5f, 1.5f), christmasTree.position.y + Random.Range(-1.5f, 1.5f));
                // selleks, et seina sisse ei tekiks dropid:
            int failSafe = 0;
            while(!GlobalReferences.CheckTile(newPos)){
                failSafe++;
                if(failSafe > 1000)
                    break;
                    newPos = new Vector2(christmasTree.position.x
                + Random.Range(-1.5f, 1.5f), christmasTree.position.y + Random.Range(-1.5f, 1.5f));
            }
            GameObject hpAdd = Instantiate(GlobalReferences.healthKitPrefab, christmasTree.transform.position, Quaternion.identity, transform);
            hpAdd.GetComponent<AmmoDrop>().FallToNewPosition(newPos);
        }
        // 
        for(int i = 0; i < n; i++){
            Vector2 newPos = new Vector2(christmasTree.position.x
                + Random.Range(-1.5f, 1.5f), christmasTree.position.y + Random.Range(-1.5f, 1.5f));
                // selleks, et seina sisse ei tekiks dropid:
            int failSafe = 0;
            while(!GlobalReferences.CheckTile(newPos)){
                failSafe++;
                if(failSafe > 1000)
                    break;
                    newPos = new Vector2(christmasTree.position.x
                + Random.Range(-1.5f, 1.5f), christmasTree.position.y + Random.Range(-1.5f, 1.5f));
            }
            GameObject ammoDrop = Instantiate(GlobalReferences.ammoDropPrefab, christmasTree.transform.position, Quaternion.identity, transform);
            ammoDrop.GetComponent<AmmoDrop>().FallToNewPosition(newPos);
        }
        }
        
    }
    public void LoadNextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GlobalReferences.hp = hpAtStart;
        //GlobalReferences.hp = GlobalReferences.maxHP;
        GlobalReferences.maxBulletCount = maxBulletCountAtStart;
        GlobalReferences.bulletCount = GlobalReferences.maxBulletCount;
        GlobalReferences.bulletCount = bulletCountAtStart;
    }
}
