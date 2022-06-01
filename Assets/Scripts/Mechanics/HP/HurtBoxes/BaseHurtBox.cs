using System;
using UnityEngine;

public abstract class BaseHurtBox : MonoBehaviour {
    
    public Action HurtHandler;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "HitBox") {
            HurtHandler?.Invoke();
        }
    }
}