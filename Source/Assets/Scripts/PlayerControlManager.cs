using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlManager : MonoBehaviour
{
    //public GameObject[] _UIObjects;
    public GameObject[] _PCUIObjects;
    public GameObject[] _VRUIObjects;
    public GameObject[] buttons;

    public enum controlModes
    {
        PC,
        OVR,
        AndroidMobile,
        Auto
    }
    public bool isDisplayPerformanceStats = false;

    public controlModes controlMode;
    public GameObject pcPlayerController;
    public GameObject ovrPlayerController;
    public GameObject androidMobilePlayerController;
    
    
    public GameObject pcPerformanceStats;
    public GameObject ovrPerformanceStats;
    public GameObject androidPerformanceStats;
   
    // Start is called before the first frame update
    void Start()
    {
        _PCUIObjects = GameObject.FindGameObjectsWithTag("PCUIObjects");
        _VRUIObjects = GameObject.FindGameObjectsWithTag("VRUIObjects");
        buttons = GameObject.FindGameObjectsWithTag("Button");

        switch (controlMode)
        {
            case controlModes.PC:
                ActivateControlPC();
                break;
            case controlModes.OVR:
                ActivateControlOVR();
                break;
            case controlModes.AndroidMobile:
                ActivateControlAndroidMobile();
                break;
            default:
                if (OVRManager.isHmdPresent)
                    ActivateControlOVR();
                else
                    ActivateControlPC();
                break;
        }
    }

    void ActivateControlPC()
    {
        pcPlayerController.SetActive(true);
        ovrPlayerController.SetActive(false);
        androidMobilePlayerController.SetActive(false);

        pcPerformanceStats.SetActive(isDisplayPerformanceStats);
        androidPerformanceStats.SetActive(false);
        ovrPerformanceStats.SetActive(false);

        foreach (GameObject vruiObject in _VRUIObjects)
        {
            vruiObject.SetActive(false);
        }

        //foreach (GameObject uiObject in _UIObjects) {
            //uiObject.GetComponent<OVRRaycaster>().enabled = false;
        //}

        
    }
    void ActivateControlOVR()
    {
        pcPlayerController.SetActive(false);
        ovrPlayerController.SetActive(true);
        androidMobilePlayerController.SetActive(false);

        ovrPerformanceStats.SetActive(isDisplayPerformanceStats);
        androidPerformanceStats.SetActive(false);
        pcPerformanceStats.SetActive(false);

        foreach (GameObject pcuiObject in _PCUIObjects)
        {
            pcuiObject.SetActive(false);
        }

        foreach (GameObject button in buttons)
        {
            button.GetComponent<BoxCollider>().enabled = false;
        }

        //foreach (GameObject uiObject in _UIObjects)
        //{
            //uiObject.GetComponent<GraphicRaycaster>().enabled = false;
        //}

        
    }
    
    void ActivateControlAndroidMobile()
    {
        pcPlayerController.SetActive(false);
        ovrPlayerController.SetActive(false);
        androidMobilePlayerController.SetActive(true);

        androidPerformanceStats.SetActive(isDisplayPerformanceStats);
        pcPerformanceStats.SetActive(false);
        ovrPerformanceStats.SetActive(false);

        

        // androidMobilePerformanceStats.SetActive(isDisplayPerformanceStats);
    }
}
