using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private MovementController _movementController;
    private Animator _animator;
    private CharacterController _characterController;
    private Vector3 _velocity;
    // BINDING ATTACK KEYS

    // BINDING MOVEMENT KEYS
    [SerializeField] private KeyCode _moveForward = KeyCode.D;
    [SerializeField] private KeyCode _moveBack = KeyCode.A;
    [SerializeField] private KeyCode _jump = KeyCode.Space;

    // TWIST KEY
    // [SerializeField] private KeyCode _twist = KeyCode.T;


    #region COMMANDS
    private CmdMovement _cmdMovementForward;
    private CmdMovement _cmdMovementBack;
    private CmdJump _cmdJump;


    // private CmdAttack _cmdAttack;
    // private CmdApplyDamage _cmdApplyDamage;

    #endregion

    void Start()
    {
        _movementController = GetComponent<MovementController>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _cmdMovementForward = new CmdMovement(_animator,_movementController, transform.forward);
        _cmdMovementBack = new CmdMovement(_animator,_movementController, -transform.forward);
        _cmdJump = new CmdJump(_movementController,_characterController);
        // _cmdApplyDamage = new CmdApplyDamage(GetComponent<IDamagable>(), 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddEvent(_cmdMovementForward);
        if (Input.GetKey(_moveBack)) EventQueueManager.instance.AddEvent(_cmdMovementBack);
        if (Input.GetKey(_jump)) EventQueueManager.instance.AddEvent(_cmdJump);
        // else _animator.SetBool("isWalking", false);
        // /* Gameover Test */
        // if (Input.GetKeyDown(KeyCode.Return)) EventManager.instance.EventGameOver(true);
        // /* Lifebar Test */
        // if (Input.GetKeyDown(KeyCode.Backspace)) EventQueueManager.instance.AddEventToQueue(_cmdApplyDamage);
        _velocity.y += -10f * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
