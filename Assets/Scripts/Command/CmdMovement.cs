using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMovement : ICommand
{
    private IMoveable _moveable;
    private Vector3 _direction;
    private Animator _animator;
    public CmdMovement(Animator animator,IMoveable moveable, Vector3 direction)
    {
        _animator = animator;
        _moveable = moveable;
        _direction = direction;
    }

    public void Execute() => _moveable.Move(_animator,_direction);
}
