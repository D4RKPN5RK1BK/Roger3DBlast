using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
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
        Debug.Log("Primary collision detection");
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Primary trigger detection");
    }
}
