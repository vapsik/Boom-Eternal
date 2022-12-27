using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimingAndShooting : MonoBehaviour
{
    private float mouseX, mouseY;
    private int resolutionX, resolutionY;
    [SerializeField]
    Image crosshair;
    [SerializeField]
    float sensX, sensY, reach; // reach ehk siruulatus
    public GameObject testBulletPrefab;
    public Transform gunBarrel;
    public GameObject gunPivot;
    public Transform playerBullets;
    private Vector3 crosshairPosition, centerOfTheCanvas = Vector3.zero; 

    [HideInInspector]
    public Vector2 aimingVector, aimingVectorFromBarrel; // on ühikvektor, mis näitab, mis suunas sihitakse
    [HideInInspector]
    public float aimingProgress; // näitab, mis murdosa maksimaalsest keskpunkti-crosshairi nihkest on praegu saavutatud

    [SerializeField] bool spinningGunOnHand = true;

    // Start is called before the first frame update
    void Start()
    {
        GlobalReferences.bulletCount = GlobalReferences.maxBulletCount;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        crosshairPosition = crosshair.rectTransform.position;

        resolutionX = GlobalReferences.mainCamera.pixelWidth;
        resolutionY = GlobalReferences.mainCamera.pixelHeight;

        mouseX = centerOfTheCanvas.x = resolutionX * 0.5f;
        mouseY = centerOfTheCanvas.y = resolutionY * 0.5f;
        //väike offset algusesse:
        mouseX += 50f;
        mouseY += 50f;
    }

    // Update is called once per frame
    void Update()
    {
        sensX = sensY = GlobalReferences.Settings.sensitivity;

        //järgnev kood töötab, sest Canvas UI Scale Mode = Scale With Screen Size
        mouseX += sensX * Time.deltaTime * Input.GetAxis("Mouse X");
        mouseX = Mathf.Clamp(mouseX, 8f, resolutionX - 8f);
        mouseY += sensY * Time.deltaTime * Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, 8f, resolutionY - 8f);
        
        crosshairPosition.x = mouseX;
        crosshairPosition.y = mouseY;
        crosshair.rectTransform.position = crosshairPosition;
        
        Vector2 playerPosOnCanvas = GlobalReferences.mainCamera.WorldToScreenPoint(transform.position);
        Vector2 barrelPosOnCanvas = GlobalReferences.mainCamera.WorldToScreenPoint(gunBarrel.position);
        //kui vaja kasutada crosshairi reaalset positisooni:
        Vector3 crosshairRealPosition = GlobalReferences.mainCamera.ScreenToWorldPoint(crosshair.rectTransform.position);
        
        aimingVector = new Vector2(crosshairPosition.x - playerPosOnCanvas.x, crosshairPosition.y - playerPosOnCanvas.y).normalized;
        aimingProgress = (crosshairPosition - centerOfTheCanvas).magnitude/(centerOfTheCanvas.magnitude); 
        
        if(spinningGunOnHand){
            //gun.transform.LookAt(crosshairRealPosition, Vector3.back);
            gunPivot.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan(aimingVector.y/aimingVector.x)*Mathf.Rad2Deg);
        }
        
        //tulistamise osa:
        if(Input.GetKeyDown(KeyCode.Mouse0) && GlobalReferences.bulletCount > 0 && !GlobalReferences.onPause){
            aimingVectorFromBarrel = new Vector2(crosshairPosition.x - barrelPosOnCanvas.x, crosshairPosition.y - barrelPosOnCanvas.y).normalized;
            //selline instantiate'imine toimib ainult puhul ümara kuuli puhul: 
            GameObject bullet = Instantiate(testBulletPrefab, gunBarrel.position, Quaternion.identity, playerBullets.transform);
            bullet.GetComponent<Rigidbody2D>().velocity = aimingVectorFromBarrel * 12f;
            ////kuulikujulise kuuli puhul:
            //GameObject bullet = Instantiate(testBulletPrefab, gunBarrel.position, Quaternion.identity);
            //bullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan(aimingVector.y/aimingVector.x)*Mathf.Rad2Deg);
            bullet.GetComponent<Bullet>().affectsTarget = "Enemy"; //iseendale dammi ei tee

            GlobalReferences.bulletCount -= 1;
        }
    }
}