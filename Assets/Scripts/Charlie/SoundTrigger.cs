using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public GameObject soundPosition;

    public AudioClip sound;

    public bool playOnStart = false;

    public bool loop = false;

    public float spacialBlend = 1;

    public float volume = 1;

    public void Start()
    {
        soundPosition.AddComponent<AudioSource>();
        soundPosition.GetComponent<AudioSource>().clip = sound;
        soundPosition.GetComponent<AudioSource>().playOnAwake = playOnStart;
        soundPosition.GetComponent<AudioSource>().loop = loop;
        soundPosition.GetComponent<AudioSource>().spatialBlend = spacialBlend;
        soundPosition.GetComponent<AudioSource>().volume = volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        soundPosition.GetComponent<AudioSource>().Play();
        Destroy(this);
    }
}
