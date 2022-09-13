using System;
using UnityEngine;

public class EnvironmentalAudio : MonoBehaviour
{
    public AudioSource environmentalAudio;

    private void Start()
    {
        // loop the audio
        environmentalAudio.loop = true;
        // assume the player spawns outside the telescope 
        environmentalAudio.playOnAwake = true;
    }

    private void OnTriggerEnter(Collider player)
    {
        // pause the environmental audio if the player enters the telescope
        if (environmentalAudio.isPlaying)
        {
            environmentalAudio.Pause();
        }
    }

    private void OnTriggerExit(Collider player)
    {
        // play the environmental audio if the player exits the telescope
        if (!environmentalAudio.isPlaying)
        {
            environmentalAudio.Play();
        }
    }
}
