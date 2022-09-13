using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCameraAndroidMobile : MonoBehaviour
{
    public Joystick cameraJoystick;

    public float sensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {

        float mouseX = cameraJoystick.Horizontal * sensitivity;
        float mouseY = cameraJoystick.Vertical * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
