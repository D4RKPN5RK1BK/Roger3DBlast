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
            // Transform parentTransform = transform;
            // while (parentTransform != null || objRigidbody == null)
            // {
            //     if (parentTransform.tag == "Carriage")
            //     {
            //         objRigidbody = parentTransform.GetComponent<Rigidbody>();
            //     }
            //     parentTransform = parentTransform.parent;
            // }
            // if (objRigidbody == null)
            // {
            //     throw new NullReferenceException("Не найдены объекты с тегом \"Carriage\" которые бы содержали компонент \"Rigidbody\"");
            // }
            objRigidbody = transform.parent.GetComponent<Rigidbody>();
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
            trainHead = other.transform.parent.gameObject.GetComponent<TrainController>();
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
