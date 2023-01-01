using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    // muudetud enemybehaviour 1; minigun boss
    bool lineOfSight = false, shooting = false;
    public float detectionRadius = 30f, shootingRadius = 10f, shootingDuration = 0.1f, detonationRadius = 3f;
    public int numberOfRicochets = 16;
    public float shootingSlowDown = 0.5f; // aeglustus laskmise ja sihtimise ajal
    float counter = 0;
    int fury, currentFury = 0;


    private Vector3 homePosition;

    [SerializeField] LayerMask layerMask;

    [SerializeField]
<<<<<<< HEAD
    float speed = 2f; //phmst max aeg, mille jooksul ta teeb midagi (4 sekundit jooksu suvalises suunas, 4 sekundit seistes tulistamist, 4 sekundit ns passimist jms)
    [SerializeField] bool moving = true;
=======
    float speed = 2f; 
    [SerializeField] bool canMove = true;
>>>>>>> 979d154 (updated boss)
    [SerializeField] bool canShoot = true;
    //animeeritud relva parameetrid:
    [SerializeField] Transform gunPivot, gunBarrel;
    [SerializeField] bool spinningGun = false, clamped = true;
    [SerializeField] float maxAngle, minAngle;

    GameObject player;
    Vector2 enemyToPlayerVector, homeVector;
    
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        homePosition = transform.position; //koht kus ta spawnitakse
        player = GlobalReferences.thePlayer;
        if (gunBarrel == null)
        {
            gunBarrel = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemyToPlayerVector = player.transform.position - transform.position;
        homeVector = homePosition - transform.position;
<<<<<<< HEAD
        if (homeVector.magnitude < 4f)
=======
        if (!canMove)
        {
            if (counter + Time.deltaTime < Time.time)
            {
                Detonate(numberOfRicochets + fury);
                fury++;
                counter = Time.time;
            }
            if (currentFury <= fury)
            {
                canMove = true;
            }
        }

        if (homeVector.magnitude < 1f && canMove)
>>>>>>> 979d154 (updated boss)
        {
            canShoot = true;
        }

        if (enemyToPlayerVector.magnitude < shootingRadius && canShoot)
        {
            // kordam��da:
            // kui lineOfSight = true, siis tulistab kuni crashib seina, siis ootab kuni algusesse j�uab
            if (lineOfSight && counter < Time.time)
            {
                //Debug.Log("tulistan");
                GameObject bullet = Instantiate(GlobalReferences.enemyBulletPrefabs[0], gunBarrel.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = enemyToPlayerVector.normalized * 12f;
                bullet.GetComponent<Bullet>().affectsTarget = "Player";
                counter = Time.time + shootingDuration;
                shooting = true;

                Vector3 posDifference = transform.position - GlobalReferences.thePlayer.transform.position;
                float distanceSquared = posDifference.x * posDifference.x + posDifference.y * posDifference.y;
                float volume = 8 - Mathf.Sqrt(distanceSquared);
                if (volume > 0)
                    GlobalReferences.audioManager.playSound("enemyShoot", volume, 1);

            }
            else
            {
                //Random.Range(0f, maxBehaviourTime);
                Debug.Log("ei tulista ja liigun tagasi algusesse");
                shooting = false;
            }
        }
        else
        {
            shooting = false;
        }

        if (canMove)
        {
            if (shooting)
            {
                transform.Translate(enemyToPlayerVector.normalized * shootingSlowDown * speed * -1f * Time.deltaTime) ; //recoil pushes back
            }
            else if (homeVector.magnitude > 3f) //pole kodus
            {
                rb.velocity = homeVector.normalized * speed; //liigub algusesse
            }

        }

        if (spinningGun)
        {
            float angle = Mathf.Atan(enemyToPlayerVector.y / enemyToPlayerVector.x) * Mathf.Rad2Deg;
            if (clamped)
            {
                angle = Mathf.Clamp(angle, minAngle, maxAngle);
            }
            gunPivot.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyToPlayerVector.normalized, Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position, enemyToPlayerVector, Color.red);
        Debug.DrawRay(transform.position, enemyToPlayerVector.normalized * shootingRadius, Color.blue);
        Debug.DrawRay(transform.position, enemyToPlayerVector.normalized * detectionRadius, Color.yellow);
        Debug.DrawRay(transform.position, enemyToPlayerVector.normalized * detonationRadius, Color.red);
        if (hit.transform != null)
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

    private void OnCollisionEnter2D(Collision2D other) //crashib seina ja hakkab reset'ima
    {
        if (other.transform.CompareTag("Wall")) 
        {
            canShoot = false;
            shooting = false;
            canMove = false;
            counter = Time.time + 3/(3 + fury);
            currentFury = fury + 3;
        }
    }
    public void Detonate(int numberOfRicochets)
    {
        for (int i = 0; i < numberOfRicochets; i++)
        {
            float angle = 360f / numberOfRicochets;
            GameObject bullet = Instantiate(GlobalReferences.enemyBulletPrefabs[0], transform.position, Quaternion.identity);
            Vector2 directionVector = new Vector2(Mathf.Cos((i * angle) * Mathf.Deg2Rad), Mathf.Sin((i * angle) * Mathf.Deg2Rad));
            bullet.GetComponent<Rigidbody2D>().velocity = directionVector * 10f;
            bullet.GetComponent<Bullet>().affectsTarget = "Player";
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle * i);
        }
    }
}
