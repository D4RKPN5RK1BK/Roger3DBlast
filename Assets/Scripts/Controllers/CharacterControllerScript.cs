using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
*   Остановился на том что решил прописать собственную гравиацию.
*   После нее соответственно нужно решить как тут все привести в порядок
*
*   В данный момент тут целая солянки из ЖЕЛАТЕЛЬНО разных классов
*
*   Идеально было бы разбить на:
*   - класс обрабатывающий инпуты
*   - класс отвечающий за гравитацию
*   - всякие приколы по типу проверки родительских компонентов на соответствие
*     или поиск других объектов в сцене тоже вынести отдельно
*/

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerScript : MonoBehaviour
{
    [Min(0)]
    public float PlayerSpeed = 1;
    [Min(0)]
    public float PlayerRotationSpeed = 1;
    [Min(0)]
    public float PlayerJumpForce = 10;
    private GameObject character;
    private PlayerActions playerActions;
    private GameObject observerCamera;
    private CharacterController controller;

    private Vector3 velocity;

    void OnEnable()
    {
        playerActions.Enable();
    }

    void OnDisable()
    {
        playerActions.Disable();
    }

    void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.InGame.Jump.performed += Jump;
        playerActions.InGame.Activate.performed += Activate;
        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
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

       

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = playerActions.InGame.Move.ReadValue<Vector2>();
        Vector3 cameraPos = observerCamera.transform.position;

        Vector3 forward = new Vector3(transform.position.x - cameraPos.x, 0, transform.position.z - cameraPos.z).normalized * move.y;
        Vector3 right = observerCamera.transform.right.normalized * move.x;

        Vector3 layedMove = forward + right;
        controller.Move(new Vector3(layedMove.x, 0, layedMove.z) * PlayerSpeed);

        if (Vector2.zero != move)
            character.transform.forward = Vector3.RotateTowards(character.transform.forward, layedMove, Time.deltaTime * PlayerRotationSpeed, 0).normalized;

        if (!controller.isGrounded) {
            controller.Move(new Vector3(0, -Time.deltaTime, 0));
        }
        
    }

    void Activate(InputAction.CallbackContext context)
    {
        Debug.Log("Player Activate his current ability!");


    }

    void Jump(InputAction.CallbackContext context)
    {

        if (controller.isGrounded)
        {
            Debug.Log("Player used jump");

        }
    }

}
