using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Класс предназначен для имитации движения персонажей и расчитан как на нпс
 *  так и на прямой контроль через чтнеие инпутов.
 * 
 *  Реализует базовые методы по типу ходьба, бег, прыжок.
 **/

[RequireComponent(typeof(CharacterController))]
public class GravityController : MonoBehaviour
{
    private CharacterController controller;
    private GameObject character;

    private float jumpStartTime;

    [Min(0)]
    public float JumpForce;

    public float RotationSpeed;

    [Min(0)]
    public float JumpContinueTime;

    public float JumpContinueForce;

    [Min(0)]
    public float JumpControl;

    [Min(0)]
    public float FallSpeed;

    [Min(0)]
    public float GravityPressure;

    [Min(0)]
    public float WalkSpeed;

    [Range(0, 1)]
    public float Drag;

    [Range(0, 1)]
    public float JumpDrag;

    private Vector3 inertion;

    private Vector3 jumpInertion;

    private Vector3 fallInertion;

    private Vector3 moveInertion;

    private Vector3 _motion;

    void Awake()
    {
        jumpInertion = Vector3.zero;
        fallInertion = Vector3.zero;
        _motion = Vector3.zero;
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        try
        {
            character = transform.Find("Character").gameObject;
        }
        catch (NullReferenceException ex)
        {
            Debug.LogWarning("Не удалось найти дочерний объек Character!\n" + ex.Message);
            GameObject temp = Instantiate(new GameObject("Character"));
            temp.transform.SetParent(transform.parent);
            transform.SetParent(temp.transform);
        }
    }

    void Update()
    {

        if (controller.isGrounded)
        {
            fallInertion += Vector3.down * Time.deltaTime;
        }
        else
        {
            fallInertion += Vector3.down * Time.deltaTime * FallSpeed;
        }
        controller.Move(jumpInertion + fallInertion + moveInertion);


        moveInertion -= moveInertion * Drag;
        fallInertion -= fallInertion * Drag;
        jumpInertion -= jumpInertion * JumpDrag;
    }

    public void SetController(CharacterController controller)
    {
        this.controller = controller;

    }

    public void Walk(Vector2 motion)
    {
        Vector3 projectedMotion = new Vector3(motion.x, 0, motion.y);
        if (Vector2.zero != motion)
            character.transform.forward = Vector3.RotateTowards(character.transform.forward, projectedMotion, Time.deltaTime * RotationSpeed, 0).normalized;

        _motion = projectedMotion;
        moveInertion += _motion * Time.deltaTime * WalkSpeed;
    }

    public void Walk(Vector3 motion)
    {
        _motion = motion;
        moveInertion += _motion * Time.deltaTime * WalkSpeed;
        if (Vector3.zero != motion)
            character.transform.forward = Vector3.RotateTowards(character.transform.forward, motion, Time.deltaTime * RotationSpeed, 0).normalized;
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            jumpStartTime = Time.time;
            jumpInertion += Vector3.up * JumpForce + _motion * moveInertion.magnitude;
        }
    }

    public void ContinueJump()
    {
        if (jumpStartTime + JumpContinueTime > Time.time)
            jumpInertion += Vector3.up * JumpContinueForce * Time.deltaTime;
    }

    public void Knockback() {
        jumpInertion = (Vector3.up -_motion) * JumpForce  ;
    }

    public void Knockback(Vector3 direction) {
        jumpInertion = Vector3.up * JumpForce + direction * moveInertion.magnitude;
    }

    public void Float(float streght) {
        jumpInertion += Vector3.up * streght * Time.deltaTime;
    }
}
