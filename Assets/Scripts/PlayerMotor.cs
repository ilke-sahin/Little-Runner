using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;

    private float speed = 5.0f,
        jumpForce = 8.0f, // The force applied when jumping
        verticalVelocity = 0.0f,
        gravity = 12.0f;

    public static int numOfCoins;

    private float animationDuration = 3.0f;
    private float maxSpeed = 50f; // Maximum speed allowed

    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private float laneDistance = 3.0f; // The distance between two lanes

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        numOfCoins = 0;
        StartCoroutine(IncreaseSpeedOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        moveVector = Vector3.zero;

        // Applying gravity
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;

            // Jumping
            if (SwipeManager.swipeUp)
            {
                Debug.Log("Jumping!");
                verticalVelocity = jumpForce; // Apply upward force for jumping
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Check for swipes
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        // Move towards the target position
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        // X = left and right
        moveVector.x = (targetPosition - transform.position).x;

        // Y = up and down
        moveVector.y = verticalVelocity;

        // Z = forward and backward
        moveVector.z = speed;

        // Move the player
        controller.Move(moveVector * Time.deltaTime);
    }

    IEnumerator IncreaseSpeedOverTime()
    {
        float increaseAmount = 1.0f;
        float timeInterval = 1.0f;

        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            speed = Mathf.Min(speed + increaseAmount, maxSpeed);
            increaseAmount *= 0.9f;
            timeInterval += 0.5f;
        }
    }

    public void Setspeed(float modifier)
    {
        speed = 5.0f + modifier;
    }
}
