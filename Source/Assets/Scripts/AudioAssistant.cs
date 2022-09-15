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
        
        _soundSettingMap = new Dictionary<string, AudioSource>();
        var soundSettings = GameObject.Find("/SoundSettings");
        var children = soundSettings.transform.childCount;
        for (var i = 0; i < children; ++i)
        {
            _soundSettingMap[soundSettings.transform.GetChild(i).name] = soundSettings.transform.GetChild(i).GetComponent<AudioSource>();
        }

        // the mapping from the unique name of the audio location to the audio location, for quick access
        _audioLocationMap = new Dictionary<string, AudioLocation>();
        foreach (var audioLocation in audioLocations)
        {
            audioLocation.Instantiate();
            _audioLocationMap[audioLocation.uniqueAudioLocationName] = audioLocation;
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
    
    public GameObject playerController;
    private static GameObject _player;
    
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

    public AudioLocation[] audioLocations;
    
    /// <summary>
    ///  the audio location object with their corresponding unique custom name,
    /// for quick access to the public audio location array
    /// </summary>
    private static Dictionary<string, AudioLocation> _audioLocationMap;

    [Serializable]
    public class AudioLocation
    {
        // for uniquely identifying the audio location
        public string uniqueAudioLocationName;
        public GameObject audioLocation;
        public AudioSource soundSetting;

        private AudioSource _audioSource;
        private Collider _collider;
        private bool _isPlayerIn; // is the player in the collider (assume the player is out)

        public void Instantiate()
        {
            if (audioLocation.Equals(null)) audioLocation = new GameObject("AudioLocationGameObject");
            _collider = audioLocation.GetComponent<Collider>().Equals(null) ? null : audioLocation.GetComponent<Collider>();
            
            // import the sound setting if the sound setting is not null
            if (soundSetting != null)
            {
                _audioSource = UnityEngine.Object.Instantiate(soundSetting, audioLocation.transform, false);
            }
            else
            {
                _audioSource = audioLocation.AddComponent<AudioSource>();
                _audioSource.spatialBlend = audioLocation.name == "AudioLocationGameObject" ? 0f : 1.0f;
            }
        }
        
        public AudioSource GetAudioSource()
        {
            return _audioSource;
        }
        
        public Collider GetCollider()
        {
            return _collider;
        }

        public bool IsPlayerIn()
        {
            return _isPlayerIn;
        }

        public void SetPlayerIn(bool isPlayerIn)
        {
            _isPlayerIn = isPlayerIn;
        }
    }
    
    private void Start()
    {
        // for player
        var manager = playerController.GetComponent<PlayerControlManager>();
        switch (manager.controlMode)
        {
            case PlayerControlManager.controlModes.PC:
                _player = manager.pcPlayerController.transform.GetChild(1).gameObject;
                break;
            case PlayerControlManager.controlModes.OVR:
                _player = manager.ovrPlayerController.transform.GetChild(0).gameObject;
                break;
            case PlayerControlManager.controlModes.AndroidMobile:
                _player = manager.androidMobilePlayerController.transform.GetChild(0).gameObject;
                break;
            case PlayerControlManager.controlModes.Auto:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        // initialize audio prototypes
        foreach (var prototype in _audioPrototypes)
        {
            prototype.Start();
        }
    }

    private void Update()
    {
        foreach (var prototype in _audioPrototypes)
        {
            prototype.Update();
        }
    }

    #region put the prototype in the array for every customisation
    private readonly AudioPrototype[] _audioPrototypes =
    {
        new PlayerAudio()
    };
    #endregion

    /// <summary>
    /// the factory class for constructing audio prototypes
    /// </summary>
    private abstract class AudioPrototype
    {
        public abstract void Start();
        public abstract void Update();
    }

    #region add customisation here
    private class PlayerAudio : AudioPrototype
    {
        private Vector3 _lastPlayerPosition;
        
        private AudioSource _playerAudioSource;
        
        private AudioClip _walkingAudio;
        public override void Start()
        {
            _lastPlayerPosition = _player.transform.position;
            _playerAudioSource = _audioLocationMap["player audio source"].GetAudioSource();
            _walkingAudio = _audioClipMap["walk"];
        }

        public override void Update()
        {
            // walking
            if (_lastPlayerPosition.x.Equals(_player.transform.position.x) &&
                _lastPlayerPosition.z.Equals(_player.transform.position.z))
            {
                // stop player walking audio if the player is not moving horizontally
                _playerAudioSource.Stop();
            }
            else
            {
                if (!_playerAudioSource.isPlaying)
                {
                    _playerAudioSource.PlayOneShot(_walkingAudio);
                }
            }

            // reset last player position
            _lastPlayerPosition = _player.transform.position;
        }
    }
    #endregion

    public static AudioSource GetAudioSource(GameObject audioLocation, string setting)
    {
        return Instantiate(_soundSettingMap[setting], audioLocation.transform, false);
    }

    public static AudioClip GetAudioClip(string clip)
    {
        return _audioClipMap[clip];
    }
}
