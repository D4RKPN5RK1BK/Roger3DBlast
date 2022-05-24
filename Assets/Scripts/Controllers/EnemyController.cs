using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(HitPoints))]
public class EnemyController : MonoBehaviour
{
    public BaseRouting AI;
    private GravityController gravity;
    private Action behaviour;

    void Awake() {
        gravity = GetComponent<GravityController>();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (AI != null) {
            behaviour += Roaming;
        }
    }

    // Update is called once per frame
    void Update()
    {
        behaviour?.Invoke();
    }

    void Roaming() {
        gravity.Walk(Vector3.ProjectOnPlane((AI.CurrentTarget - transform.position).normalized, Vector3.up));
    }
}
