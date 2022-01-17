using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpForce = 20f;
    public float runningSpeed = 100f;

    Vector3 momentum = Vector3.zero;
    
    bool isGrounded;
    float gravity = Physics.gravity.y;

    public float sensitivity = 3f;
    private Vector2 rotation = Vector2.zero;
    public Camera camera;
    public float cameraClamp = 20f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (characterController == null) return;
      
        CameraRotation();
        MoveCharacter();

        characterController.Move(momentum * Time.deltaTime);
        Debug.Log(momentum);
    }

    void MoveCharacter()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && momentum.y <= 0)
        {
            momentum.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move;

        if (isGrounded) move = (transform.right * x * runningSpeed) + (transform.forward * z * runningSpeed);
        else move = momentum;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            momentum.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        move.y = momentum.y;
        move.y += Time.deltaTime * gravity;

        momentum = move;
    }

    void CameraRotation()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");

        rotation.x = Mathf.Clamp(rotation.x, -cameraClamp, cameraClamp);

        characterController.transform.eulerAngles = new Vector2(0, rotation.y) * sensitivity;
        camera.transform.localRotation = Quaternion.Euler(rotation.x * sensitivity, 0, 0);
    }

}
