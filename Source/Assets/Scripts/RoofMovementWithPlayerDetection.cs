using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class RoofMovementWithPlayerDetection : MonoBehaviour
{
    public Animator roof;
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    private bool isIn;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = AudioAssistant.GetAudioSource("Linear Source 50", gameObject);
        _audioClip = AudioAssistant.GetAudioClip("shell");
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
                if (!_audioSource.isPlaying)
                {
                    _audioSource.PlayOneShot(_audioClip);
                }
            }
        }
    }

    public bool getIsIn()
    {
        return isIn;
    }
    
    
}
