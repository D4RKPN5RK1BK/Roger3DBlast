using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Хитбокс определяет НАНЕСЕНИЕ демеджа
 * Так что обработка получения демеджа должна быть тут 
 * 
 * Если вводить дополнительные теги по типу hitBox и hurtbox то 
 * скорее всего можно будет куда проще запутаться.
 * На данный момент лучше будет оставить player и enemy.
 * 
 **/

public class HitBox : MonoBehaviour
{

    private SphereCollider hitBox;


    void Awake()
    {
        hitBox = GetComponent<SphereCollider>();
    }


    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HurtBox") {
            Debug.Log("Произошло HitBox столкновение:\n Объект " + this.gameObject.name  + " столкнулся с " + other.gameObject.name);

            var comp = other.GetComponentInChildren<HurtBox>() ?? GetComponent<HurtBox>();
            if (comp != null) {
                Debug.Log("Произошел вызов обработчика получения урона");
                comp.HurtHandler();

            }
        }
    }
}
