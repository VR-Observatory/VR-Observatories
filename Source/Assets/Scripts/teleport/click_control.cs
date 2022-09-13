using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click_control : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] canvas_list = new GameObject[24];
    public void all_enable()
    {
        foreach (GameObject obj in canvas_list)
        {
            if (obj)
            {

                obj.GetComponent<button_control>().enableButton();

            }
        }
    }

    public void all_disable()
    {
        foreach (GameObject obj in canvas_list)
        {
            if (obj)
            {

                obj.GetComponent<button_control>().disableButton();

            }
        }
    }
}
