using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGravity {

    public Vector3 velocity;
    public bool isGrounded;

    public CharacterGravity() {
        velocity = new Vector3(0, -1, 0);
        isGrounded = true;
    }

    public Vector3 MoveCharacter() {
        return velocity;
    }    
}
