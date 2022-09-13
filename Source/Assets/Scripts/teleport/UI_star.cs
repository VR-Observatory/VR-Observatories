using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_star : MonoBehaviour
{
    public GameObject Crosshairs;
    public GameObject elevator_notice;
    private bool whether_first = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Crosshairs.GetComponent<DrawStar>().Crosshairs_visible = true;
        if (whether_first == false)
        {
            elevator_notice.SetActive(true);
            Crosshairs.GetComponent<DrawStar>().whether_first = true;
            whether_first = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        elevator_notice.SetActive(false);
        Crosshairs.GetComponent<DrawStar>().Crosshairs_visible = false;


    }
}
