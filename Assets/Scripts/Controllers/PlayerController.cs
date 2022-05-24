using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 *   Также как и остальные контроллеры отвечает за инициализацию всех зависимостей и компонентов
 *   объекта, а также за обработку инпутов
 *
 *
 *   Идеально было бы разбить на:
 *   - [CHECK] класс отвечающий за гравитацию 
 *   - [CHECK] всякие приколы по типу проверки родительских компонентов на соответствие
 *     или поиск других объектов в сцене тоже вынести отдельно (в юньке уже усть втроенные
 *     для этого функции)
 *   - [CHECK]класс или апи для конвертации векторов (трансляция векторов от камеры объекту
 *     или проекция векторов на плоскость) (в итоге кнвертация уместилась в две строчки кода,
 *     так что не вижу смысла делать для этого отдельный класс)
**/

[Serializable]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(HitPoints))]
public class PlayerController : MonoBehaviour
{

    [Min(0)]
    public float PlayerRotationSpeed = 1;

    private GameObject character;

    private PlayerActions playerActions;

    private GameObject observerCamera;

    private GravityController gravity;

    private float JumpStartTime;


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
        gravity = GetComponent<GravityController>();
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

        if (playerActions.InGame.Jump.IsPressed())
        {
            gravity.ContinueJump();
        }

        Vector2 move = playerActions.InGame.Move.ReadValue<Vector2>();

        Vector3 forward = Vector3.ProjectOnPlane(observerCamera.transform.forward, Vector3.up).normalized * move.y;
        Vector3 right = observerCamera.transform.right.normalized * move.x;

        gravity.Walk(forward + right);

    }

    void Activate(InputAction.CallbackContext context)
    {
        Debug.Log("Player Activate his current ability!");
    }

    void Jump(InputAction.CallbackContext context)
    {
        // Debug.Log("Jump action was perfomed");
        gravity.Jump(Time.time);
    }

}
