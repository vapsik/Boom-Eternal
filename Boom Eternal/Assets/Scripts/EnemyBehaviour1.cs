using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour1 : MonoBehaviour
{
    // siia tuleb lihtsaim enemy behaviour: liigub ringi suvaliselt mängija ümber ja laseb suvaliste intervallide taga 3 kuuli
    bool lineOfSight;
    public float detectionRadius;    
    float counter = 0;
    float time;
    bool isRunning = false;

    [SerializeField] LayerMask layerMask;

    [SerializeField]
    float maxBehaviourTime = 4f, speed = 2f; //phmst max aeg, mille jooksul ta teeb midagi (4 sekundit jooksu suvalises suunas, 4 sekundit seistes tulistamist, 4 sekundit ns passimist jms)
    [SerializeField]
    bool monkeMode = true;
    
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
        if (monkeMode && lineOfSight)
        {
            transform.Translate(enemyToPlayerVector.normalized * speed * Time.deltaTime);
        }

        /*if(enemyToPlayerVector.magnitude < detectionRadius){
            // kordamööda:
            // kui lineOfSight = true, siis tulistab Random(0, maxBehaviourTime) aja kestel mängijat
            if (lineOfSight)
            {
                Debug.Log("tulistan");
            }
            else
            {
                //Random.Range(0f, maxBehaviourTime);
                Debug.Log("jooksen suunas");

            }
            // kui laskmine läbi või lineOfSight = false jookseb suvalises suunas mängija 
        }*/

        // praegu ei arvesta see sellega, et vastasel endal ka collider:
        RaycastHit2D hit = Physics2D.Raycast(transform.position, enemyToPlayerVector.normalized, Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position, enemyToPlayerVector.normalized, Color.red);
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
            Debug.Log(hit.transform.tag);
        }
    }
}