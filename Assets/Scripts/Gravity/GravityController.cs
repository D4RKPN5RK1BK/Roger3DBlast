using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GravityController
{
    private CharacterController controller;

    private float jumpStartTime;

    [Min(0)]
    public float JumpForce = 1;

    [Min(0)]
    public float JumpContinueTime;

    public float JumpContinueForce;

    [Range(0, 1)]
    public float JumpControl = 1;

    [Min(0)]
    public float FallSpeed = 1;

    [Min(0)]
    public float GravityPressure = 1;

    [Min(0)]
    public float WalkSpeed = 1;

    [Range(0, 1)]
    public float Drag = 0.1f;

    private Vector3 inertion = Vector3.zero;

    public GravityController(CharacterController controller)
    {
        this.controller = controller;
    }

    public void Move(Vector3 motion)
    {

        if (controller.isGrounded)
        {
            inertion += motion * Time.deltaTime * WalkSpeed;
            inertion += Vector3.down * Time.deltaTime * GravityPressure;
        }
        else
        {
            inertion += motion * Time.deltaTime;
            inertion += Vector3.down * Time.deltaTime * FallSpeed;
        }

        controller.Move(inertion);


        inertion -= inertion * Drag;
    }

    public void Walk(Vector3 motion)
    {

    }

    public void Jump(float time)
    {
        if (controller.isGrounded)
        {
            jumpStartTime = time;
            inertion += Vector3.up * JumpForce;
        }
    }

    public void ContinueJump()
    {
        if (jumpStartTime + JumpContinueTime > Time.time)
            inertion += Vector3.up * JumpContinueForce * Time.deltaTime;
    }


}
