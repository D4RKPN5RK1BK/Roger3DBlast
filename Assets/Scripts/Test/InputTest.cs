using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private PlayerActions playerActions;

    void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnDisable()
    {
        playerActions.Disable();
    }

    void OnEnable()
    {
        playerActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = playerActions.InGame.Move.ReadValue<Vector2>();
        if (move != Vector2.zero)
            Debug.Log(move);

    }
}
