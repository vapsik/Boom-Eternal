using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float inputX;
    float inputY;

    public float speed;
    Rigidbody2D rb;
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        GlobalReferences.thePlayer = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void FixedUpdate() {
        //fikseeritud max kiirusega
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(inputX, inputY).normalized * speed;
    }
}