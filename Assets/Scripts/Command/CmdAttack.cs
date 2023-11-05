using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdAttack : ICommand
{
    private Animator _animator;
    private IDamagable _damageable;
    private int _damage;
    public CmdAttack(Animator animator, IDamagable damagable, int damage)
    {
        _animator = animator;
        _damageable = damagable;
        _damage = damage;
    }

    public void Execute() 
    {
        _animator.SetTrigger("Attack");
        _damageable.TakeDamage(_damage);
    }
}
