using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController characterController;

    public float walkingSpeed = 12f;
    public float runningSpeed = 20f;
    public float speed;
    //public float gravity = -9.81f;
    public float gravity = -20f;
    public bool isWalking;

    public Transform groundCheck;
    //public float groundDistance = 0.4f;
    public float groundDistance = 1f;
    public LayerMask groundMask;

    public float jumpHeight = 2f;

    Vector3 velocity;
    bool isGrounded;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isWalking = !Input.GetKey(KeyCode.LeftShift);
        speed = isWalking ? walkingSpeed : runningSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isWalking)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        if(isGrounded && velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        if(move != Vector3.zero)
        {
            characterController.Move(move * speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("isJumping", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }
}
