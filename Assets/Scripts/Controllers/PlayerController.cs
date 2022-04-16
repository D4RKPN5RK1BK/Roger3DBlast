using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;


// Отвечает за взаимодействие объекта с внешними триггерами и за его инициализацию
public class PlayerController : MonoBehaviour
{
    [Min(0)]
    public float PlayerSpeed = 1;
    [Min(0)]
    public float PlayerRotationSpeed = 1;
    [Min(0)]
    public float PlayerJumpForce = 10;
    private GameObject character;
    private PlayerActions playerActions;
    private Rigidbody rigetbody;
    private GameObject observerCamera;
    private bool playerCanJump;
    private bool playerJumpPosition;

    void Awake()
    {
        playerActions = new PlayerActions();
        rigetbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        playerActions.Enable();
    }

    void OnDisable()
    {
        playerActions.Disable();
    }

    void Start()
    {
        try
        {
            observerCamera = FindObjectOfType<CameraController>().gameObject;
        }
        catch (NullReferenceException ex)
        {
            Debug.LogWarning(ex.Message);
            observerCamera = Camera.main.gameObject;
        }

        try
        {
            foreach (Transform t in transform)
            {
                if (t.tag == "Character")
                {
                    character = t.gameObject;
                }
            }

            if (character == null)
            {
                throw new Exception("Не найден дочерний объект с тегом \"Character\"!");
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
            character = Instantiate(new GameObject());
        }

        playerActions.InGame.Jump.performed += Jump;
        playerActions.InGame.Activate.performed += Activate;
    }


    void Update()
    {
        Vector2 move = playerActions.InGame.Move.ReadValue<Vector2>();
        Vector3 cameraPos = observerCamera.transform.position;

        Vector3 forward = new Vector3(transform.position.x - cameraPos.x, 0, transform.position.z - cameraPos.z).normalized * move.y;
        Vector3 right = observerCamera.transform.right.normalized * move.x;

        Vector3 layedMove = forward + right;
        rigetbody.AddForce(new Vector3(layedMove.x, 0, layedMove.z) * PlayerSpeed);

        if (Vector2.zero != move)
            character.transform.forward = Vector3.RotateTowards(character.transform.forward, layedMove, Time.deltaTime * PlayerRotationSpeed, 0).normalized;
    }

    void Jump(InputAction.CallbackContext context)
    {

        if (playerCanJump)
        {
            playerJumpPosition = true;
            rigetbody.AddForce(new Vector3(0, PlayerJumpForce, 0), ForceMode.VelocityChange);
            playerCanJump = false;
        }
    }

    void Activate(InputAction.CallbackContext context)
    {
        Debug.Log("Player Activate his current ability!");


    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Surface")
        {
            playerJumpPosition = false;
            playerCanJump = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Surface")
        {
            playerJumpPosition = true;
            playerCanJump = false;
        }
    }

    public bool GetJumpState()
    {
        return playerJumpPosition;
    }

}
