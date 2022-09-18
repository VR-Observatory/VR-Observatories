using System;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    // access the audio master via the singleton instance
    private static AudioMaster Instance { get; set; }
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public enum AudioAction
    {
        None,
        Play,
        PlayOneShot,
        Pause,
        Stop
    }

    public enum EventType
    {
        None, // for the audio without a game object
        OnTransform, // if the game object is transforming.
        OnTransformStart, // then the game object starts transforming
        OnTransformStop, // if the game object stops transforming.
        OnTriggerEnter, // if the player enters the collision area.
        OnTriggerExit, // if the player exits the collision area.
        OnTriggerStay // if the player stays in the collision area.
    }
    
    [Serializable]
    public class AudioEvent : AudioEventObject { }

    [Serializable]
    public class AudioEventObject
    {
        public AudioClip audioClip; // the audio clip to be referenced if the type of the event is triggered
        public EventType eventType; // the event type, see the EventType enumeration
        public AudioAction audioAction; // the action for the audio clip, such as play, pause or stop if the event is triggered 
    }

    [Serializable]
    public class AudioGameObject
    {
        public GameObject audioLocation; // the game object with the audio source
        public AudioSource soundSetting;
            
        private AudioSource _audioSource;
        private Collider _collider;
        private bool _isPlayerIn; // is the player in the collider (assume the player is out)
        private bool _isTransforming; // if the game object is in the process of transforming
        
        public void Instantiate(Sound sound)
        {
            // assign the audio location to a game object if the audio location is null
            if (audioLocation.Equals(null)) audioLocation = new GameObject("AudioLocationGameObject");
            _collider = audioLocation.GetComponent<Collider>().Equals(null) ? null : audioLocation.GetComponent<Collider>();
            if (sound.hasSingularSetting)
            {
                soundSetting = sound.soundSetting;
            }
            if (sound.hasSingularSource)
            {
                // assume singular source if the parameter is not null 
                _audioSource = sound.GetAudioSource();
                _audioSource.spatialBlend = sound.singularLocation.name == "SingularSound" ? 0f : 1.0f;
            }
            else
            {
                if (soundSetting != null)
                {
                    _audioSource = UnityEngine.Object.Instantiate(soundSetting, audioLocation.transform, false);
                }
                else
                {
                    // the audio source is on audio game object if the parameter is null
                    _audioSource = audioLocation.AddComponent<AudioSource>();
                    _audioSource.spatialBlend = audioLocation.name == "AudioLocationGameObject" ? 0f : 1.0f;
                }
            }
            audioLocation.transform.hasChanged = false;
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

        public bool IsTransforming()
        {
            return _isTransforming;
        }

        public void SetTransforming(bool isTransforming)
        {
            _isTransforming = isTransforming;
        }
    }
    
    [Serializable]
    public class Controller
    {
        public AudioGameObject[] audioGameObjects;
        public AudioEvent[] audioEvents;

        public void Instantiate(Sound sound)
        {
            foreach (var audioGameObject in audioGameObjects)
            {
                audioGameObject.Instantiate(sound);
            }
        }
    }

    [Serializable]
    public class AudioController : Controller { }

    public GameObject playerController;
    public Sound[] sounds;
    private static GameObject _player;

    [Serializable]
    public class Sound
    {
        public string name; // the name of the sound group, for keeping track of everything
        public bool hasSingularSource; // all audio controllers will have a singular audio source if this is true
        public GameObject singularLocation;
        public bool hasSingularSetting;
        public AudioSource soundSetting;
        public AudioController[] audioControllers;
        
        private AudioSource _audioSource;
        
        public void Instantiate()
        {
            if (hasSingularSource)
            {
                if (singularLocation.Equals(null))
                {
                    singularLocation = new GameObject("SingularSound"); 
                    if (soundSetting != null)
                    {
                        _audioSource = UnityEngine.Object.Instantiate(soundSetting, singularLocation.transform, false);
                    }
                    else
                    {
                        _audioSource = singularLocation.AddComponent<AudioSource>();
                        _audioSource.spatialBlend = 0f;
                    }
                }
                else
                {
                    if (soundSetting != null)
                    {
                        _audioSource = UnityEngine.Object.Instantiate(soundSetting, singularLocation.transform, false);
                    }
                    else
                    {
                        _audioSource = singularLocation.AddComponent<AudioSource>();
                        _audioSource.spatialBlend = 1.0f;
                    }
                }
                singularLocation.transform.hasChanged = false;
            }
        }

        public AudioSource GetAudioSource()
        {
            return _audioSource;
        }
    }

    private void Start()
    {
        PlayerControlManager manager = playerController.GetComponent<PlayerControlManager>();
        switch (manager.controlMode)
        {
            case PlayerControlManager.controlModes.PC:
                _player = GameObject.Find("PCPlayerController");
                break;
            case PlayerControlManager.controlModes.OVR:
                _player = GameObject.Find("OVRPlayerControllerHands");
                break;
            case PlayerControlManager.controlModes.AndroidMobile:
                _player = GameObject.Find("AndroidMobileController");
                break;
            case PlayerControlManager.controlModes.Auto:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        foreach (var sound in sounds)
        {
            sound.Instantiate();
            foreach (var audioController in sound.audioControllers)
            {
                audioController.Instantiate(sound);
            }
        }
    }

    private void Update()
    {
        // iterate over the sound
        foreach (var sound in sounds)
        {
            foreach (var audioController in sound.audioControllers) 
            {
                foreach (var audioGameObject in audioController.audioGameObjects)
                {
                    foreach (var audioEvent in audioController.audioEvents)
                    {
                        var triggerEvent = false;
                        switch (audioEvent.eventType)
                        {
                            case EventType.None:
                                break;
                            case EventType.OnTransform:
                                if (audioGameObject.audioLocation.transform.hasChanged)
                                {
                                    audioGameObject.SetTransforming(true);
                                    triggerEvent = true;
                                }
                                break;
                            case EventType.OnTransformStart:
                                if (audioGameObject.audioLocation.transform.hasChanged)
                                {
                                    if (!audioGameObject.IsTransforming())
                                    {
                                        audioGameObject.SetTransforming(true);
                                        triggerEvent = true;
                                    }
                                }
                                else
                                {
                                    audioGameObject.SetTransforming(false);
                                }
                                break;
                            case EventType.OnTransformStop:
                                if (!audioGameObject.audioLocation.transform.hasChanged)
                                {
                                    if (audioGameObject.IsTransforming())
                                    {
                                        audioGameObject.SetTransforming(false);
                                        triggerEvent = true;
                                    }
                                }
                                break;
                            case EventType.OnTriggerEnter:
                                // trigger the event only if the player is out (on the last frame)
                                if (!audioGameObject.IsPlayerIn())
                                {
                                    // if enter then trigger the audio event
                                    if (audioGameObject.GetCollider().bounds.Contains(_player.transform.position))
                                    {
                                        triggerEvent = true;
                                        audioGameObject.SetPlayerIn(true);
                                    }
                                }
                                break;
                            case EventType.OnTriggerExit:
                                // trigger the event only if the player is in (on the last frame)
                                if (audioGameObject.IsPlayerIn())
                                {
                                    // if exit then trigger the audio event
                                    if (!audioGameObject.GetCollider().bounds.Contains(_player.transform.position))
                                    {
                                        triggerEvent = true;
                                        audioGameObject.SetPlayerIn(false);
                                    }
                                }
                                break;
                            case EventType.OnTriggerStay:
                                // if  the player is in the trigger area
                                if (audioGameObject.GetCollider().bounds.Contains(_player.transform.position))
                                {
                                    triggerEvent = true;
                                    audioGameObject.SetPlayerIn(true);
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        
                        // handle audio event
                        if (triggerEvent)
                        {
                            switch (audioEvent.audioAction)
                            {
                                case AudioAction.None:
                                    break;
                                case AudioAction.Play:
                                    if (!audioGameObject.GetAudioSource().isPlaying)
                                    {
                                        audioGameObject.GetAudioSource().clip = audioEvent.audioClip;
                                        audioGameObject.GetAudioSource().Play();
                                    }
                                    break;
                                case AudioAction.PlayOneShot:
                                    if (!audioGameObject.GetAudioSource().isPlaying)
                                    {
                                        audioGameObject.GetAudioSource().PlayOneShot(audioEvent.audioClip);
                                    }
                                    break;
                                case AudioAction.Pause:
                                    audioGameObject.GetAudioSource().Pause();
                                    break;
                                case AudioAction.Stop:
                                    audioGameObject.GetAudioSource().Stop();
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }
                }
            }
        }
        
        // reset transform status to false
        foreach (var sound in sounds)
        {
            foreach (var audioController in sound.audioControllers)
            {
                foreach (var audioGameObject in audioController.audioGameObjects)
                {
                    foreach (var unused in audioController.audioEvents)
                    {
                        audioGameObject.audioLocation.transform.hasChanged = false;
                    }
                }
            }
        }
    }
}
