using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Mathf;

public class CameraController : MonoBehaviour
{
    [Min(0)]
    public float ScaleSpeed = 1;
    [Min(0)]
    public float RotationSpeed = 1;
    private GameObject observableObject;
    private CameraActions cameraActions;
    private Vector3 offset;

    void Awake() {
        cameraActions = new CameraActions();
    }

    void OnEnable() {
        cameraActions.Enable();
    }

    void OnDisable() {
        cameraActions.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        observableObject = FindObjectOfType<PlayerController>().gameObject;
        offset = transform.position - observableObject.transform.position;  
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = observableObject.transform.position + offset;
        Vector2 angle = cameraActions.InGame.RotateCamera.ReadValue<Vector2>() * Time.deltaTime * RotationSpeed;
        
        transform.forward = -offset.normalized;
        offset = Quaternion.AngleAxis(angle.x, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(-angle.y, transform.right) * offset;
    }

    void SetForwardToPlayer() {

    }

}
