using System;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;

    public static VolumeControl Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void SetSound(float volume)
    {
        mixer.SetFloat("globalVolume", volume);
    }
}
