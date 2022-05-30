using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityController))]
public class CarriageController : MonoBehaviour
{
    [NonSerialized]
    public GameObject FolowingObject;
    public GravityController gravity;
    private TrainController trainHead;
    void Start()
    {

    }

    void Awake()
    {
        gravity = GetComponent<GravityController>();
    }

    void Update()
    {
        if (FolowingObject != null)
        {
            Vector3 direction = FolowingObject.transform.position - transform.position;
            Vector3 folowingForce = direction.normalized *  Mathf.Pow(direction.magnitude, 2) - direction.normalized * trainHead.CarriageDistance;
            // objRigidbody.AddForce(folowingForce);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarriagePickupArea" && FolowingObject == null)
        {
            trainHead = other.transform.GetComponentInParent<TrainController>();
            trainHead.AddCarriage(this);
        }
    }

    public void SetFree()
    {
        FolowingObject = null;
    }

    public void SetFolowingObject(GameObject following)
    {
        FolowingObject = following;
    }

}
