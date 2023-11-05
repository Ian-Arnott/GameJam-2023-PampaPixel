using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Actor))]
public class Character : MonoBehaviour
{
    // HAS OBJECTIVE
    private bool _hasObjective;


    // CONTROLLERS
    private MovementController _movementController;
    private Animator _animator;
    private CharacterController _characterController;
    private Vector3 _velocity;

    // TWIST
    private bool _isTwist;

    // COOLDOWNS
    private float _twistCooldown;
    private float _twistDuration;
    private float _attackCooldown;

    private float _jumpCooldown;

    // ATTACK
    public int Damage => GetComponent<Actor>().Stats.Damage;
    private int _damage;
    [SerializeField] private GameObject _raycast;
    [SerializeField] private float _attackRange;

    // BINDING MOVEMENT KEYS
    [SerializeField] private KeyCode _moveForward = KeyCode.D;
    [SerializeField] private KeyCode _moveBack = KeyCode.A;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;

    // TWIST KEY
    [SerializeField] private KeyCode _twist = KeyCode.T;


    #region COMMANDS
    private CmdMovement _cmdMovementForward;
    private CmdMovement _cmdMovementBack;
    private CmdJump _cmdJump;
    #endregion

    void Start()
    {
        _movementController = GetComponent<MovementController>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _damage = Damage;
        _isTwist = false;
        GlobalVictory.instance.hasObjective = false;
        _attackCooldown = 0;
        _twistCooldown = 0;
        _twistDuration = 0;
        _jumpCooldown = 0;

        EventManager.instance.OnObjectivePickup += PickObjective;
        EventManager.instance.onGameWin += WinGame;

        _cmdMovementForward = new CmdMovement(_animator,_movementController, transform.forward);
        _cmdMovementBack = new CmdMovement(_animator,_movementController, -transform.forward);
        _cmdJump = new CmdJump(_movementController,_characterController);
    }

    void WinGame()
    {
        if (GlobalVictory.instance.hasObjective)
        {
            //win game
            EventManager.instance.EventGameOver(true);
        }
        else
        {
            //do nothing
        }
    }

    void PickObjective()
    {
        GlobalVictory.instance.hasObjective = true;
        _isTwist = true;
        Debug.Log("_hasObjective: " + GlobalVictory.instance.hasObjective + " _isTwist: " + _isTwist);
        EventManager.instance.EventTwist(_isTwist);
        _movementController.setForceMultiplier(2);
    }

    void Update()
    {

        if (_characterController.isGrounded) { 
            EventManager.instance.CharacterJump(0);
        }

        

        if (_attackCooldown < 0) { 
            _attackCooldown = 0; 
            EventManager.instance.CharacterAttack(_attackCooldown, 2f);
        }
        else if(_attackCooldown > 0) {
            _attackCooldown -= Time.deltaTime;
            EventManager.instance.CharacterAttack(_attackCooldown, 2f);
        }
        
        if (_twistCooldown < 0) { 
            _twistCooldown = 0; 
            EventManager.instance.CharacterTwist(_twistCooldown, 6f);
        } 
        else if (_twistCooldown > 0) { 
            _twistCooldown -= Time.deltaTime;  
            EventManager.instance.CharacterTwist(_twistCooldown, 6f);
        }

        if (_twistDuration < 0) 
        {
            if (GlobalVictory.instance.hasObjective) { _isTwist = true; _movementController.setForceMultiplier(2); } else { _isTwist = false; _movementController.setForceMultiplier(1); }

            EventManager.instance.EventTwist(_isTwist);
            _twistDuration = 0;
            
        } 
        else if(_twistDuration > 0)
        { 
            _twistDuration -= Time.deltaTime; 
        }

        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        if (Input.GetKeyDown(_twist)) {
            if (_twistCooldown <= 0)
            {
                if (GlobalVictory.instance.hasObjective) { _movementController.setForceMultiplier(1); } else { _movementController.setForceMultiplier(2); }
                _twistDuration = 6;
                _twistCooldown = 6;
                EventManager.instance.CharacterTwist(_twistCooldown, 6);
                _isTwist = !_isTwist;
                
                EventManager.instance.EventTwist(_isTwist);
            }
        }

        if (Input.GetKey(_moveForward)) 
        { 
            EventQueueManager.instance.AddEvent(_cmdMovementForward);
            transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z+1));
        }

        if (Input.GetKey(_moveBack))
        {
            EventQueueManager.instance.AddEvent(_cmdMovementBack);
            transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1));
        }

        if (_jumpCooldown > 0)
        {
            EventManager.instance.CharacterJump(1);
            _jumpCooldown -= Time.deltaTime;
            EventQueueManager.instance.AddEvent(_cmdJump);
        }else {
            _velocity.y += -10f * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        if (Input.GetKey(_jump) && _characterController.isGrounded && _jumpCooldown<=0)
        {
            _jumpCooldown = 1f; // Reset cooldown
            EventManager.instance.CharacterJump(1);
        }


        Debug.DrawRay(_raycast.transform.position, _raycast.transform.forward * _attackRange);
        if (Input.GetKeyDown(_attack) && _isTwist) {
            if (_attackCooldown <= 0)
            {
                _attackCooldown = 2f;
                EventManager.instance.CharacterAttack(_attackCooldown, 2f);
                RaycastHit hit;
                if (Physics.Raycast(_raycast.transform.position, _raycast.transform.forward, out hit, _attackRange))
                {

                    bool isEnemy = hit.transform.tag == "Enemy";
                    Debug.Log(isEnemy);
                    if (isEnemy)
                    {
                        IDamagable damagable = hit.collider.transform.GetComponent<IDamagable>();
                        if (damagable != null)
                        {
                            EventQueueManager.instance.AddEventToQueue(new CmdAttack(_animator, damagable, _damage));
                        }
                    }
                }
                else {
                    _animator.SetTrigger("Attack");
                }
            }
            
        }
        if (_characterController.velocity.z == 0) {
            _animator.SetBool("isWalking",false);
        }

    }
}
