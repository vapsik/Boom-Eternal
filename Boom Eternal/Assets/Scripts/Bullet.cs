using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string affectsTarget;
    int damage = 1;
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
        {
            Destroy(gameObject);
        }
        if (other.transform.tag == affectsTarget)
        {
            if (affectsTarget == "Player")
            {
                GlobalReferences.hp -= 1;
                Destroy(gameObject);
                //proc slowdown ja invuln l?bi PlayerResources
            }
            if (affectsTarget == "Enemy")
            {
                other.GetComponent<EnemyHP>().hp -= damage;
                other.GetComponent<EnemyHP>().OnDamageTaken();

                Destroy(gameObject);
            }
        }
    }
}