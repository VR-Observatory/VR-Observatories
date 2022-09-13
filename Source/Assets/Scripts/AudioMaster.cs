using System;
using System.Collections.Generic;
using UnityEngine;

public static class AudioMaster
{
    public static void Instantiate()
    {
        _audioMinInterval = new Dictionary<AudioSource, float>();
        _audioTimerMap = new Dictionary<AudioSource, float>();
        _earlyStopTimerMap = new Dictionary<AudioSource, float>();
        _maxStopTimerMap = new Dictionary<AudioSource, float>();
    }

    // audioType is the name for each audio
    public enum AudioType
    {
        // add additional audio type here
        TelescopeInteractionAudio
    }

    private static Dictionary<AudioSource, float> _audioMinInterval;
    private static Dictionary<AudioSource, float> _audioTimerMap;
    private static Dictionary<AudioSource, float> _earlyStopTimerMap;
    private static Dictionary<AudioSource, float> _maxStopTimerMap;
    
    /// <summary>
    /// Given an audio source and the minimum replay time, record the relevant statistics.
    /// </summary>
    /// <param name="audioSource">The audio source to be initialized</param>
    /// <param name="minInterval">The minimum replay time interval</param>
    public static void InitAudioSource(AudioSource audioSource, float minInterval)
    {
        _earlyStopTimerMap[audioSource] = 0f;
        _maxStopTimerMap[audioSource] = 0f;
        _audioTimerMap[audioSource] = -minInterval;
        _audioMinInterval[audioSource] = minInterval;
        audioSource.playOnAwake = false;
    }

    /// <summary>
    /// If an audio is playing, the minimum time interval for the audio to be replayed.
    /// For example, if an audio is 2 seconds long,
    /// it is probably the best to wait for at least 2 seconds before the audio can be replayed.
    /// </summary>
    /// <param name="audioSource">the audio tuple containing the audio source</param>
    /// <returns>
    /// return true if the audio is replayable. 
    /// </returns>
    private static bool IsAudioPlayable(AudioSource audioSource)
    {
        // get the audio last play time
        float lastPlayTime = _audioTimerMap[audioSource];
        // audio is playable if the time interval has expired
        if (lastPlayTime + _audioMinInterval[audioSource] < Time.time)
        {
            // register the latest play time
            _audioTimerMap[audioSource] = Time.time;
            return true;
        }
        // otherwise the audio is not playable
        return false;
    }

    private static void ResetEarlyStopTimer(AudioSource audioSource)
    {
        _earlyStopTimerMap[audioSource] = 0f;
    }
    
    /// <summary>
    /// Early stop the audio given an audio source and a delay interval,
    /// for example, if the player stops walking, after 0.1 seconds delay,
    /// stop the audio.
    /// </summary>
    /// <param name="audioSource">The audio source to be early stopped.</param>
    /// <param name="delayStopInterval">The delayed time interval for stopping the audio after an event.</param>
    public static void EarlyStop(AudioSource audioSource, float delayStopInterval)
    {
        if (!audioSource.isPlaying)
        {
            ResetEarlyStopTimer(audioSource);
            return;
        }
        _earlyStopTimerMap[audioSource] += Time.deltaTime;
        if (_earlyStopTimerMap[audioSource] >= delayStopInterval)
        {
            audioSource.Stop();
        }
    }
    
    private static void ResetMaxStopTimer(AudioSource audioSource)
    {
        _maxStopTimerMap[audioSource] = 0f;
    }

    /// <summary>
    /// Stop an audio upon reaching the maximum time interval.
    /// </summary>
    /// <param name="audioSource">the audio source</param>
    /// <param name="maxStopInterval">the maximum time interval</param>
    /// <remarks>
    /// The moment the audio source starts playing, the max stop timer starts,
    /// once the maximum stop interval is reached, stop the audio.
    /// </remarks>
    public static void MaxStop(AudioSource audioSource, float maxStopInterval)
    {
        if (!audioSource.isPlaying)
        {
            ResetMaxStopTimer(audioSource);
            return;
        }
        _maxStopTimerMap[audioSource] += Time.deltaTime;
        if (_maxStopTimerMap[audioSource] >= maxStopInterval)
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Play an audio given an audio source,
    /// this method automatically resets the early stop time.
    /// </summary>
    /// <param name="audioSource">The audio source.</param>
    /// <remarks>
    /// This method automatically resets the early stop time, 
    /// for example, if the player is walking, the method is called every frame the player presses the forward button,
    /// which resets the early stop timer. The moment the player stops pressing the forward button,
    /// the method stops being called, the audio will still play, the early stop counter will start.
    /// </remarks>
    public static void PlayAudio(AudioSource audioSource)
    {
        ResetEarlyStopTimer(audioSource);
        // if the audio source is not playing (idle) or if the minimum replay time has expired.
        if (!audioSource.isPlaying || IsAudioPlayable(audioSource))
        {
            if (audioSource.clip == null)
            {
                throw new ArgumentException("Invalid audio source with no audio clip!");
            }
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
