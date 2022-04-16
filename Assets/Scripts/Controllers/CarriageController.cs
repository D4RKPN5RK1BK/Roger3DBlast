using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour
{

    private GameObject folowingObject;
    private Rigidbody objRigidbody;
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

    void Awake() {
        Debug.Log("Carriage object was successefuly intialized!");
    }

    void Update()
    {
        if (folowingObject != null) {
            Debug.Log("folowing object exists");
            Vector3 folowingForce = folowingObject.transform.position - transform.position;
            objRigidbody.AddForce(folowingForce);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Carriage entered trigger area: " + other.tag);
        if (other.tag == "CarriagePickupArea" && folowingObject == null)
        {
            folowingObject = other.gameObject;
        }
    }

    public void SetFree()
    {
        folowingObject = null;
    }

}
