using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource button;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void playButton()
    {
        button.Play();
        Debug.Log("Audio");
    }
}
