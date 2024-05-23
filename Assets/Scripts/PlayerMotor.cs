using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private Animator animator;

    private float speed = 5.0f,
        verticalVelocity = 0.0f,
        jumpForce = 8.0f, // The force applied when jumping
        gravity = 12.0f;

    public static int numOfCoins;

    private float animationDuration = 3.0f;
    private bool isSliding = false; // Flag to check if the player is sliding
    private float slideDuration = 1.0f; // Duration of the slide
    private float slideSpeed = 10.0f; // Speed during the slide

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        numOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (EndOfGame.gameOver) // Stop updating if the game is over
        {
            return;
        }

        //to stop the player from moving left right in the first 3 seconds
        if (Time.time < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return; //when it hits return the rest of the code wont be run
        }

        moveVector = Vector3.zero;

        // Handle sliding
        if (Input.GetKeyDown(KeyCode.S) && !isSliding) // Changed to KeyCode.S
        {
            StartCoroutine(Slide());
        }

        // Applying gravity
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;

            // Jumping
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Jumping!");
                verticalVelocity = jumpForce; // Apply upward force for jumping
                animator.SetTrigger("Jump");
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // X = left and right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;

        // Y = up and down
        moveVector.y = verticalVelocity;

        // Z = forward and backward
        moveVector.z = isSliding ? slideSpeed : speed;

        controller.Move(moveVector * Time.deltaTime);

        // Check if the player falls off the screen
        if (transform.position.y < -5) // Adjust this value as needed
        {
            EndOfGame.TriggerGameOver();
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        // Trigger the slide animation
        animator.SetBool("isSliding", true);

        // Reduce the height of the player for sliding
        controller.height = 0.5f;
        controller.center = new Vector3(controller.center.x, 0.25f, controller.center.z);

        // Set slide speed
        float originalSpeed = speed;
        speed = slideSpeed;

        // Wait for the slide duration
        yield return new WaitForSeconds(slideDuration);

        // Reset the height of the player after sliding
        controller.height = 2.0f;
        controller.center = new Vector3(controller.center.x, 1.0f, controller.center.z);

        // Reset speed
        speed = originalSpeed;

        // Reset the slide animation
        animator.SetBool("isSliding", false);

        isSliding = false;
    }

    public void Setspeed(float modifier)
    {
        speed = 5.0f + modifier;
    }
<<<<<<< Updated upstream
    public void Setspeed(float modifier)
    {
        speed = 5.0f + modifier;
    }
}
=======

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacles")
        {
            EndOfGame.TriggerGameOver();
            animator.SetTrigger("Die"); // Maybe we can add death animation
        }
    }
}
>>>>>>> Stashed changes
