using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFK : MonoBehaviour
{


    public AudioClip clip;
    public AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            source.Play();
        }
        
    }


}
