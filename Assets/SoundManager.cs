using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource; // for sound effects
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
    }


    public void PlaySound(string clipName)
    {
        if (soundEffects.ContainsKey(clipName))
        {
            audioSource.clip = soundEffects[clipName];
            audioSource.PlayOneShot(audioSource.clip);
            //AudioSource.PlayClipAtPoint(soundEffects[clipName], Vector3.zero);
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
}