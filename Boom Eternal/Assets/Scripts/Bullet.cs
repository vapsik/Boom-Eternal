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
        if (other.CompareTag("Wall")) //  || other.CompareTag("Ceiling") -- avastasin, et pole teinud sellist tagi
        {
            Destroy(gameObject);
        }
        if (other.CompareTag(affectsTarget))
        {
            if (affectsTarget == "Player")
            {
                if(!GlobalReferences.thePlayerIsInvincible){
                    GlobalReferences.hp -= 1;
                    //proc slowdown
                }
                //eat lead, töötab ka :
                GlobalReferences.AddAmmo(true);
                Destroy(gameObject);
                
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