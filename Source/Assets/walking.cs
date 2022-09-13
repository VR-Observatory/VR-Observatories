using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walking : MonoBehaviour

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            source.Play();
        }
        
    }

}
