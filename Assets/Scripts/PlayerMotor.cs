using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;

    private float speed = 5.0f,
        verticalVelocity = 0.0f,
        gravity = 12.0f;

    private float animationDuration = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //to stop the player from moving left right in the first 3 seconds
        if (Time.time < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return; //when it hits return the rest of the code wont be run
        }
        moveVector = Vector3.zero;

        //applying gravity
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // X = left and right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

        // Y = up and down
        //need to calculate gravity
        moveVector.y = verticalVelocity;

        // Z = forward and backward
        //we dont need backward we can just say it equals speed
        //which is a positive value
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
    }
}
