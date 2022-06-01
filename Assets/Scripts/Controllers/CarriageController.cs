using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(GravityController))]
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
        gravity = GetComponentInParent<GravityController>();
    }

    void Update()
    {
        if (FolowingObject != null)
        {
            Vector3 target = FolowingObject.transform.position - transform.position;
            Vector3 targetProjection = Vector3.ProjectOnPlane(target, Vector3.up);
            // target = target - target.normalized * trainHead.CarriageDistance;
            if (trainHead.CarriageDistance < targetProjection.magnitude)
                gravity.Walk(targetProjection.normalized);
            // Vector3 direction = FolowingObject.transform.position - transform.position;
            // Vector3 folowingForce = direction.normalized *  Mathf.Pow(direction.magnitude, 2) - direction.normalized * trainHead.CarriageDistance;
            // objRigidbody.AddForce(folowingForce);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarriagePickupArea" && FolowingObject == null)
        {
            trainHead = other.GetComponentInParent<TrainController>();
            gravity.WalkSpeed = other.GetComponentInParent<GravityController>().WalkSpeed;
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
