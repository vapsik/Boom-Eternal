using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTint : MonoBehaviour
{

    Image uiImage;

    private void Awake()
    {
        uiImage = GetComponent<Image>();
        GlobalReferences.damageTint = this;
    }

    private void Update()
    {
        float timeSinceDamage = GlobalReferences.thePlayer.GetComponent<PlayerMovement>().timeSinceDamage;
        float damageTintTime = 0.4f;

        Color color = uiImage.color;
        if (timeSinceDamage >= damageTintTime)
        {
            color.a = 0;
        }
        else
        {
            color.a = damageTintTime - timeSinceDamage;
        }
        uiImage.color = color;
    }

}
