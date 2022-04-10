using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour
{
    private Rigidbody rb;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider other) {
        if(other.tag == "Surface") {
            float distance = (transform.position - other.transform.position).magnitude;
            rb.AddForce(0, 40 / distance, 0);
        }
        
    }
}
