using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomemadeCinemachine : MonoBehaviour
{
    AimingAndShooting aimingAndShooting;
    GameObject player;
    //[SerializeField]
    private float maxDisplacementRadius = 2f;
    private Vector2 cameraPos2D;
    private Vector2 shakeOffset;

    [HideInInspector]
    public bool cameraShake;
    public float timeSinceShake;
    private const float shakeStartFade = 5f;
    private const float shakeEndFade = 5f;
    private const float shakeTime = 13f;
    private const float intensity = 0.15f;

    void Awake(){
        GlobalReferences.mainCamera = gameObject.GetComponent<Camera>();
        cameraShake = false;
        timeSinceShake = 0f;
    }
    void Start()
    {
        player = GlobalReferences.thePlayer;
        aimingAndShooting = player.GetComponent<AimingAndShooting>();    
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        player = GlobalReferences.thePlayer;
        cameraPos2D = aimingAndShooting.aimingVector * aimingAndShooting.aimingProgress * maxDisplacementRadius;
        cameraPos2D += new Vector2(player.transform.position.x, player.transform.position.y) + shakeOffset;
        transform.position = new Vector3(cameraPos2D.x, cameraPos2D.y, -10); //ASEMEL:
        //kinda clunky implementation, pole kindel kas teeb smoothimaks, aga fps-i eriti ei vähendanud, vist
        /*Vector3 newPos;
        newPos.x = Mathf.SmoothStep(transform.position.x, cameraPos2D.x , 3f);
        newPos.y = Mathf.SmoothStep(transform.position.y, cameraPos2D.y , 3f);
        newPos.z = -10f;
        transform.position = newPos;
        //*/
    }

    private void FixedUpdate()
    {
        if (!cameraShake)
        {
            shakeOffset = Vector2.zero;
            return;
        }

        if (timeSinceShake == 0f)
            GlobalReferences.audioManager.playSound("earthquake");

        float localIntensity;
        timeSinceShake += Time.fixedDeltaTime;
        
        if (timeSinceShake < shakeStartFade)
        {
            localIntensity = timeSinceShake / shakeStartFade;
        }
        else if (timeSinceShake < shakeTime - shakeEndFade)
        {
            localIntensity = 1f;
        }
        else if (timeSinceShake < shakeTime)
        {
            localIntensity = (shakeTime - timeSinceShake) / shakeEndFade;
        }
        else
        {
            localIntensity = 0f;
            cameraShake = false;
        }

        float x = UnityEngine.Random.Range(-intensity, intensity);
        float y = UnityEngine.Random.Range(-intensity, intensity);
        shakeOffset = new Vector2(x * localIntensity, y * localIntensity);

    }

}
