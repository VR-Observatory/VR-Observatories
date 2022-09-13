using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetting : MonoBehaviour
{
    [Header("Player Menu Object")]
    public Animator playerMenuAnimator;

    [Header("Player Height")]
    [Tooltip("The slider in menu canvas to change player's height")]
    public Slider slider;
    public float heightMaxValue;
    public float heightMinValue;
    public GameObject playerObject;

    [Header("Player Rotation Mode")]
    public OVRPlayerController oVRPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        //playerMenuAnimator = this.GetComponent<Animator>();

        slider.minValue = heightMinValue;
        slider.maxValue = heightMaxValue;

        slider.onValueChanged.AddListener(heightUpdate);
    }

    private void heightUpdate(float value)
    {
        playerObject.transform.localPosition = new Vector3(playerObject.transform.localPosition.x, value, playerObject.transform.localPosition.z);
    }

    // Update is called once per frame
    public void Update()
    {

        print(playerObject.transform.localPosition);

        if (Input.GetKeyDown(KeyCode.JoystickButton6)) {
            if (!playerMenuAnimator.GetBool("isOpen")) { 
                changeMenu(true);
                //oVRPlayerController.EnableLinearMovement = false;
                //oVRPlayerController.EnableRotation = false;
            }
            else { 
                changeMenu(false);
                //oVRPlayerController.EnableLinearMovement = true;
                //oVRPlayerController.EnableRotation = true;
            }
        }
            
    }

    private void changeMenu(bool menuStatus)
    {
        playerMenuAnimator.SetBool("isOpen",menuStatus);
    }

    public void changeRotationModeToFixed() {
        oVRPlayerController.SnapRotation = true;
    }

    public void changeRotationModeToSmooth(){
        oVRPlayerController.SnapRotation = false;
    }

    }
