using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour1 : MonoBehaviour
{
    // siia tuleb lihtsaim enemy behaviour: liigub ringi suvaliselt mängija ümber ja laseb suvaliste intervallide taga 3 kuuli
    [SerializeField] GameObject bulletPrefab;
    bool lineOfSight = false, shooting = false;
    public float detectonRadius = 15f;
    public float shootingRadius = 7.5f;    
    public float shootingDuration = 2f;
    public float shootingSlowDown = 0.5f; // aeglustus laskmise ja sihtimise ajal
    public float reloadingDuration = 2f;
    public int bulletsPerMagazine = 12;
    int bulletCounter = 0;
    float counter = 0;    
    bool isRunning = false;

    [SerializeField] LayerMask layerMask;

    [SerializeField]
    float maxBehaviourTime = 4f, speed = 2f; //phmst max aeg, mille jooksul ta teeb midagi (4 sekundit jooksu suvalises suunas, 4 sekundit seistes tulistamist, 4 sekundit ns passimist jms)
    [SerializeField] bool monkeMode = true;
    [SerializeField] bool canShoot = false;
    
    GameObject player;
    Vector2 enemyToPlayerVector;

    
    void Start()
    {
        player = GlobalReferences.thePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        enemyToPlayerVector = player.transform.position - transform.position;
        if(enemyToPlayerVector.magnitude < shootingRadius && canShoot){
            // kordamööda:
            // kui lineOfSight = true, siis tulistab Random(0, maxBehaviourTime) aja kestel mängijat
            if (lineOfSight && counter < Time.time && enemyToPlayerVector.magnitude < detectonRadius)
            {
                
                Debug.Log("tulistan");
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = enemyToPlayerVector.normalized * 10f;
                bullet.GetComponent<Bullet>().affectsTarget = "Player";
                bulletCounter += 1;
                counter = Time.time + shootingDuration;

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
                Debug.Log("ei tulista ja ideaalis jooksen mängija vaatesse");
                //shooting = false;

            }
            // kui laskmine läbi või lineOfSight = false jookseb suvalises suunas mängija 
        }
        else {
            shooting = false;
        }
        

        if (monkeMode && lineOfSight)
        {
            if(shooting){
                transform.Translate(enemyToPlayerVector.normalized * shootingSlowDown * speed * Time.deltaTime);
            }
            else{
                transform.Translate(enemyToPlayerVector.normalized * speed * Time.deltaTime);
            }
        }

        // praegu ei arvesta see sellega, et vastasel endal ka collider:
        RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyToPlayerVector.normalized, Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position, enemyToPlayerVector, Color.red);
        Debug.DrawRay(transform.position, enemyToPlayerVector.normalized * shootingRadius, Color.blue);
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
            Debug.Log("lineOfSight: " + lineOfSight);
            Debug.Log(hit.transform.tag + " " + transform.position);
        }
    }
}