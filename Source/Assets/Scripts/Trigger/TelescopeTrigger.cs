﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeTrigger : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Trigger"))
        {
            animator.SetBool("Move", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Trigger"))
        {
            animator.SetBool("Move", false);
        }
    }

}
