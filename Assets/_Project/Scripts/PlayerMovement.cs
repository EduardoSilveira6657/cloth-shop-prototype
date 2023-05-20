using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeedMultiplier = 5f;
    [SerializeField] Rigidbody2D playerRigidBody;
    
    Vector2 movementInput;
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }
    
    void FixedUpdate()
    {
        playerRigidBody.MovePosition(playerRigidBody.position + movementInput * movementSpeedMultiplier * Time.fixedDeltaTime);
    }
}
