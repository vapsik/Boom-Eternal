using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
public class SceneReferencer : MonoBehaviour
// ma pole veel kindel selles, mida veel võiks see skript teha ja kas see võiks jääda esimeselt levelilt viitajaks või igal stseenil olla kasulik
{
    [SerializeField] GameObject ammoDropPrefab;
    [SerializeField] GameObject[] listOfEnemyPrefabs;
    [SerializeField] bool ligthsOff = true;
    void Awake(){
        GlobalReferences.ammoDropPrefab = ammoDropPrefab;
        GlobalReferences.listOfEnemyPrefabs = listOfEnemyPrefabs;
    }
    void Start(){
        if(ligthsOff){
            foreach(var el in listOfEnemyPrefabs){
                el.GetComponent<Light2D>().enabled = false;
            }
        }
    }
}
