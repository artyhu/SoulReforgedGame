using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource source;
    [SerializeField]
    private AudioClip playerSlash, cloneSlash, zap, fireball, hurt, 
        running, zapSwitch, fbSwitch, heal, dash;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    
    public void PlayAudio(string clip)
    {
        switch (clip)
        {
            case "r":
                source.clip = running;
                source.volume = 1f;
                break;
            case "ps":
                source.clip = playerSlash;
                source.volume = 0.5f;
                break;
            case "cs":
                source.clip = cloneSlash;
                source.volume = 1f;
                break;
            case "d":
                source.clip = dash;
                source.volume = 1f;
                break;
            case "z":
                source.clip = zap;
                source.volume = 0.7f;
                break;
            case "fb":
                source.clip = fireball;
                source.volume = 0.8f;
                break;
            case "heal":
                source.clip = heal;
                source.volume = 1f;
                break;
            case "hurt":
                source.clip = hurt;
                source.volume = 0.7f;
                break;
            case "zs":
                source.clip = zapSwitch;
                source.volume = 1f;
                break;
            case "fbs":
                source.clip = fbSwitch;
                source.volume = 1f;
                break;
        }

        Debug.Log(clip);
        source.Play();

    }
}
