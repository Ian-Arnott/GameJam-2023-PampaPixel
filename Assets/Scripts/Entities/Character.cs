using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private MovementController _movementController;
    
    // BINDING ATTACK KEYS

    // BINDING MOVEMENT KEYS
    [SerializeField] private KeyCode _moveForward = KeyCode.D;
    [SerializeField] private KeyCode _moveBack = KeyCode.A;

    // TWIST KEY
    // [SerializeField] private KeyCode _twist = KeyCode.T;


    #region COMMANDS
    private CmdMovement _cmdMovementForward;
    private CmdMovement _cmdMovementBack;


    // private CmdAttack _cmdAttack;
    // private CmdApplyDamage _cmdApplyDamage;

    #endregion

    void Start()
    {
        _movementController = GetComponent<MovementController>();

        _cmdMovementForward = new CmdMovement(_movementController, transform.forward);
        _cmdMovementBack = new CmdMovement(_movementController, -transform.forward);
        // _cmdApplyDamage = new CmdApplyDamage(GetComponent<IDamagable>(), 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddEvent(_cmdMovementForward);
        if (Input.GetKey(_moveBack)) EventQueueManager.instance.AddEvent(_cmdMovementBack);

        // /* Gameover Test */
        // if (Input.GetKeyDown(KeyCode.Return)) EventManager.instance.EventGameOver(true);
        // /* Lifebar Test */
        // if (Input.GetKeyDown(KeyCode.Backspace)) EventQueueManager.instance.AddEventToQueue(_cmdApplyDamage);
    }
}
