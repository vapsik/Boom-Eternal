using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdDoor : MonoBehaviour
{
    bool triggered = false;
    [SerializeField] Transform trigger;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!triggered){
            if(GlobalReferences.thePlayer.transform.position.y > trigger.transform.position.y)
                triggered = true;
        }

        if(triggered){
            GetComponent<Collider2D>().isTrigger = false;
        }
        animator.SetBool("Closes", triggered);
    }
}
