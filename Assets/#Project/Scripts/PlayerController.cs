using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    float verticalSpeed;

    Vector2 moveVector;

    public float movementSpeed;
    public float gravityMultiplier;

    public CharacterController controller;

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("PEW PEW");
        }
        else if (context.canceled)
        {
            Debug.Log("*bruit de la balle qui tombe*");
        }
    }

    public void Jump (InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            verticalSpeed = 20f;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
       moveVector = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if(controller.isGrounded && verticalSpeed < 0)
        {
            verticalSpeed = 0;
        } 

        verticalSpeed += Physics.gravity.y * Time.deltaTime * gravityMultiplier;
    
        Vector3 movement = new Vector3(moveVector.x, 0, moveVector.y) * movementSpeed;

        if(movement != Vector3.zero) //new Vector3(0,0,0)) Autre manière de le faire // movement.magnitude >= 0
        {
            transform.forward = Vector3.Lerp(transform.forward, movement, 0.01f);
        }

        movement.y = verticalSpeed;
        controller.Move(movement * Time.deltaTime);
    }
}
