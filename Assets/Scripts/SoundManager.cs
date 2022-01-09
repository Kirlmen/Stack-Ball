using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource audioSource;
    public bool sound = true;


    void Awake()
    {
        Singleton();
        audioSource = GetComponent<AudioSource>();

    }


    void Singleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {

    }

    public void PlaySoundFX(AudioClip clip, float volume)
    {
        if (sound)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
