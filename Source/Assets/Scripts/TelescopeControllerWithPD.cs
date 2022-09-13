using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeControllerWithPD : MonoBehaviour
{
    public Joystick telescopeSlider1;
    public Joystick telescopeSlider2;
    public float sliderThreshold = 0.5f;

    public GameObject playerController; // needed to get input mode
    public AudioSource telescopeInteractionAudio;
    
    [Header("Settings")]
    [SerializeField]
    private GameObject midpartConnection;
    [SerializeField]
    private GameObject toppartConnection;
    [SerializeField]
    [Range(0f, 100f)]
    private int rotationSpeed;

  

    [Header("Midpart Rotation")]
    [SerializeField]
    [Range(-90f, 270f)]
    private float midpartMinimumAngle;
    [SerializeField]
    [Range(-90f, 270f)]
    private float midpartMaximumAngle;

    [Header("Toppart Rotation")]
    [SerializeField]
    [Range(-90f, 270f)]
    private float toppartMinimumAngle;
    [SerializeField]
    [Range(-90f, 270f)]
    private float toppartMaximumAngle;

    [Header("Start Angles")]
    [SerializeField]
    private float midpartStartAngle = 90f;
    [SerializeField]
    private float toppartStartAngle = 90f;

    private float midpartDefaultAngle, toppartDefaultAngle;
    private bool isPlayerIn;
    
    // Start is called before the first frame update
    void Start()
    {
        midpartDefaultAngle = midpartStartAngle;
        toppartDefaultAngle = toppartStartAngle;
        AudioMaster.InitAudioSource(telescopeInteractionAudio, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.GetComponent<PlayerControlManager>().controlMode.Equals(PlayerControlManager.controlModes.AndroidMobile))
            MoveMobile();
        else
            Move();
        // early stop the telescope interaction audio
        AudioMaster.EarlyStop(telescopeInteractionAudio, 0.5f);
    }
    
    private void rotateUpdate(float value)
    {
        midpartConnection.transform.localEulerAngles = new Vector3(midpartConnection.transform.localEulerAngles.x,-value,midpartConnection.transform.localEulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isPlayerIn = false;
        }
    }

    private void Move()
    {
        if (isPlayerIn)
        {
            // Button A/B for rotating middle part
            if (Input.GetButton("Telescope Mid Rotate Pos") && midpartDefaultAngle <= midpartMaximumAngle)
            {
                // Debug.Log(IsPlayerDetected(player,playerDetection));
                midpartConnection.transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
                midpartDefaultAngle += Time.deltaTime * rotationSpeed;
                // play telescope interaction audio
                AudioMaster.PlayAudio(telescopeInteractionAudio);
            }
            else if (Input.GetButton("Telescope Mid Rotate Neg") && midpartDefaultAngle >= midpartMinimumAngle) {
                midpartConnection.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
                midpartDefaultAngle -= Time.deltaTime * rotationSpeed;
                // play telescope interaction audio
                AudioMaster.PlayAudio(telescopeInteractionAudio);
            }

            // Button X/Y for rotating top part
            if (Input.GetButton("Telescope Top Rotate Pos") && toppartDefaultAngle <= toppartMaximumAngle)
            {
                toppartConnection.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                toppartDefaultAngle += Time.deltaTime * rotationSpeed;
                // play telescope interaction audio
                AudioMaster.PlayAudio(telescopeInteractionAudio);
            }
            else if (Input.GetButton("Telescope Top Rotate Neg") && toppartDefaultAngle >= toppartMinimumAngle)
            {
                toppartConnection.transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
                toppartDefaultAngle -= Time.deltaTime * rotationSpeed;
                // play telescope interaction audio
                AudioMaster.PlayAudio(telescopeInteractionAudio);
            }
        }
    }


    private void MoveMobile()
    {
        if (isPlayerIn)
        {
            // Button A/B for rotating middle part
            if (telescopeSlider1.Horizontal > sliderThreshold && midpartDefaultAngle <= midpartMaximumAngle)
            {
                midpartConnection.transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
                midpartDefaultAngle += Time.deltaTime * rotationSpeed;
                // play telescope interaction audio
                AudioMaster.PlayAudio(telescopeInteractionAudio);
            }
            else if (telescopeSlider1.Horizontal < -sliderThreshold && midpartDefaultAngle >= midpartMinimumAngle)
            {
                midpartConnection.transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
                midpartDefaultAngle -= Time.deltaTime * rotationSpeed;
                // play telescope interaction audio
                AudioMaster.PlayAudio(telescopeInteractionAudio);
            }

            // Button X/Y for rotating top part
            if (telescopeSlider2.Horizontal > sliderThreshold && toppartDefaultAngle <= toppartMaximumAngle)
            {
                toppartConnection.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                toppartDefaultAngle += Time.deltaTime * rotationSpeed;
                // play telescope interaction audio
                AudioMaster.PlayAudio(telescopeInteractionAudio);
            }
            else if (telescopeSlider2.Horizontal < -sliderThreshold && toppartDefaultAngle >= toppartMinimumAngle)
            {
                toppartConnection.transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
                toppartDefaultAngle -= Time.deltaTime * rotationSpeed;
                // play telescope interaction audio
                AudioMaster.PlayAudio(telescopeInteractionAudio);
            }
        }
    }
}
