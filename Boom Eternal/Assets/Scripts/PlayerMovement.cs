using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float inputX;
    float inputY;
    float leapingCounter = 0; bool startedCountingLeapTime = false;
    public float leapingCoolDown = 4f;
    // jääl kõndimise asjandus:
    bool onIce = false; Vector2 lastFrameInput; float inputDelay = 0.05f, inputDelayCounter; List<Vector2> savedInputs; int i = 0;
    public float speed;
    Rigidbody2D rb;
    //leaping stuff:
    
    [HideInInspector] public bool isDodgeLeaping = false;
    Vector2 dodgeDirection;
    [SerializeField] float dodgeDuration = 0.5f;
    float dodgeTimer = 0;
    AimingAndShooting aimingAndShooting;

    private const float slowdownTime = 1.5f;
    private const float slowdownEndSmoothTime = 0.5f;
    private float timeSinceSlowdown = 1000000f;
    private const float slowDown = 0.3f;

    [HideInInspector]
    public float timeSinceDamage = 100000f;
    private SpriteRenderer spriteRenderer;

    void Awake() {
        Time.timeScale = 1f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        GlobalReferences.thePlayer = gameObject;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        aimingAndShooting = GetComponent<AimingAndShooting>();
    }
    public void OnDamageTaken()
    {
        GlobalReferences.thePlayerIsInvincible = true;
        timeSinceSlowdown = 0f;
        //GlobalReferences.score -= 1; me mby tahame seda

    }
    // Update is called once per frame
    void Update()
    {
        timeSinceSlowdown += Time.unscaledDeltaTime;

        GlobalReferences.thePlayerIsInvincible = (isDodgeLeaping || timeSinceSlowdown < slowdownTime);

        if (timeSinceSlowdown >= slowdownTime)
        {
            Time.timeScale = 1f;
        }
        else if (timeSinceSlowdown >= slowdownTime - slowdownEndSmoothTime)
        {
            float timeScaling = (slowdownTime - timeSinceSlowdown) / slowdownEndSmoothTime;
            Time.timeScale = slowDown + (1f - slowDown) * (1f - timeScaling);
        } else
        {
            Time.timeScale = slowDown;
        }
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        // Kusti kood
        /*if (timeSinceSlowdown < slowdownTime && !UIManager.onPause)
        {
            Time.timeScale += 1f / 3f * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if (Time.timeScale == 1)
            {
                timeSlowed = false;
            }
        }*/

        if (Input.GetKeyDown(KeyCode.Mouse1) && rb.velocity.magnitude != 0 && GlobalReferences.leapCount > 0){
            isDodgeLeaping = true;
            //disainiküsimus: kas dodgeDirection on sihtimise suund või viimatise liikumise suund? 
            //dodgeDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            dodgeDirection = aimingAndShooting.aimingVector;
            dodgeTimer = Time.time + dodgeDuration;
            // invincibility peale
            GlobalReferences.thePlayerIsInvincible = true;
            GlobalReferences.leapCount -= 1;
            GlobalReferences.audioManager.playSound("playerLeap");
        }

        if(GlobalReferences.leapCount < 3 && !startedCountingLeapTime){
            startedCountingLeapTime = true;
            leapingCounter = Time.time + leapingCoolDown;
        }
        if(startedCountingLeapTime && leapingCounter < Time.time){
            GlobalReferences.leapCount += 1;
            startedCountingLeapTime = false;
        }
        if(GlobalReferences.leapCount == 3){
            
            startedCountingLeapTime = false;
        }

        timeSinceDamage += Time.deltaTime;

        float damageTintTime = 0.6f;
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
    void FixedUpdate() {
        
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 movementInput = new Vector2(inputX, inputY);
        if(!isDodgeLeaping){
            // fikseeritud max kiirusega
            rb.velocity = movementInput.normalized * speed;
        }
        if(isDodgeLeaping){
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
        //Debug.Log("on ice = " + onIce);
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