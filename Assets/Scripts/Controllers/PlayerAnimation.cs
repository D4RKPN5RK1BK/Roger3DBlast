using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;
    private Rigidbody playerRigidbody;
    private CharacterController player;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterControllerScript>().GetComponent<CharacterController>();
        // playerRigidbody = player.GetComponent<Rigidbody>();
        controller = player.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("PlayerJump", !player.isGrounded);
        animator.SetFloat("PlayerSpeed", controller.velocity.magnitude);
    }
}
