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
    public PoolController poolController;

    public int ParticiesCount = 0;

    private GravityController gravity;
    private Action behaviour;
    private BaseHurtBox hurtBox;
    private GameObject character;
    public float SecondsToWait;

    private bool wait;
    private float waitForTime;


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
            hurtBox = GetComponentInChildren<EnemyHurtBox>();
            hurtBox.HurtHandler += OnDamageTake;
            character = transform.Find("Character").gameObject;
            poolController = FindObjectOfType<PoolController>();
            // afterDeathParticies = FindObjectOfType<PoolController>();

        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Контроллер противников не смог обнаружить дочерный элемент:\n" + ex.Message);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (wait && waitForTime < Time.time)
            wait = false;


        behaviour?.Invoke();
    }

    void Roaming()
    {
        Vector3 dest = Routing.CurrentTarget - transform.position;
        if (dest.magnitude < 1)
        {
            Routing.NextTarget();
            wait = true;
            waitForTime = Time.time + SecondsToWait;
        }

        if (!wait)
            gravity.Walk(Vector3.ProjectOnPlane(dest.normalized, Vector3.up));

    }

    void OnDamageTake()
    {
        Debug.Log("Противник получил урон");
        character.SetActive(false);

        var particies = poolController.Take("RedCubeParticies", 20);
        foreach (var p in particies)
            p.GetComponent<ParticiresController>().Acitivate(transform.position, "RedCubeParticies");
    }

    public void Reset()
    {
        
    }
}
