using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float maxAmmoDropDuration = 20f;
    float timer = 0;
    bool intitated = false;

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null && !intitated)
        {
            timer = Time.time + maxAmmoDropDuration;
            intitated = true;
        }
        if (timer < Time.time && intitated)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if(GlobalReferences.AddAmmo(false)){
                Destroy(gameObject);
            }
        }
    }
}