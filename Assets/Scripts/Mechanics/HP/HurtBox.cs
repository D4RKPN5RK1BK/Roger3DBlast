using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Хартбокс определяет ПОЛУЧЕНИЕ урона
 * Данный класс по сути нужен чтобы проверять хитбоксы
 * 
 **/

public class HurtBox : MonoBehaviour
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
        if (other.tag != "Surface" && other.tag != this.tag) {
            Debug.Log("Произошло HurtBox столкновение:\n Объект " + this.gameObject.name  + " столкнулся с " + other.gameObject.name);

            var comp = other.GetComponentInChildren<HitBox>();
            HurtHandler();
        }
        // Debug.Log("Произошло столкновение");
        

        // HurtHandler?.Invoke();
        // if (other.tag != "Surface")
        // {
        //     if (other.gameObject.TryGetComponent<HitBox>(out HitBox hitBox))
        //     {
        //         Debug.Log("Столкновение с хитбоксом");
        //         HurtHandler?.Invoke();
        //     }
        //     else
        //     {
        //         Debug.Log("Столкновение безразличных объектов");
        //     }
        // }

    }
    
    public void HurtHandler() {
        Debug.Log($"ОБъект {this.gameObject.name} получил урон");
        gravity.Knockback();
    }

}
