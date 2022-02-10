using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform camera;
    public Camera cameraMain;

    private void Start()
    {
        cameraMain = Camera.main;
        camera = cameraMain.GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
