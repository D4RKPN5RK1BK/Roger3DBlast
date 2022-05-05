using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GravityController
{
    private CharacterController controller;

    public float FallSpeed = 1;
    public float GravityPressure = 1;
    [Min(0)]
    public float PlayerJumpForce = 10;
    public float WalkSpeed = 1;
    [Range(0, 1)]
    public float JumpControll = 1;
    public float JumpStrnght = 10;
    [Range(0, 1)]
    public float Drag = 0.1f;
    private Vector3 inertion = Vector3.zero;

    public GravityController(CharacterController controller) {
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

    public void Jump()
    {
        if (controller.isGrounded)
            inertion += Vector3.up * PlayerJumpForce;
    }


}
