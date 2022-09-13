using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetection : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    private int climbSpeed;
   
  private GameObject player ;
  private bool isOnladder = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnladder && Input.GetButton("Jump"))
        {
            player.transform.Translate(Vector3.up * Time.deltaTime * climbSpeed / 10,Space.World);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isOnladder = true;
            player = other.gameObject;
            
            // other.transform.Translate(Vector3.up * Time.deltaTime * climbSpeed / 10,Space.World);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isOnladder = false;
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isOnladder = true;
        }    }
}
