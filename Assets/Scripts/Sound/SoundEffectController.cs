using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour, IListenable
{
    #region IListenable_Properties
    public AudioClip AudioClip => _audioClip;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip _defeatAudioClip;

    public AudioSource AudioSource => _audioSource;
    private AudioSource _audioSource;
    #endregion

    #region IListenable_Methods
    public void InitAudioSource()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = AudioClip;
    }

    public void PlayOneShot() => AudioSource.PlayOneShot(AudioClip);

    public void Play() => AudioSource.Play();

    public void Stop() => AudioSource.Stop();
    #endregion

    #region UNITY_EVENTS
    void Start()
    {
        InitAudioSource();
        EventManager.instance.OnTwist += OnTwist;
    }
    #endregion

    #region EVENT_ACTIONS
    private void OnTwist(bool twist)
    {
        if (twist)
        {
            AudioSource.clip = _defeatAudioClip;
        }
        else
        {
            AudioSource.clip = _audioClip;
        }

        AudioSource.loop = true;

        AudioSource.Play();
    }

    #endregion
}
