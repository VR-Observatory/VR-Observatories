using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_control : MonoBehaviour
{
    // Start is called before the first frame update
    public void enableButton()
    {
        GameObject child = transform.Find("Button").gameObject;
        child.SetActive(true);
        child.GetComponent<Button>().interactable = true;
    }

    public void disableButton()
    {
        GameObject child = transform.Find("Button").gameObject;
        child.GetComponent<Button>().interactable = false;
        child.SetActive(false);
    }
}
