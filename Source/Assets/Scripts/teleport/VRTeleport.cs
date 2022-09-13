using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTeleport : MonoBehaviour
{
    public GameObject vrcontroller;
    public GameObject target;

    

    public void Teleport()
    {

        vrcontroller.transform.position = target.transform.position;
    }
}
