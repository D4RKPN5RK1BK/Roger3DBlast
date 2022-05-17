using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GravityController : MonoBehaviour
{
    private CharacterController controller;

    private float jumpStartTime;

    [Min(0)]
    public float JumpForce;

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

    }

    void Upadte()
    {
        if (controller.isGrounded)
        {
            fallInertion = Vector3.zero;
        }
        else
        {
            fallInertion += Vector3.down * Time.deltaTime;
        }
        controller.Move(jumpInertion + fallInertion + moveInertion);


        inertion -= inertion * Drag;
        fallInertion -= fallInertion * Drag;
        jumpInertion -= jumpInertion * JumpDrag;
    }

    public void SetController(CharacterController controller)
    {
        this.controller = controller;

    }

    public void Move(Vector3 motion)
    {
        _motion = motion;
        moveInertion += _motion * Time.deltaTime * WalkSpeed;

    }

    public void Jump(float time)
    {
        if (controller.isGrounded)
        {
            jumpStartTime = time;
            jumpInertion += Vector3.up * JumpForce + _motion * moveInertion.magnitude;
        }
    }

    public void ContinueJump()
    {
        if (jumpStartTime + JumpContinueTime > Time.time)
            jumpInertion += Vector3.up * JumpContinueForce * Time.deltaTime;
    }
}
