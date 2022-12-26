using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float inputX;
    float inputY;

    public float speed;
    Rigidbody2D rb;
    //leaping stuff:
    [HideInInspector] public bool isDodgeLeaping = false;
    Vector2 dodgeDirection;
    [SerializeField] float dodgeDuration = 0.5f;
    float dodgeTimer = 0;
    AimingAndShooting aimingAndShooting;
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        GlobalReferences.thePlayer = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        aimingAndShooting = GetComponent<AimingAndShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && rb.velocity.magnitude != 0){
            isDodgeLeaping = true;
            //disainiküsimus: kas dodgeDirection on sihtimise suund või viimatise liikumise suund? 
            //dodgeDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            dodgeDirection = aimingAndShooting.aimingVector;
            dodgeTimer = Time.time + dodgeDuration;
            // invincibility peale
        }
    }
    
    void FixedUpdate() {
        if(!isDodgeLeaping){
            // fikseeritud max kiirusega
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(inputX, inputY).normalized * speed;
        }
        else{
            if(dodgeTimer > Time.time){
                rb.velocity = 10f * dodgeDirection;
            }
            else{
                rb.velocity = Vector2.zero;
                isDodgeLeaping = false;
                // invincibility maha
            }
        }
    }
}