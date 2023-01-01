using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour1 : MonoBehaviour
{
    // siia tuleb lihtsaim enemy behaviour: liigub ringi suvaliselt mängija ümber ja laseb suvaliste intervallide taga 3 kuuli
    bool lineOfSight = false, shooting = false;
    public float detectionRadius = 15f, shootingRadius = 7f, shootingDuration = 2f, detonationRadius = 3f;
    public int numberOfRicochets = 16; 
    public float shootingSlowDown = 0.5f; // aeglustus laskmise ja sihtimise ajal
    public float reloadingDuration = 2f;
    public int bulletsPerMagazine = 12;
    int bulletCounter = 0;
    float counter = 0;    
    bool isRunning = false;
    [SerializeField] LayerMask layerMask;

    [SerializeField]
    float maxBehaviourTime = 4f, speed = 2f; //phmst max aeg, mille jooksul ta teeb midagi (4 sekundit jooksu suvalises suunas, 4 sekundit seistes tulistamist, 4 sekundit ns passimist jms)
    [SerializeField] bool isGrenade = false;
    //animeeritud relva parameetrid:
    [SerializeField] Transform gunPivot, gunBarrel; 
    [SerializeField] bool spinningGun = false, clamped = true;
    [SerializeField] float maxAngle, minAngle;
    Rigidbody2D rb;
    GameObject player;
    Vector2 enemyToPlayerVector; 
    // knockback dampening:
    [HideInInspector] public Vector2 additionalVelocity = Vector2.zero;
    [SerializeField] float additionalVelocityDampeningRate = 0.5f;
    
    void Start()
    {
        player = GlobalReferences.thePlayer;
        if(gunBarrel == null){
            gunBarrel = transform;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyToPlayerVector = player.transform.position - transform.position;
        // tulistamine
        if(enemyToPlayerVector.magnitude < shootingRadius  && !isGrenade){
            if (lineOfSight && counter < Time.time)
            {
                Debug.Log("tulistan");
                GameObject bullet = Instantiate(GlobalReferences.enemyBulletPrefabs[0], gunBarrel.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = enemyToPlayerVector.normalized * 10f;
                bullet.GetComponent<Bullet>().affectsTarget = "Player";
                bulletCounter += 1;
                counter = Time.time + shootingDuration;

                GlobalReferences.audioManager.playSound("enemyShoot");

                if(bulletCounter == bulletsPerMagazine){
                    counter = Time.time + reloadingDuration;
                    bulletCounter = 0;
                    shooting = false;
                    //reloading = true; //kui seda kunagi vaja läheb
                }
                else{
                    
                    shooting = true;
                }
            }
            else
            {
                //Random.Range(0f, maxBehaviourTime);
                //Debug.Log("ei tulista ja ideaalis jooksen mängija vaatesse");
                //shooting = false;

            }
        }
        else {
            shooting = false;
        }

        if (lineOfSight)
        {
            if(shooting || !isGrenade){
                //transform.Translate(enemyToPlayerVector.normalized * shootingSlowDown * speed * Time.deltaTime);
                rb.velocity = enemyToPlayerVector.normalized * shootingSlowDown * speed + additionalVelocity;
            }
            else if(enemyToPlayerVector.magnitude < detectionRadius){
                rb.velocity = enemyToPlayerVector.normalized * speed + additionalVelocity;
            }
            if(enemyToPlayerVector.magnitude < detonationRadius && isGrenade){
                Detonate(numberOfRicochets);
            }
        }
        else{
            rb.velocity = additionalVelocity;
        }
        

        // additionalVelocity on KnockBackVelocity, mis sumbub ajas
        //Debug.Log("AdditionalVelocity" + additionalVelocity);
        if(additionalVelocity.magnitude > 0.1f)
            additionalVelocity *= (1 - additionalVelocityDampeningRate * Time.deltaTime);
        else
            additionalVelocity = Vector2.zero;

        if(spinningGun){
            float angle = Mathf.Atan(enemyToPlayerVector.y/enemyToPlayerVector.x)*Mathf.Rad2Deg;
            if(clamped){
                angle = Mathf.Clamp(angle, minAngle, maxAngle);
            }
            gunPivot.transform.rotation = Quaternion.Euler(0,0,angle);
        }
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyToPlayerVector.normalized, Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position, enemyToPlayerVector, Color.red);
        Debug.DrawRay(transform.position, enemyToPlayerVector.normalized * shootingRadius, Color.blue);
        Debug.DrawRay(transform.position, enemyToPlayerVector.normalized * detectionRadius, Color.yellow);
        Debug.DrawRay(transform.position, enemyToPlayerVector.normalized * detonationRadius, Color.red);
        if(hit.transform != null)
        {
            if (hit.transform.tag == "Player")
            {
                lineOfSight = true;
            }
            else
            {
                lineOfSight = false;
            }
            //Debug.Log("lineOfSight: " + lineOfSight);
            //Debug.Log(hit.transform.tag + " " + transform.position);
        }
    }
    public void Detonate(int numberOfRicochets){
        for(int i = 0; i < numberOfRicochets; i++){
            float angle = 360f / numberOfRicochets;
            GameObject bullet = Instantiate(GlobalReferences.enemyBulletPrefabs[0], transform.position, Quaternion.identity);
            Vector2 directionVector = new Vector2(Mathf.Cos((i * angle) * Mathf.Deg2Rad), Mathf.Sin((i * angle) * Mathf.Deg2Rad));
            bullet.GetComponent<Rigidbody2D>().velocity = directionVector * 10f;
            bullet.GetComponent<Bullet>().affectsTarget = "Player";
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle * i);
        }
        Destroy(gameObject);
        GlobalReferences.audioManager.playSound("enemyDetonate");
    }
}