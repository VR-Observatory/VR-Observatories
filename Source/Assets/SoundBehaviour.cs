using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehaviour : MonoBehaviour

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
        if (Input.GetMouseButtonDown(0))
        {
            source.Play();
        }
    }


}
