using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSystem : MonoBehaviour
{
    bool lookLeft = true;
    AimingAndShooting aimingAndShooting;
    Rigidbody2D rb;

    [SerializeField]
    private GameObject gunObject;

    private Vector3 playerScale;
    private float initialXScale;

    // Start is called before the first frame update
    void Start()
    {
        aimingAndShooting = gameObject.GetComponent<AimingAndShooting>();

        rb = gameObject.GetComponent<Rigidbody2D>(); // läheb hiljem vaja animatsioonide juhtimisel
    
        playerScale = transform.localScale;
        initialXScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //idk see ei tööta smh:
        //Vector2 lookDir = 10f * aimingDirection + new Vector2(transform.position.x, transform.position.y);
        


        //vasakule-paremale scaling
        if(aimingAndShooting.aimingVector.x > 0){
            lookLeft = false;
        }
        if (aimingAndShooting.aimingVector.x < 0){
            lookLeft = true;
        }

        if(lookLeft){
            playerScale.x = initialXScale;
        }
        else{
            playerScale.x = -initialXScale;
        }
        
        transform.localScale = playerScale;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5f);
        Gizmos.color = Color.red;
    }
}
