using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;
    private CharacterController controller;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        try
        {
            controller = transform.GetComponentInParent<CharacterController>();
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Ошибка при инициализции анимации персонажа: " + ex.Message);
            gameObject.AddComponent<CharacterController>();
            controller = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        animator.SetBool("PlayerJump", !controller.isGrounded);
        animator.SetFloat("PlayerSpeed", controller.velocity.magnitude);
    }
}
