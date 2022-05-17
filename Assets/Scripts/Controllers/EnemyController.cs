using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(HitPoints))]
public class EnemyController : MonoBehaviour
{
    public MonoBehaviour AI;
    private GravityController gravity;

    void Awake() {
        gravity = GetComponent<GravityController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
