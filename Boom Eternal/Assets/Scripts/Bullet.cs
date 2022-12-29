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
        if (other.CompareTag("Wall")) //  || other.CompareTag("Ceiling") -- avastasin, et pole teinud sellist tagiaaaw
        {
            //Debug.Log(transform.position + " vasta seina ");
            Destroy(gameObject);
        }
        if (other.CompareTag(affectsTarget))
        {
            if (affectsTarget == "Player")
            {
                if(!GlobalReferences.thePlayerIsInvincible){
                    GlobalReferences.hp -= 1;
                }
                //eat lead, töötab ka :
                GlobalReferences.AddAmmo(true);
                Destroy(gameObject);
                //proc slowdown ja invuln l?bi PlayerResources
            }
            if (affectsTarget == "Enemy")
            {
                other.GetComponent<EnemyHP>().hp -= damage;
                other.GetComponent<EnemyHP>().OnDamageTaken(GetComponent<Rigidbody2D>().velocity);

                Destroy(gameObject);
            }
        }
    }
}