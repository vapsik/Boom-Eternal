using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSystem : MonoBehaviour
{
    [SerializeField] bool lookLeft = true;
    float pantingCounter = 0; //RIP Pengu
    AimingAndShooting aimingAndShooting;
    Rigidbody2D rb;

    [SerializeField]
    private GameObject gunObject; //kui on
    [SerializeField]
    Animator animator;

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

        //peaks selle osa (animaatori bash) vist hiljem pigem sisendi järgi tegema, sest jääl võiks tehniliselt ka hingeldada
        bool movementInput = Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 ? true : false;
        animator.SetBool("MovementInput", movementInput);
        if(rb.velocity.magnitude <= 0.1f)
            pantingCounter += Time.deltaTime;
        else
            pantingCounter = 0;
        animator.SetFloat("PantingCounter", pantingCounter);
    }
}
