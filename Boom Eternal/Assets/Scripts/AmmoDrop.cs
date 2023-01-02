using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float maxAmmoDropDuration = 20f;
    float timer = 0;
    bool intitated = false; 
    [SerializeField] public bool isHealthKitActually = false, expires = true;
    // kukkumine:
    [SerializeField] float fallingDuration = 0.3f;
    Vector2 initialPos, finalPos;
    bool falling = false;

    // Update is called once per frame
    void Update()
    {
        if(expires){
            if (gameObject != null && !intitated)
            {
                timer = Time.time + maxAmmoDropDuration;
                intitated = true;
            }
        if (timer < Time.time && intitated)
            {
                Destroy(gameObject);
            }
        }
        // ammoDrop tekib alguses initialPositionile (EnemyHP.transform.position) 
        // ja liigub uuele suvalisele asukohale, mille EnemyHP välja annab
        if (falling){
            transform.Translate(Time.deltaTime * (finalPos - initialPos).normalized * (finalPos - initialPos).magnitude / fallingDuration);
        }
        if((new Vector2 (transform.position.x, transform.position.y) - initialPos).magnitude 
        >= (finalPos - initialPos).magnitude){
            falling = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.transform.tag == "Player")
        {
            if(!isHealthKitActually){
                if(GlobalReferences.AddAmmo(false)){
                    Destroy(gameObject);
                }
            }
            else{
                if(GlobalReferences.AddHP(1, false)){
                    Destroy(gameObject);
                }
            }
        }
    }
    public void FallToNewPosition(Vector2 newPos){
        falling = true;
        finalPos = newPos;
        initialPos = transform.position;
    }
}