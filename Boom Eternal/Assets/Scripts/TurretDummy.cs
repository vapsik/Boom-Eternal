using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDummy : MonoBehaviour
{
    // Start is called before the first frame update
    public float rateOfFire = 2f; //shots per second
    float timeSinceShot = 0f;
    public GameObject testBulletPrefab;

    // Update is called once per frame
    void Update()
    {
        timeSinceShot += Time.deltaTime;
        if (timeSinceShot >= 1 / rateOfFire)
        {
            GameObject bullet = Instantiate(testBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1f) * 12f;
            bullet.GetComponent<Bullet>().affectsTarget = "Player";
            timeSinceShot = 0f;
        }
    }
}
