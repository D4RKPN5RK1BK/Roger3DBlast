using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;


// Отвечает за взаимодействие объекта с внешними триггерами и за его инициализацию
public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
    private PlayerActions playerActions;
    private Rigidbody rigetbody;
    private GameObject camera;
    private bool playerCanJump;

    void Awake() {
        playerActions = new PlayerActions();
        rigetbody = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        playerActions.Enable();
    }

    void OnDisable(){
        playerActions.Disable();
    }

    void Start() {
        camera = FindObjectOfType<CameraController>().gameObject;
        playerActions.InGame.Jump.performed += Jump;
        playerActions.InGame.Activate.performed += Activate;
    }

    // Обновляется каждый фрейм, отвечает за движения персонажа и его вращение
    void Update() {
        Vector2 move = playerActions.InGame.Move.ReadValue<Vector2>();
        Vector3 cameraPos = camera.transform.position;

        Vector3 forward = new Vector3(transform.position.x - cameraPos.x, 0, transform.position.z - cameraPos.z).normalized * move.y;
        Vector3 right = camera.transform.right.normalized * move.x;

        Vector3 layedMove = forward + right;
        rigetbody.AddForce(new Vector3(layedMove.x, 0, layedMove.z) * PlayerSpeed);

        if (Vector2.zero != move)
            transform.forward = layedMove;
    }

    void Jump(InputAction.CallbackContext context) {
        
        if (playerCanJump) {
            Debug.Log("Player Jump!");
            rigetbody.AddForce(new Vector3(0, 10, 0), ForceMode.VelocityChange);
            playerCanJump = false;
        }
    }

    void Activate(InputAction.CallbackContext context) {
        Debug.Log("Player Activate his current ability!");

        
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Surface") {
            playerCanJump = true;
        }
    }

}
