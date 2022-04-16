using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    private Rigidbody parentRigidbody;
    
    void Start() {
        try {
            parentRigidbody = transform.parent.GetComponent<Rigidbody>();
        }
        catch(NullReferenceException ex) {
            Debug.LogWarning(ex);
            transform.parent.gameObject.AddComponent<Rigidbody>();
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.tag == "Surface") {
            float distance = (transform.position - other.transform.position).magnitude;
            parentRigidbody.AddForce(0, 60 / distance, 0);
        }
        
    }

    void OnTriggerEnter(Collider other) {
       Debug.Log("FloaterTriggerEnter");
        
    }

    void ForceUpward() {

    }

}
