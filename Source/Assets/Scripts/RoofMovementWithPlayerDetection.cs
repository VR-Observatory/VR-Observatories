using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class RoofMovementWithPlayerDetection : MonoBehaviour
{
    public Animator roof;

    private bool isIn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Open Roof"))
        {
            RoofMove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isIn = false;
        }
    }

    public void RoofMove()
    {
        if (isIn)
        {
            if (!roof.IsInTransition(0) && roof.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9)
            {
                roof.SetBool("isOpen",!roof.GetBool("isOpen"));
                print("open roof");
            }
        }
    }

    public bool getIsIn()
    {
        return isIn;
    }
    
    
}
