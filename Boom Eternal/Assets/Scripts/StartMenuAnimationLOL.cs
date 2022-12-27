using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuAnimationLOL : MonoBehaviour
{
    [SerializeField] Image[] panels;
    float y1, x2, y3;
    
    // Start is called before the first frame update
    void Awake(){
        y1 = panels[0].rectTransform.localPosition.y;
        x2 = panels[1].rectTransform.localPosition.x;
        y3 = panels[2].rectTransform.localPosition.y;
    }
    void Start()
    {
        panels[0].rectTransform.localPosition += new Vector3(0, 5000f, 0);
        panels[1].rectTransform.localPosition += new Vector3(5000f, 0, 0);
        panels[2].rectTransform.localPosition += new Vector3(0, -5000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(panels[0].rectTransform.localPosition.y >= y1){
            panels[0].rectTransform.localPosition += Vector3.down * Time.deltaTime * 5000f;
        }
        else if(panels[1].rectTransform.localPosition.x >= x2){
            panels[1].rectTransform.localPosition += Vector3.left * Time.deltaTime * 5000f;
        }
        else if(panels[2].rectTransform.localPosition.y <= y3){
            panels[2].rectTransform.localPosition += Vector3.up * Time.deltaTime * 5000f;
        }
        
    }
}
