using System;
using UnityEngine;

public class PlayerHurtBox : BaseHurtBox 
{
    private GravityController gravity;
    private HitPoints hitPoints;
    private SphereCollider hurtBox;

    public Action HurtHandlerAction;
    void Awake()
    {
        hurtBox = GetComponent<SphereCollider>();
    }

    void Start()
    {
        try
        {
            gravity = GetComponentInParent<GravityController>();
            hitPoints = GetComponentInParent<HitPoints>();
        }
        catch (NullReferenceException ex)
        {
            Debug.LogWarning("Не удалось инициализировать HurtBox персонажа!\n" + ex.Message);

            transform.parent.gameObject.AddComponent<GravityController>();
            transform.parent.gameObject.AddComponent<HitPoints>();

            gravity = GetComponentInParent<GravityController>();
            hitPoints = GetComponentInParent<HitPoints>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox") {
            HurtHandler();
        }
    }
    
    // public override void HurtHandler() {
    //     gravity.Knockback();
    // }
}
