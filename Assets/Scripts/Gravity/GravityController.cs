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
    [SerializeField]
    public float JumpForce;

    [Min(0)]
    [SerializeField]
    public float JumpContinueTime;

    [SerializeField]
    public float JumpContinueForce;

    [Range(0, 1)]
    [SerializeField]
    public float JumpControl;

    [Min(0)]
    [SerializeField]
    public float FallSpeed;

    [Min(0)]
    [SerializeField]
    public float GravityPressure;

    [Min(0)]
    [SerializeField]
    public float WalkSpeed;

    [Range(0, 1)]
    [SerializeField]
    public float Drag;

    [Range(0, 1)]
    [SerializeField]
    public float JumpDrag;

    private Vector3 inertion = Vector3.zero;
    
    private Vector3 jumpInertion = Vector3.zero;

    private Vector3 fallInertion = Vector3.zero;

    public void SetController(CharacterController controller)
    {
        this.controller = controller;

    }
    
    public void Move(Vector3 motion)
    {

        if (controller.isGrounded)
        {
            inertion += motion * Time.deltaTime * WalkSpeed;
            inertion += Vector3.down * Time.deltaTime * GravityPressure;
            fallInertion = Vector3.zero;
        }
        else
        {
            inertion += motion * Time.deltaTime * WalkSpeed * JumpControl;
            inertion += Vector3.down * Time.deltaTime * FallSpeed;
            fallInertion += Vector3.down * Time.deltaTime;
        }

        inertion += jumpInertion;
        inertion += fallInertion.normalized * Mathf.Pow(fallInertion.magnitude, 3) * FallSpeed;

        controller.Move(inertion);


        inertion -= inertion * Drag;
        jumpInertion -= jumpInertion * JumpDrag;
    }

    public void Walk(Vector3 motion)
    {

    }

    public void Jump(float time)
    {
        if (controller.isGrounded)
        {
            jumpStartTime = time;
            jumpInertion += Vector3.up * JumpForce;
        }
    }

    public void ContinueJump()
    {
        if (jumpStartTime + JumpContinueTime > Time.time)
            jumpInertion += Vector3.up * JumpContinueForce * Time.deltaTime;
    }

    // public void OnBeforeSerialize()
    // {
    //     throw new NotImplementedException();
    // }

    // public void OnAfterDeserialize()
    // {
    //     throw new NotImplementedException();
    // }
}
