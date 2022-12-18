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
    float sensX, sensY;
    public GameObject testBulletPrefab;
    public Transform gunBarrel;
    private Vector3 crosshairPosition, centerOfTheCanvas = Vector3.zero; 

    [HideInInspector]
    public Vector2 aimingVector; // on ühikvektor, mis näitab, mis suunas sihitakse
    [HideInInspector]
    public float aimingProgress; // näitab, mis murdosa maksimaalsest keskpunkti-crosshairi nihkest on praegu saavutatud

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        crosshairPosition = crosshair.rectTransform.position;

        resolutionX = GlobalReferences.mainCamera.pixelWidth;
        resolutionY = GlobalReferences.mainCamera.pixelHeight;

        //Debug.Log(resolutionX);
        //Debug.Log(resolutionY);    
        //Debug.Log(crosshairPosition);

        mouseX = centerOfTheCanvas.x = resolutionX * 0.5f;
        mouseY= centerOfTheCanvas.y = resolutionY * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //järgnev kood töötab, sest Canvas UI Scale Mode = Scale With Screen Size
        mouseX += sensX * Time.deltaTime * Input.GetAxis("Mouse X");
        mouseX = Mathf.Clamp(mouseX, 0, resolutionX);
        mouseY += sensY * Time.deltaTime * Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, 0, resolutionY);
        
        crosshairPosition.x = mouseX;
        crosshairPosition.y = mouseY;
        crosshair.rectTransform.position = crosshairPosition;
        
        Vector2 playerPosOnCanvas = GlobalReferences.mainCamera.WorldToScreenPoint(transform.position);       
        //kui vaja kasutada crosshairi reaalset positisooni:
        //Vector3 crosshairRealPosition = GlobalReferences.mainCamera.ScreenToWorldPoint(crosshair.rectTransform.position);
        
        aimingVector = new Vector2(crosshairPosition.x - playerPosOnCanvas.x, crosshairPosition.y - playerPosOnCanvas.y).normalized;
        aimingProgress = (crosshairPosition - centerOfTheCanvas).magnitude/(centerOfTheCanvas.magnitude); 
        //Debug.Log(aimingProgress);

        // tulistamise osa:
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            GameObject bullet = Instantiate(testBulletPrefab, gunBarrel.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = aimingVector * 12f;
        }
    }
}
