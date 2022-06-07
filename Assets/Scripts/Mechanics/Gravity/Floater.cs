using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [Min(0)]
    public float Force = 1;
    private GravityController gravity;
    private SphereCollider floatCollider;

    void Awake()
    {
        floatCollider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        gravity = GetComponentInParent<GravityController>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Surface")
        {
            float distance = (transform.position - other.ClosestPoint(transform.position)).magnitude;
            float finalForce = ((floatCollider.radius - distance) / floatCollider.radius) * Force;
            gravity.Float(finalForce * Time.deltaTime);
        }

    }
}
