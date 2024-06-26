using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Actor))]
public class Character : MonoBehaviour
{
    // BOOLEANS
    private bool _isGrounded;
    [SerializeField] private bool _isTwist;
    private bool _isJumping;
    
    // GROUND COLLIDER
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private LayerMask _groundMask; 

    // MOVEMENT
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _playerGravity;
    private Vector3 _velocity;

    // STATS
    public float Speed => GetComponent<Actor>().Stats.MovementSpeed;
    public float JumpForce => GetComponent<Actor>().Stats.JumpForce;

    // TWIST
    [SerializeField] private float _twistCooldown;
    private float _twistDuration;

    // INPUTS
    private float _xInput;

    // AUDIO
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _hurtClip;
    [SerializeField] private AudioClip _twistClip;
    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        EventManager.instance.EventTwist(_isTwist);
        _isJumping = false;
        _twistDuration = 0;
        EventManager.instance.OnObjectivePickup += PickObjective;
        EventManager.instance.OnCharacterLifeChange += DamageHandler;
        EventManager.instance.onGameWin += WinGame;

        // Initialize AudioSource
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void WinGame()
    {
        if (GlobalManager.instance.hasObjective) EventManager.instance.EventGameOver(true);
    }

    void PickObjective()
    {
        GlobalManager.instance.hasObjective = true;
        _isTwist = true;
        EventManager.instance.EventTwist(_isTwist);
        PlayAudioClip(_twistClip);
    }

    void Update()
    {
        if (_isGrounded && _velocity.y < 0) _velocity.y = 0f;
        GetInput();
        MoveWithInput();
        HandleJump();
        _velocity.y += -_playerGravity*Time.deltaTime;
        _characterController.Move(_velocity*Time.deltaTime);
        if (_isGrounded) EventManager.instance.CharacterJump(0);
        else EventManager.instance.CharacterJump(100);
        HandleTwist();
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    void GetInput()
    {
        _xInput = Input.GetAxis("Horizontal");
    }

    void MoveWithInput()
    {
        if (Mathf.Abs(_xInput) > 0) 
        { 
            _characterController.Move(new Vector3(0,0,_xInput*Time.deltaTime*Speed));
            _animator.SetBool("isWalking",true);
            float sign = Mathf.Sign(_xInput);
            transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z + sign));
        }
        else _animator.SetBool("isWalking",false);
    }

    void HandleJump()
    {
        if (_isJumping) { return; }
        if (Input.GetButtonDown("Jump") && _isGrounded) 
        { 
            _isJumping = true;
            _velocity.y += Mathf.Sqrt(JumpForce * -1.0f * -_playerGravity);
            PlayAudioClip(_jumpClip);
        }
    }

    void CheckGround() {
        bool aux = _isGrounded;
        _isGrounded = Physics.OverlapBox(_boxCollider.bounds.center,_boxCollider.bounds.extents, _boxCollider.transform.rotation ,_groundMask).Length > 0;
        if (_isGrounded != aux && _isGrounded && _isJumping) { _isJumping = false; }
    }

    void HandleTwist()
    {
        if (_twistDuration < 0) 
        {
            _isTwist = !_isTwist;
            EventManager.instance.EventTwist(_isTwist);
            _twistDuration = 0;   
        } 
        else if(_twistDuration > 0)
        { 
            _twistDuration -= Time.deltaTime; 
            EventManager.instance.CharacterTwist(_twistDuration, _twistCooldown);
        }
        if (Input.GetButtonDown("Twist"))
        {
            if (_twistDuration <= 0)
            {
                _twistDuration = _twistCooldown;
                _isTwist = !_isTwist;
                EventManager.instance.CharacterTwist(_twistDuration, _twistCooldown);
                EventManager.instance.EventTwist(_isTwist);
                PlayAudioClip(_twistClip);
            }
        }
    }

    void DamageHandler(float currentLife, float maxLife)
    {
        _animator.SetTrigger("TakeDamage");
        PlayAudioClip(_hurtClip);
    }

    void PlayAudioClip(AudioClip clip)
    {
        if (clip != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
