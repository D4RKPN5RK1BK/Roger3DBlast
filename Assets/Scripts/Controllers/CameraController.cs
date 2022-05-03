using System;
using UnityEngine;

/*
*   Класс возможно будет применяться не только к игроку но и ко всему "Вагону", так что ObservableObject не должен крутиться вместе с персонажем.
*   
*/

public class CameraController : MonoBehaviour
{
    [Min(0)]
    public float ScaleSpeed = 1;
    [Min(0)]
    public float RotationSpeed = 1;

    [Min(0)]
    public float CameraDistance = 10;
    public Vector3 CameraStartAngle;
    private GameObject observableObject;
    private CameraActions cameraActions;
    private Vector3 offset;

    void Awake()
    {
        cameraActions = new CameraActions();
    }

    void OnEnable()
    {
        cameraActions.Enable();
    }

    void OnDisable()
    {
        cameraActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject player = FindObjectOfType<CharacterController>().gameObject;
            foreach (Transform t in player.transform)
            {
                if (String.Equals(t.tag, "ObservablePoint"))
                {
                    observableObject = t.gameObject;
                    break;
                }
            }
            if (observableObject == null)
            {
                throw new Exception("Не найден дочерний объект с тегом \"ObservablePoint\"");
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
            observableObject = Instantiate(new GameObject());
        }
        finally
        {
            offset = new Vector3(0, 0, -CameraDistance);
            offset = Vector3.RotateTowards(offset, CameraStartAngle, 1, 0);
        }


    }

    // Update is called once per frame
    /*
    *   Спустя несколько фреймов после старта сцены система считывает все совершенные за несколько фреймов движеня мыши
    *   что в 90% случаев приводит к тому что камера находиться не на задуманной начальной позиции а хер пойми где
    */
    void Update()
    {
        transform.position = observableObject.transform.position + offset;
        Vector2 angle = cameraActions.InGame.RotateCamera.ReadValue<Vector2>() * Time.deltaTime * RotationSpeed;

        transform.forward = -offset.normalized;
        offset = Quaternion.AngleAxis(angle.x, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(-angle.y, transform.right) * offset;
    }

}
