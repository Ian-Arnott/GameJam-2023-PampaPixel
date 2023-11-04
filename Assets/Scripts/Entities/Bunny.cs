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
    private bool _isTwist;
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private float _attackRange = 2.0f; // La distancia a la que el enemigo debe detenerse y atacar

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyMovementController = GetComponent<EnemyMovementController>();
        _animator = GetComponent<Animator>();
        _isTwist = false;
        _damage = Damage;
        EventManager.instance.OnTwist += Twist;
    }

    void Twist(bool isTwist) {
        _isTwist = isTwist;
        Debug.Log(_isTwist);
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);

        if (distanceToTarget <= _attackRange && _isTwist)
        {
            _navMeshAgent.isStopped = true;
            _animator.SetBool("isRunning", false);
            EventQueueManager.instance.AddEvent(new CmdAttack(_animator));
            if(_animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterArmature|Weapon")) EventQueueManager.instance.AddEvent(new CmdApplyDamage(_target.GetComponent<IDamagable>(), _damage));
        }
        else if (distanceToTarget > _attackRange && _isTwist)
        {
            _navMeshAgent.isStopped = false;
            EventQueueManager.instance.AddEvent(new CmdMovement(_animator,_enemyMovementController,_target.transform.position));
        }

    }
}

