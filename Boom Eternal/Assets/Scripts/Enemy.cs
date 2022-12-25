using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        /*spriteRenderer = GetComponent<SpriteRenderer>();
        System.Random random = new System.Random();
        spriteRenderer.color = new Color(random.Next(256) / 256, 0, 0);*/
    }

    /* j�rgnev koodil�ik on hea debuggimiseks aga ei konformeeru kusti hp s�steemiga
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == GlobalReferences.Tags.PlayerBullet)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
    */

}