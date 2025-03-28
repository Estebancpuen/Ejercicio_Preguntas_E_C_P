using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{
    private AudioSource music;
    public AudioClip clickAudio;
    public AudioClip switchAudio;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();

    }

    public void ClickAudio()
    {
        music.PlayOneShot(clickAudio);
    }

    public void SwitchAudio()
    {
        music.PlayOneShot(switchAudio);
    }




}
