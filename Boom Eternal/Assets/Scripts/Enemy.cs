using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        System.Random random = new System.Random();
        spriteRenderer.color = new Color(random.Next(256), 0, 0);
    }
}