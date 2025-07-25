using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages audio playback in the game
public class AudioManagement : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;  // Source for background music
    [SerializeField] AudioSource SFXSource;    // Source for sound effects

    [Header("Audio Clip")]
    public AudioClip background;  // Background music clip
    public AudioClip click;       // Click sound effect clip

    // Start is called before the first frame update
    private void Start()
    {
        // Set and play the background music when the game starts
        musicSource.clip = background;
        musicSource.Play();
    }

    // Method to play a sound effect
    public void PlaySFX(AudioClip clip)
    {
        // Play the given sound effect once
        SFXSource.PlayOneShot(clip);
    }
}
