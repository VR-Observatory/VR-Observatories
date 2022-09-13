using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            source.Play();
        }

}
}