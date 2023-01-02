using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class FlameFlicker : MonoBehaviour
{
    [SerializeField] Light2D light2D;
    float[] initialValue = {0, 0};
    float w;
    // Start is called before the first frame update
    void Start()
    {
        w = Random.Range(0.1f,1f);
    }

    // Update is called once per frame
    void Update()
    {   if(initialValue[0] == 0 && initialValue[1] == 0){
            initialValue[0] = GetComponent<Light2D>().pointLightInnerRadius;
            initialValue[1] = GetComponent<Light2D>().pointLightOuterRadius;
            
        }
        float k = Mathf.Sin(Time.time * 15f * w);
        GetComponent<Light2D>().pointLightInnerRadius = (1+ 0.01f * k) * initialValue[0];
        GetComponent<Light2D>().pointLightOuterRadius = (1+ 0.01f * k) * initialValue[1];
    }
}
