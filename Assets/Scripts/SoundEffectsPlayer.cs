using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundEffectsPLayer : MonoBehaviour
{
    [Header("-------- Audio Source ---------")]
    public AudioSource src;
    public AudioSource music;

    [Header ("-------- Audio Clip ---------")]
    public AudioClip sfx1,sfx2;
    public AudioClip background;
    public AudioClip death;

    public void Button1()
    {
        src.clip = sfx1;
        src.Play();
    }
    public void Button2()
    {
        src.clip = sfx2;
        src.Play();
    }

    private void Start()
    {
        music.clip = background;
        music.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        src.PlayOneShot(clip);
    }

}
