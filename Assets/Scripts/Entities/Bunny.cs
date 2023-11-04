using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Actor))]
public class Bunny : MonoBehaviour
{
    public int Damage => GetComponent<Actor>().Stats.Damage;
    private Animator _animator;
    private EnemyMovementController _enemyMovementController;
    private NavMeshAgent _navMeshAgent;
    private int _damage;
    private bool _isChasing;
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private float _attackRange = 2.0f; // La distancia a la que el enemigo debe detenerse y atacar

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyMovementController = GetComponent<EnemyMovementController>();
        _animator = GetComponent<Animator>();
        _isChasing = true;
        _damage = Damage;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);

        if (distanceToTarget <= _attackRange && _isChasing)
        {
            _navMeshAgent.isStopped = true;
            _animator.SetBool("isRunning", false);
            EventQueueManager.instance.AddEvent(new CmdAttack(_animator));
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterArmature|Weapon")) EventQueueManager.instance.AddEvent(new CmdApplyDamage(_target.GetComponent<IDamagable>(), _damage));
            _isChasing = false;
        }
        else if (distanceToTarget > _attackRange && !_isChasing)
        {
            _navMeshAgent.isStopped = false;
            EventQueueManager.instance.AddEvent(new CmdMovement(_animator,_enemyMovementController,_target.transform.position));
            _isChasing = true; 
        }

        if (distanceToTarget <= _attackRange)
        {
            _animator.SetTrigger("Attack"); 
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterArmature|Weapon")) EventQueueManager.instance.AddEvent(new CmdApplyDamage(_target.GetComponent<IDamagable>(), _damage));
        }

        if (_isChasing)
        {
            EventQueueManager.instance.AddEvent(new CmdMovement(_animator, _enemyMovementController, _target.transform.position));
        }
    }
}

