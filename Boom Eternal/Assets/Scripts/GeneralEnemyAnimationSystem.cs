using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemyAnimationSystem : MonoBehaviour
{
    [SerializeField] bool lookLeft = true;
    [SerializeField] Animator animator;
    GameObject player;
    EnemyBehaviour1 enemyBehaviour1;

    Vector3 initialSize, currentScale;
    // Start is called before the first frame update
    void Start()
    {
        player = GlobalReferences.thePlayer;
        initialSize = transform.localScale;
        currentScale = initialSize;
        enemyBehaviour1 = GetComponent<EnemyBehaviour1>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > transform.position.x){
            lookLeft = false;
        }
        else{
            lookLeft = true;
        }

        if(lookLeft){
            currentScale.x = initialSize.x; 
        }
        else{
            currentScale.x = -initialSize.x;
        }
        transform.localScale = currentScale;
        if(animator != null){
            animator.SetBool("HasSeenEnemy", enemyBehaviour1.lineOfSight);
            animator.SetBool("Shooting", enemyBehaviour1.shooting);
        }
    }
}
