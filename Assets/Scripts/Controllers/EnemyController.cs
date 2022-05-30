using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(HitPoints))]
public class EnemyController : MonoBehaviour
{
    public BaseRouting Routing;
    private GravityController gravity;
    private Action behaviour;
    private HurtBox hurtBox;


    void Awake()
    {
        gravity = GetComponent<GravityController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (Routing != null)
        {
            behaviour += Roaming;
        }

        try
        {
            hurtBox = GetComponentInChildren<HurtBox>();
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Контроллер противников не смог обнаружить дочерный элемент HurtBox:\n" + ex.Message);
        }

    }

    // Update is called once per frame
    void Update()
    {
        behaviour?.Invoke();
    }

    void Roaming()
    {
        Vector3 dest = Routing.CurrentTarget - transform.position;
        if (dest.magnitude < 1)
        {
            Routing.NextTarget();
        }
        gravity.Walk(Vector3.ProjectOnPlane(dest.normalized, Vector3.up));

    }

    void OnDamageTake()
    {
        Debug.Log("Противник получил урон");
    }
}
