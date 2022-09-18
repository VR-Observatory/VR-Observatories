using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// for additional customisation
/// </summary>
public class AudioAssistant : MonoBehaviour
{
    // access the audio assistant via the singleton instance
    private static AudioAssistant Instance { get; set; }

    private void Awake()
    {
        // the mapping from the filename of the audio clip to the audio clip, for quick access
        _audioClipMap = new Dictionary<string, AudioClip>();
        foreach (var audioClip in audioClips)
        {
            _audioClipMap[audioClip.name] = audioClip;
        }
        
        // the mapping from the game object's name to the sound setting
        _soundSettingMap = new Dictionary<string, AudioSource>();
        var soundSettings = GameObject.Find("/SoundSettings");
        var children = soundSettings.transform.childCount;
        for (var i = 0; i < children; ++i)
        {
            _soundSettingMap[soundSettings.transform.GetChild(i).name] =
                soundSettings.transform.GetChild(i).GetComponent<AudioSource>();
        }

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public AudioClip[] audioClips;

    /// <summary>
    ///  the audio clip with their corresponding (unique) filename,
    /// for quick access to the public audio clip array
    /// </summary>
    private static Dictionary<string, AudioClip> _audioClipMap;

    /// <summary>
    /// the audio setting with their corresponding (unique) name
    /// </summary>
    private static Dictionary<string, AudioSource> _soundSettingMap;

    public static AudioSource GetAudioSource(string setting = "Default", GameObject audioLocation = null)
    {
        if (audioLocation == null) audioLocation = new GameObject("AudioLocationGameObject");
        return Instantiate(_soundSettingMap[setting], audioLocation.transform, false);
    }

    public static AudioClip GetAudioClip(string clip)
    {
        return _audioClipMap[clip];
    }
}
