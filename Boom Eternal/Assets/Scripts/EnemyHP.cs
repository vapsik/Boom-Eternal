using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int hp;
    public float spawnDistance;
    public int maxDropAmount;
    GameObject testAmmoDropPrefab, healthKitPrefab;
    public float knockBackForce = 2;
    void Start(){
        testAmmoDropPrefab = GlobalReferences.ammoDropPrefab;
        healthKitPrefab = GlobalReferences.healthKitPrefab;
    }
    public void OnDamageTaken(Vector2 bulletVelocity) //siia saab knockbacki ka lisada
    {
        Vector2 knockBack = knockBackForce * bulletVelocity.normalized;
        if(gameObject.GetComponent<EnemyBehaviour1>() != null)
            gameObject.GetComponent<EnemyBehaviour1>().additionalVelocity += knockBack;
        // kui on veel mingi skript, mis liikumist mõjutab lisada sinna additionalVelocity ja siia viide 
        // siis tuleb teha eelnevalt näidatud null exception if-lausega
        
        if (hp <= 0)
        {
            //loot drops
            GameObject player = GlobalReferences.thePlayer;
            float distancefromplayer = Vector2.Distance(gameObject.transform.position, player.transform.position);
            float deltaDistance = spawnDistance - distancefromplayer;
            float dropAmount = Mathf.Round(maxDropAmount * Mathf.Pow(deltaDistance / (spawnDistance * 0.75f), 2) + Random.Range(-0.5f, 1.5f));
            for (int i = 0; i < Mathf.Min(dropAmount, maxDropAmount); i++)
            {
                //instantiate ammo drop
                GameObject ammoDrop = Instantiate(testAmmoDropPrefab, new Vector2(gameObject.transform.position.x
                + Random.Range(-1.5f, 1.5f), gameObject.transform.position.y + Random.Range(-1.5f, 1.5f)), Quaternion.identity);
            }
            // praegu ei teki rohkem kui 1 healthdrop per kill
            if(Random.Range(0f,100f)<((maxDropAmount+GlobalReferences.killsSinceHealthDrop)* GlobalReferences.maxHP /GlobalReferences.hp))
            {
                GameObject hpAdd = Instantiate(healthKitPrefab, new Vector2(gameObject.transform.position.x
                + Random.Range(-1.5f, 1.5f), gameObject.transform.position.y + Random.Range(-1.5f, 1.5f)), Quaternion.identity);
                GlobalReferences.killsSinceHealthDrop = 0;
            }
            else
            {
                GlobalReferences.killsSinceHealthDrop++;
            }
            GlobalReferences.score += maxDropAmount;
            Destroy(gameObject);
        }
    }
}