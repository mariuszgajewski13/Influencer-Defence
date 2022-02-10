using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float sensitivity = 3f;

    private Vector2 rotation = Vector2.zero;

    public new Camera camera;

    public float cameraClamp = 80f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");

        rotation.x = Mathf.Clamp(rotation.x, -cameraClamp, cameraClamp);

        transform.eulerAngles = new Vector2(0, rotation.y) * sensitivity;
        camera.transform.localRotation = Quaternion.Euler(rotation.x * sensitivity, 0, 0);


    }



}
