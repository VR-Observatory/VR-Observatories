using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonControlAndroidMobile : MonoBehaviour
{
    public CharacterController controller;
    public Joystick movementJoystick;

    public bool canFly = false;

    public float baseSpeed = 10f;
    public float sprintFactor = 1.5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public GameObject mobileInfo;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask buildingMask;

    Vector3 velocity;
    bool isGrounded;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        mobileInfo.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ToggleMenu"))
            mobileInfo.SetActive(!mobileInfo.activeSelf);

        if (Input.GetButton("Sprint"))
            speed = sprintFactor * baseSpeed;
        else
            speed = baseSpeed;

        if (!canFly)
            Move();
        else
            Fly();
    }

    void Move()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) ||
            Physics.CheckSphere(groundCheck.position, groundDistance, buildingMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = movementJoystick.Horizontal;
        float z = movementJoystick.Vertical;

        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Fly()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Y_axis");

        Vector3 move = transform.right * x + transform.up * y + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}