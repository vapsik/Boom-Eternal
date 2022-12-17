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
    private Vector3 crosshairPosition, centerOfTheCanvas = Vector3.zero; 

    public Vector2 aimingVector; // on ühikvektor, mis näitab, mis suunas sihitakse
    public float aimingProgress; // näitab, mis murdosa maksimaalsest keskpunkti-crosshairi nihkest on praegu saavutatud

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        crosshairPosition = crosshair.rectTransform.position;

        resolutionX = GlobalReferences.mainCamera.pixelWidth;
        resolutionY = GlobalReferences.mainCamera.pixelHeight;

        Debug.Log(resolutionX);
        Debug.Log(resolutionY);    
        Debug.Log(crosshairPosition);

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
        
        aimingVector = new Vector2(crosshairPosition.x - centerOfTheCanvas.x, crosshairPosition.y - centerOfTheCanvas.y).normalized; 
        
        aimingProgress = (crosshairPosition - centerOfTheCanvas).magnitude/(centerOfTheCanvas.magnitude); 
    }
}
