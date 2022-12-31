using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMovement : MonoBehaviour
{
    [SerializeField] Transform copyCat;

    // Update is called once per frame
    void Update()
    {
        copyCat.transform.rotation = gameObject.transform.rotation;
    }
}
