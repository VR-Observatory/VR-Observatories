using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcontrol : MonoBehaviour
{
    public GameObject currentLight;
    public GameObject[] teleportlights = new GameObject[3];
    public GameObject Crosshairs;
    public GameObject teleport_notice;
    public GameObject eventsys;

    private GameObject playerController;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("PlayerController");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.GetComponent<PlayerControlManager>().controlMode.Equals(PlayerControlManager.controlModes.AndroidMobile))
        {
            teleport_notice.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter");
        eventsys.GetComponent<click_control>().all_enable();

        teleport_notice.SetActive(true); // instructions should always be visible, even after first
        if (Crosshairs.GetComponent<DrawStar>().whether_first == false)
        {
            //teleport_notice.SetActive(true);
            Crosshairs.GetComponent<DrawStar>().whether_first = true;
        }
        Crosshairs.GetComponent<DrawStar>().Crosshairs_visible = true;
        //Debug.Log("enter active");
        foreach (GameObject obj in teleportlights)
        {
            if (obj)
            {
                obj.SetActive(true);

                Vector3 tar = transform.position;
                tar.y = obj.transform.position.y;
                obj.transform.LookAt(tar);
                obj.transform.Rotate(0, 180, 0);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        eventsys.GetComponent<click_control>().all_disable();

        Crosshairs.GetComponent<DrawStar>().Crosshairs_visible = false;
        teleport_notice.SetActive(false);

        foreach (GameObject obj in teleportlights)
        {
            if (obj)
            {

                obj.SetActive(false);
            }
        }
    }
}
