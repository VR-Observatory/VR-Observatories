using UnityEngine;

public class AudioClipAsset : MonoBehaviour
{
    // the singleton instance of the audioClipAsset class.
    public static AudioClipAsset Instance { get; private set; }
    // instantiation of the singleton object
    private void Awake() 
    { 
        AudioMaster.Instantiate();
        // get instance
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    // the array of audio tuples
    public AudioTuple[] audioTuples;
    
    // an audio tuple is a combination of the audio type, audio source and the audio clip
    [System.Serializable]
    public class AudioTuple
    {
        // the name of the audio, i.e., the enumeration of the audio in the AudioMaster class
        public AudioMaster.AudioType audioType;
        // the audio source on the scene
        // useful for a single pair of audio source and audio clip
        // not applicable for multiple audio sources with a single audio clip
        // or an audio clip without an audio source
        public AudioSource audioSource;
        // the corresponding audio clip
        public AudioClip audioClip;
    }
}
