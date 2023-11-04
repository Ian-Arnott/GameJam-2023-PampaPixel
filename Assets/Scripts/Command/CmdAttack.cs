using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdAttack : ICommand
{
    private Animator _animator;

    public CmdAttack(Animator animator)
    {
        _animator = animator;
    }

    public void Execute() 
    {
        _animator.SetTrigger("Attack");
    }
}
