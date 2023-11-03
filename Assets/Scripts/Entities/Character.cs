using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private MovementController _movementController;
    private Rigidbody _rb;
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
        _rb = GetComponent<Rigidbody>();

        _cmdMovementForward = new CmdMovement(_movementController, transform.forward);
        _cmdMovementBack = new CmdMovement(_movementController, -transform.forward);
        _cmdJump = new CmdJump(_movementController,_rb);
        // _cmdApplyDamage = new CmdApplyDamage(GetComponent<IDamagable>(), 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddEvent(_cmdMovementForward);
        if (Input.GetKey(_moveBack)) EventQueueManager.instance.AddEvent(_cmdMovementBack);
        if (Input.GetKey(_jump)) EventQueueManager.instance.AddEvent(_cmdJump);

        // /* Gameover Test */
        // if (Input.GetKeyDown(KeyCode.Return)) EventManager.instance.EventGameOver(true);
        // /* Lifebar Test */
        // if (Input.GetKeyDown(KeyCode.Backspace)) EventQueueManager.instance.AddEventToQueue(_cmdApplyDamage);
    }
}
