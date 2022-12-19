using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float maxBulletDuration = 10f;
    float timer = 0;
    bool intitated = false;
    void Update()
    {
        if (gameObject != null && !intitated)
        {
            timer = Time.time + maxBulletDuration;
            intitated = true;
        }
        if (timer < Time.time && intitated)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Wall" || other.transform.tag == "Ceiling")
            Destroy(gameObject);
    }
}