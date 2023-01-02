using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasTree : MonoBehaviour
{
    public bool started = false, pleaseCheck = true;
    float time, counter;

    List<GameObject> ammoAndHP = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ammoAndHP.Add(GlobalReferences.ammoDropPrefab);
        ammoAndHP.Add(GlobalReferences.healthKitPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if(started && pleaseCheck){
            float d = Random.Range(10f, 15f);
            time = d + Time.time;
            //Debug.Log(d);
            pleaseCheck = false;
        }
        //Debug.Log(time);
        if(started && time < Time.time && !pleaseCheck){
            Debug.Log("jõuluvana tuli!");
            //spawnimine
            int n = Random.Range(4,10);

            for (int i = 0; i < n; i++){
                
                Vector2 newPos = new Vector2(gameObject.transform.position.x
                + Random.Range(-1.5f, 1.5f), gameObject.transform.position.y + Random.Range(-1.5f, 1.5f));
                GameObject ammoOrHpDrop = Instantiate(ammoAndHP[Random.Range(1,4) == 3 ? 1 : 0], transform.position, Quaternion.identity, transform);
                ammoOrHpDrop.GetComponent<AmmoDrop>().FallToNewPosition(newPos);
                ammoOrHpDrop.GetComponent<AmmoDrop>().expires = false;
                ammoOrHpDrop.transform.localScale = Vector3.one;
            }

            pleaseCheck = true;
            started = false;
        }
    }
}
