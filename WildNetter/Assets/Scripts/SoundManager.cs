using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    //Varaibles:
    float volume;
    //Component References:
    public AudioSource _audioSource;
    //Script References:
  

    // Collections:
    public static Dictionary<string, AudioClip> soundDictionary;
    //Getters & Setters:
    public float SetGetVolume {
        get { return volume; }
        set { volume = value; }
    }
    //Functions:
    public override void Init()
    {
        
    }
    public void PlaySound(string sound) { }
}
