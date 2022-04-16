using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("Alternative collision detection");
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Alternative trigger detection");
    }
}
