using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;
    private Rigidbody playerRigidbody;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("PlayerJump", player.GetJumpState());
        animator.SetFloat("PlayerSpeed", playerRigidbody.velocity.magnitude);
    }
}
