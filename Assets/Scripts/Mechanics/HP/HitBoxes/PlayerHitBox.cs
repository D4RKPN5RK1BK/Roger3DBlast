using System;
using UnityEngine;

public class PlayerHitBox : BaseHitBox
{

    private Collider[] hitColliders;
    public Action DelegateHitHandler;

    void Awake()
    {
        hitColliders = GetComponents<Collider>();
        foreach (var c in hitColliders)
        {
            c.enabled = false;
        }
    }

    public override void StartAtack()
    {
        foreach (var c in hitColliders)
        {
            c.enabled = true;
        }

    }

    public override void EndAtack()
    {
        foreach (var c in hitColliders)
        {
            c.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag =="HurtBox") {
            DelegateHitHandler?.Invoke();
        }
    }


    public override void HitHandler()
    {
        throw new System.NotImplementedException();
    }
}