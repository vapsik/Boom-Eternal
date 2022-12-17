using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomemadeCinemachine : MonoBehaviour
{
    AimingAndShooting aimingAndShooting;
    public GameObject player;
    [SerializeField]
    private float maxDisplacementRadius = 10f;
    private Vector2 cameraPos2D;
    // Start is called before the first frame update
    void Awake(){
        GlobalReferences.mainCamera = gameObject.GetComponent<Camera>();
    }
    void Start()
    {
        aimingAndShooting = player.GetComponent<AimingAndShooting>();    
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos2D = aimingAndShooting.aimingVector * aimingAndShooting.aimingProgress * maxDisplacementRadius;
        cameraPos2D += new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = new Vector3(cameraPos2D.x, cameraPos2D.y, -10);
    }
}
