using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomToggle : MonoBehaviour
{
    public GameObject toggleTarget;

    public void TargetToggle()
    {
        toggleTarget.SetActive(!toggleTarget.activeInHierarchy);
    }
}
