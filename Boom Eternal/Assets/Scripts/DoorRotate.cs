using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public bool open;
    [SerializeField] GameObject children;
    [SerializeField] Animator animator;

    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(open){
           OpenSequence(); 
        }
    }
    void OpenSequence(){

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = false;
        
        animator.SetBool("Open", true);
        children.SetActive(false);   

    }

}
