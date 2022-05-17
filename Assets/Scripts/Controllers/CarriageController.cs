using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour
{
    [NonSerialized]
    public GameObject FolowingObject;
    private Rigidbody objRigidbody;
    private TrainController trainHead;
    void Start()
    {
        try
        {
            objRigidbody = transform.GetComponentInParent<Rigidbody>();
        }
        catch (NullReferenceException ex)
        {
            Debug.LogWarning(ex.Message);
            transform.gameObject.AddComponent<Rigidbody>();
            objRigidbody = GetComponent<Rigidbody>();
        }
    }

    void Awake()
    {
        // Debug.Log("Carriage object was successefuly intialized!");
    }

    void Update()
    {
        if (FolowingObject != null)
        {
            Vector3 direction = FolowingObject.transform.position - transform.position;
            Vector3 folowingForce = direction.normalized *  Mathf.Pow(direction.magnitude, 2) - direction.normalized * trainHead.CarriageDistance;
            objRigidbody.AddForce(folowingForce);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CarriagePickupArea" && FolowingObject == null)
        {
            Debug.Log("train trigger enter");
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
