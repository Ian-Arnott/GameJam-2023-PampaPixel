using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Character : MonoBehaviour
{
    public int Damage => GetComponent<Actor>().Stats.Damage;
    private MovementController _movementController;
    private Animator _animator;
    private CharacterController _characterController;
    private Vector3 _velocity;
    [SerializeField]
    private GameObject _target;
    private int _damage; 
    // BINDING MOVEMENT KEYS
    [SerializeField] private KeyCode _moveForward = KeyCode.D;
    [SerializeField] private KeyCode _moveBack = KeyCode.A;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;

    // TWIST KEY
    // [SerializeField] private KeyCode _twist = KeyCode.T;


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

        _cmdMovementForward = new CmdMovement(_animator,_movementController, transform.forward);
        _cmdMovementBack = new CmdMovement(_animator,_movementController, -transform.forward);
        _cmdJump = new CmdJump(_movementController,_characterController);
    }

    void Update()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddEvent(_cmdMovementForward);
        if (Input.GetKey(_moveBack)) EventQueueManager.instance.AddEvent(_cmdMovementBack);
        if (Input.GetKey(_jump)) EventQueueManager.instance.AddEvent(_cmdJump);
        if (Input.GetKeyDown(_attack)) {
            EventQueueManager.instance.AddEvent(new CmdAttack(_animator));
            EventQueueManager.instance.AddEvent(new CmdApplyDamage(_target.GetComponent<IDamagable>(), _damage));
        }
        if (_characterController.velocity.z == 0) {
            _animator.SetBool("isWalking",false);
        }
        // /* Gameover Test */
        // if (Input.GetKeyDown(KeyCode.Return)) EventManager.instance.EventGameOver(true);
        _velocity.y += -10f * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
