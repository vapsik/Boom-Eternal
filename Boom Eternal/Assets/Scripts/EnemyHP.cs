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

    private float timeSinceDamage = 100000f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

        timeSinceDamage = 0f;
        
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
                Vector2 newPos = new Vector2(gameObject.transform.position.x
                + Random.Range(-1.5f, 1.5f), gameObject.transform.position.y + Random.Range(-1.5f, 1.5f));
                int failSafe = 0;
                while(!GlobalReferences.CheckTile(newPos)){
                    failSafe++;
                    if(failSafe > 1000)
                        break;
                    newPos = new Vector2(gameObject.transform.position.x
                + Random.Range(-1.5f, 1.5f), gameObject.transform.position.y + Random.Range(-1.5f, 1.5f));
                }

                GameObject ammoDrop = Instantiate(testAmmoDropPrefab, transform.position, Quaternion.identity);
                ammoDrop.GetComponent<AmmoDrop>().FallToNewPosition(newPos);
            }
            // praegu ei teki rohkem kui 1 healthdrop per kill
            if(Random.Range(0f,100f)<((maxDropAmount+GlobalReferences.killsSinceHealthDrop)* 2*GlobalReferences.maxHP /GlobalReferences.hp))
            {
                Vector2 newPos = new Vector2(gameObject.transform.position.x
                + Random.Range(-1.5f, 1.5f), gameObject.transform.position.y + Random.Range(-1.5f, 1.5f));
                // selleks, et seina sisse ei tekiks dropid
                int failSafe = 0;
                while(!GlobalReferences.CheckTile(newPos)){
                    failSafe++;
                    if(failSafe > 1000)
                        break;
                    newPos = new Vector2(gameObject.transform.position.x
                + Random.Range(-1.5f, 1.5f), gameObject.transform.position.y + Random.Range(-1.5f, 1.5f));
                }
                GameObject hpAdd = Instantiate(healthKitPrefab, transform.position, Quaternion.identity);
                hpAdd.GetComponent<AmmoDrop>().FallToNewPosition(newPos);
                GlobalReferences.killsSinceHealthDrop = 0;
            }
            else
            {
                GlobalReferences.killsSinceHealthDrop++;
            }
            GlobalReferences.score += maxDropAmount;

            GlobalReferences.audioManager.playSound("enemyDeath" + Random.Range(1, 3));
            Destroy(gameObject);
        }
        else
        {
            GlobalReferences.audioManager.playSound("enemyDamage" + Random.Range(1, 3));
        }
    }

    public void Update()
    {
        timeSinceDamage += Time.deltaTime;

        float damageTintTime = 0.4f;
        if (timeSinceDamage >= damageTintTime)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            float redNess = 1f * (damageTintTime - timeSinceDamage);
            spriteRenderer.color = new Color(1f, 1f - redNess, 1f - redNess);
        }
    }

}