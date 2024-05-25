using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private Animator animator;

    SoundEffectsPLayer soundEffectsplayer;

    private void Awake()
    {
        soundEffectsplayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundEffectsPLayer>();
    }

    private float speed = 5.0f,
        jumpSpeed,
        verticalVelocity = 0.0f,
        jumpForce = 8.0f, // The force applied when jumping
        gravity = 12.0f;

    public static int numOfCoins;

    private float animationDuration = 3.0f, time = 0f;
    private bool isSliding = false;
    private float slideDuration = 1.0f;
    private float slideSpeed = 10.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        numOfCoins = 0;
        time= 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (EndOfGame.gameOver)
        {
            return;
        }

        if (time < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return; //when it hits return the rest of the code wont be run
        }
        moveVector = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.S) && !isSliding) // sliding with s
        {
            StartCoroutine(Slide());
        }

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
            jumpSpeed = speed + 2;
            // Jumping
            if (Input.GetKeyDown(KeyCode.W))
            {
                soundEffectsplayer.PlaySFX(soundEffectsplayer.jump);
                verticalVelocity = jumpForce;
                animator.SetTrigger("Jump");

                speed = jumpSpeed;

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

        // if the player falls off the screen
        if (transform.position.y < -5)
        {
            EndOfGame.TriggerGameOver();
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        animator.SetBool("isSliding", true);

        //reduce player height when sliding
        controller.height = 0.5f;
        controller.center = new Vector3(controller.center.x, 0.25f, controller.center.z);

        float originalSpeed = speed;
        speed = slideSpeed;

        yield return new WaitForSeconds(slideDuration);

        controller.height = 2.0f;
        controller.center = new Vector3(controller.center.x, 1.0f, controller.center.z);

        speed = originalSpeed;

        // reset the slide animation
        animator.SetBool("isSliding", false);

        isSliding = false;
    }

    public void Setspeed(float modifier)
    {
        speed = 5.0f + modifier;
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacles" || hit.transform.tag == "Enemy")
        {
            soundEffectsplayer.PlaySFX(soundEffectsplayer.death);
            EndOfGame.TriggerGameOver();
            animator.SetTrigger("Die"); // Maybe we can add death animation
        }
    }

}
