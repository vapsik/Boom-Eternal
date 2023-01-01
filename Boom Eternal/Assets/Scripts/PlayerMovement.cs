using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float inputX;
    float inputY;
    // jääl kõndimise asjandus:
    bool onIce = false; Vector2 lastFrameInput; float inputDelay = 0.05f, inputDelayCounter; List<Vector2> savedInputs; int i = 0;
    public float speed;
    bool timeSlowed = false;
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

    public void OnDamageTaken()
    {
        GlobalReferences.thePlayerIsInvincible = true;
        Time.timeScale = 0.2f;
        timeSlowed = true;
        GlobalReferences.score -= 2;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        if(Input.GetKeyDown(KeyCode.Mouse1) && rb.velocity.magnitude != 0){
=======
        Debug.Log(Time.timeScale);
        if (timeSlowed && !UIManager.onPause)
        {
            Time.timeScale += (1f / 3f) * Time.unscaledDeltaTime;
=======
        // invincibility maha/peale
        GlobalReferences.thePlayerIsInvincible = (timeSlowed || isDodgeLeaping);

        if (timeSlowed && !UIManager.onPause)
        {
            Time.timeScale += 1 / 3f* Time.unscaledDeltaTime;
>>>>>>> Stashed changes
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if (Time.timeScale == 1)
            {
                timeSlowed = false;
<<<<<<< Updated upstream
                GlobalReferences.thePlayerIsInvincible = false;
            }
        }

=======
            }
        }
>>>>>>> Stashed changes
        if(Input.GetKeyDown(KeyCode.Mouse1) && rb.velocity.magnitude != 0 && GlobalReferences.leapCount > 0){
>>>>>>> Stashed changes
            isDodgeLeaping = true;
            //disainiküsimus: kas dodgeDirection on sihtimise suund või viimatise liikumise suund? 
            //dodgeDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            dodgeDirection = aimingAndShooting.aimingVector;
            dodgeTimer = Time.time + dodgeDuration;
            // invincibility peale
            GlobalReferences.thePlayerIsInvincible = true;
        }
    }
    
    void FixedUpdate() {
        
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 movementInput = new Vector2(inputX, inputY);
        if(!isDodgeLeaping && !onIce){
            // fikseeritud max kiirusega
            rb.velocity = movementInput.normalized * speed;
        }
        if(isDodgeLeaping && !onIce){
            if(dodgeTimer > Time.time){
                rb.velocity = 10f * dodgeDirection;
            }
            else{
                rb.velocity = Vector2.zero;
                isDodgeLeaping = false;
            }
        }
        // ei tea kas järgmise lõigu peaks panema Update-i???
        if(onIce){
            // jääle mineku algus:
            if(movementInput.magnitude != 0 && lastFrameInput == Vector2.zero){
                lastFrameInput = movementInput;
                inputDelayCounter = Time.time + inputDelay;
                savedInputs = new List<Vector2>();
            }
            else if (inputDelayCounter < Time.time && movementInput != lastFrameInput && movementInput != Vector2.zero){
                lastFrameInput = savedInputs[i] != Vector2.zero ? savedInputs[i] : lastFrameInput;
                i += 1;
            }
            rb.velocity = lastFrameInput.normalized * speed;
            savedInputs.Add(movementInput);
        }
        else if(lastFrameInput.magnitude > 0){
            lastFrameInput = Vector2.zero;
            savedInputs.Clear();
            i = 0;
        }
        Debug.Log("on ice = " + onIce);
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.transform.tag == "Ice"){
            onIce = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.tag == "Ice"){
            onIce = false;
        }
    }
}