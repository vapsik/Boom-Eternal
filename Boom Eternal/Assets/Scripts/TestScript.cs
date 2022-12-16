using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public GameObject vastasePrefab1;
    // Kui sa seda loed sa oled lol

    // Start is called before the first frame update
    void Awake() {
        if(vastasePrefab1 != null)
            GlobalReferences.enemyPrefab1 = vastasePrefab1;    
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
