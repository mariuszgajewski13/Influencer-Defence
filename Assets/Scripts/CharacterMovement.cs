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

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isWalking = !Input.GetKey(KeyCode.LeftShift);
        speed = isWalking ? walkingSpeed : runningSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x) + (transform.forward * z);

        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }
}
