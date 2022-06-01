using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    private GravityController gravity;



    void Start()
    {
        gravity = GetComponentInParent<GravityController>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Surface")
        {
            float distance = (transform.position - other.ClosestPoint(transform.position)).magnitude;
            gravity.Float(20 / distance * Time.deltaTime);
        }

    }
}
