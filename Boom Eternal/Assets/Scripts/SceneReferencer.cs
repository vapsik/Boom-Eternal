using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
public class SceneReferencer : MonoBehaviour
// ma pole veel kindel selles, mida veel võiks see skript teha ja kas see võiks jääda esimeselt levelilt viitajaks või igal stseenil olla kasulik
{
    [SerializeField] GameObject ammoDropPrefab, healthKitPrefab;
    [SerializeField] GameObject[] listOfEnemyPrefabs;
    [SerializeField] bool iterateLights = false, lightsOff = true;
    [SerializeField] GameObject[] playerBulletPrefabs, enemyBulletPrefabs;
    [SerializeField] GameObject globalLight;
    void Awake(){
        GlobalReferences.ammoDropPrefab = ammoDropPrefab;
        GlobalReferences.listOfEnemyPrefabs = listOfEnemyPrefabs;
        GlobalReferences.playerBulletPrefabs = playerBulletPrefabs;
        GlobalReferences.enemyBulletPrefabs = enemyBulletPrefabs;
        GlobalReferences.currentSceneLight = globalLight;
        GlobalReferences.healthKitPrefab = healthKitPrefab;
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
