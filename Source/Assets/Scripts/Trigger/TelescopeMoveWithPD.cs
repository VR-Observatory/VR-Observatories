using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeMoveWithPD : MonoBehaviour
{
    public Joystick telescopeSlider1;
    public Joystick telescopeSlider2;
    public float sliderThreshold = 0.5f;

    public GameObject playerController; // needed to get input mode

    // walking audio setup
    private Vector3 _lastPlayerPosition;
    private AudioSource _playerWalkingAudioSource;
    private AudioClip _playerWalkingAudioClip;
    private bool _isWalking;
    
    [Header("Settings")]
    [SerializeField]
    private GameObject telescope;
    [SerializeField]
    private GameObject buildingShell;
    
    [SerializeField]
    [Range(0f, 100f)]
    private int tRotationSpeed;
    [SerializeField]
    [Range(0f, 100f)]
    private int bRotationSpeed;

    [Header("Rotation")]
    [SerializeField]
    [Range(-90f, 270f)]
    private float minimumAngle;
    [SerializeField]
    [Range(-90f, 270f)]
    private float maximumAngle;
    
    [Header("Start Angles")]
    [SerializeField]
    private float startAngle = 90f;
    
    private float defaultAngle;
    private bool isPlayerIn;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultAngle = startAngle;
        switch (playerController.GetComponent<PlayerControlManager>().controlMode)
        {
            case PlayerControlManager.controlModes.AndroidMobile: 
                player = GameObject.Find("AndroidMobileController");
                break;
            case PlayerControlManager.controlModes.OVR:
                player = GameObject.Find("OVRPlayerControllerHands");
                break;
            case PlayerControlManager.controlModes.PC:
                player = GameObject.Find("PCPlayerController");
                break;
            default: 
                player = GameObject.Find("PCPlayerController");
                break;
        }

        _lastPlayerPosition = player.transform.position;
        _playerWalkingAudioSource = AudioAssistant.GetAudioSource("Default", player);
        _playerWalkingAudioClip = AudioAssistant.GetAudioClip("walk");
        _isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // walking
        if (_lastPlayerPosition.x.Equals(player.transform.position.x) &&
            _lastPlayerPosition.z.Equals(player.transform.position.z)) 
        { 
            // stop player walking audio if the player is not moving horizontally
            _playerWalkingAudioSource.Stop(); 
        }
        else
        {
            if (!_playerWalkingAudioSource.isPlaying)
            {
                _playerWalkingAudioSource.PlayOneShot(_playerWalkingAudioClip);
            }
        } 
        
        if (playerController.GetComponent<PlayerControlManager>().controlMode.Equals(PlayerControlManager.controlModes.AndroidMobile))
            MobileMove();
        else
            Move();
        
        // reset last player position
        _lastPlayerPosition = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {
            player = other.gameObject;
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            player = other.gameObject;
            isPlayerIn = false;
        }
    }

    public void Move()
    {
        if (isPlayerIn)
        {
            // Button for rotating the building
            if (Input.GetButton("Telescope Mid Rotate Pos"))
            {
                buildingShell.transform.RotateAround(buildingShell.transform.position,Vector3.up,Time.deltaTime * bRotationSpeed);
                // defaultAngle += Time.deltaTime * rotationSpeed;
                player.gameObject.transform.RotateAround(buildingShell.transform.position,Vector3.up,Time.deltaTime * bRotationSpeed);
            }
            else if (Input.GetButton("Telescope Mid Rotate Neg")) {
                buildingShell.transform.RotateAround(buildingShell.transform.position,Vector3.down,Time.deltaTime * bRotationSpeed);
                // defaultAngle += Time.deltaTime * rotationSpeed;
                player.gameObject.transform.RotateAround(buildingShell.transform.position,Vector3.down,Time.deltaTime * bRotationSpeed);
            }

            // Button for rotating the telescope
            if (Input.GetButton("Telescope Top Rotate Pos") && defaultAngle <= maximumAngle)
            {
                telescope.transform.Rotate(Vector3.left * Time.deltaTime * tRotationSpeed);
                defaultAngle += Time.deltaTime * tRotationSpeed;
            }
            else if (Input.GetButton("Telescope Top Rotate Neg") && defaultAngle >= minimumAngle)
            {
                telescope.transform.Rotate(Vector3.right * Time.deltaTime * tRotationSpeed);
                defaultAngle -= Time.deltaTime * tRotationSpeed;
            }
        }
    }

    public void MobileMove()
    {
        if (isPlayerIn)
        {
            // Button for rotate the building
            if (telescopeSlider1.Horizontal > sliderThreshold)
            {
                buildingShell.transform.RotateAround(buildingShell.transform.position, Vector3.up,
                    Time.deltaTime * bRotationSpeed);
                player.gameObject.transform.RotateAround(buildingShell.transform.position, Vector3.up,
                    Time.deltaTime * bRotationSpeed);
            }
            else if (telescopeSlider1.Horizontal < -sliderThreshold)
            {
                buildingShell.transform.RotateAround(buildingShell.transform.position, Vector3.down,
                    Time.deltaTime * bRotationSpeed);
                // defaultAngle += Time.deltaTime * rotationSpeed;
                player.gameObject.transform.RotateAround(buildingShell.transform.position, Vector3.down,
                    Time.deltaTime * bRotationSpeed);
            }

            // Button for rotating the telescope
            if (telescopeSlider2.Horizontal > sliderThreshold && defaultAngle <= maximumAngle)
            {
                telescope.transform.Rotate(Vector3.left * Time.deltaTime * tRotationSpeed);
                defaultAngle += Time.deltaTime * tRotationSpeed;
            }
            else if (telescopeSlider2.Horizontal < -sliderThreshold && defaultAngle >= minimumAngle)
            {
                telescope.transform.Rotate(Vector3.right * Time.deltaTime * tRotationSpeed);
                defaultAngle -= Time.deltaTime * tRotationSpeed;
            }
        }
    }
}
