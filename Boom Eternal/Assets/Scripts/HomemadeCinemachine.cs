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
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos2D = aimingAndShooting.aimingVector * aimingAndShooting.aimingProgress * maxDisplacementRadius;
        cameraPos2D += new Vector2(player.transform.position.x, player.transform.position.y);
        //transform.position = new Vector3(cameraPos2D.x, cameraPos2D.y, -10); ASEMEL:
        //kinda cranky implementation, pole kindel kas teeb smoothimaks, aga fps-i eriti ei vähendanud, vist
        Vector3 newPos;
        newPos.x = Mathf.SmoothStep(transform.position.x, cameraPos2D.x , Time.deltaTime * 7000f);
        newPos.y = Mathf.SmoothStep(transform.position.y, cameraPos2D.y , Time.deltaTime * 7000f);
        newPos.z = -10f;
        transform.position = newPos;
        
    }
}
