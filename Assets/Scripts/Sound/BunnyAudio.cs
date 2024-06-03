using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAudio : MonoBehaviour, IListenable
{
    public AudioClip AudioClip => _audioClip;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip _audioClipTwist;
    public AudioSource AudioSource => _audioSource;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float cooldown;
    private float currentCooldown;


    public void InitAudioSource()
    {
        EventManager.instance.OnTwist += OnTwist;
        currentCooldown = cooldown;
    }

    public void Play()
    {
        AudioSource.Play();
    }

    public void PlayOneShot()
    {
        AudioSource.PlayOneShot(_audioClip);
    }

    public void Stop()
    {
        AudioSource.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitAudioSource();
    }

    void OnTwist(bool twist)
    {
        if (twist)
        {
            AudioSource.clip = _audioClipTwist;
        }
        else
        {
            AudioSource.clip = _audioClip;
        }
    }

    void Update() {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            currentCooldown = cooldown;
            Play();
        }

    }
}
