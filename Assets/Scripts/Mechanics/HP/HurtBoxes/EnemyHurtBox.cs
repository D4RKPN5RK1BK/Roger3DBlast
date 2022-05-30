using System;
using UnityEngine;

public class EnemyHurtBox : BaseHurtBox
{
    private GravityController gravity;
    private HitPoints hitPoints;
    private SphereCollider hurtBox;
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

    void Update()
    { 
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBox") {
            Debug.Log("Противник получает демедж");
            HurtHandler();
        }

        Debug.Log("Enemy Trigger");
    }
    
    public override void HurtHandler() {
        // Debug.Log($"ОБъект {this.gameObject.name} получил урон");
        gravity.Knockback();
    }
}