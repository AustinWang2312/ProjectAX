using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;


    //audio pool
    private List<AudioSource> normalPriorityPool = new List<AudioSource>();
    private List<AudioSource> highPriorityPool = new List<AudioSource>();
    private int poolSize = 7; // total pool size, adjust this to fit your needs
    private int highPriorityPoolSize = 25; // adjust this to fit your needs


    public AudioSource footStepAudioSource; // for sound effects

    public AudioSource musicSource; // for music

    public List<AudioClip> audioClips; // assign your AudioClips in the Unity editor
    public List<string> clipNames; // names for the AudioClips

    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    void Awake()
    {
        // Singleton pattern to ensure only one AudioManager instance exists
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // Populate the Dictionary
        for (int i = 0; i < Mathf.Min(audioClips.Count, clipNames.Count); i++)
        {
            soundEffects[clipNames[i]] = audioClips[i];
        }


        // Initialize the AudioSource pools
        for (int i = 0; i < highPriorityPoolSize; i++)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.spatialBlend = 0f; // Make the sound 2D

            if (i < poolSize)
            {
                normalPriorityPool.Add(newAudioSource);
                
            }
            else
            {
                highPriorityPool.Add(newAudioSource);
            }
        }
    }


    public void PlaySound(string clipName, bool highPriority = true)
    {
        AudioSource freeAudioSource = GetFreeAudioSource(highPriority);


        //Abort if no free audio sources
        if(!freeAudioSource)
        {
            Debug.Log("No free AudioSource available");
            return;
        }

        if (soundEffects.ContainsKey(clipName))
        {
            freeAudioSource.clip = soundEffects[clipName];

            freeAudioSource.PlayOneShot(freeAudioSource.clip);
        }
        else
        {
            Debug.Log("No sound found with the name " + clipName);
        }
    }

    public void PlayFootStepSound()
    {
        string clipName = "Footstep";
        if (soundEffects.ContainsKey(clipName))
        {
            footStepAudioSource.clip = soundEffects[clipName];

            //Introduce Variation of Sound
            footStepAudioSource.pitch = 1.0f + Random.Range(-0.3f, 0.3f);

            footStepAudioSource.PlayOneShot(footStepAudioSource.clip);
        }
        else
        {
            Debug.Log("No sound found with the name " + clipName);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    private AudioSource GetFreeAudioSource(bool highPriority)
    {
        List<AudioSource> poolToUse = highPriority ? highPriorityPool : normalPriorityPool;

        foreach (AudioSource audioSource in poolToUse)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        return null;
    }
}