using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAudio : MonoBehaviour, IListenable
{
    public AudioClip AudioClip => _audioClip;
    [SerializeField] private AudioClip _audioClip;
    public AudioSource AudioSource => _audioSource;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _area;
    private bool playerInArea;

    public void InitAudioSource()
    {
        EventManager.instance.OnTwist += OnTwist;
        playerInArea = false;
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
        if (playerInArea)
        {
            PlayOneShot();
        }
    }

    void Update() 
    {
        if(_target!=null) 
            playerInArea = _area.GetComponent<Collider>().bounds.Contains(_target.transform.position);
    }
}
