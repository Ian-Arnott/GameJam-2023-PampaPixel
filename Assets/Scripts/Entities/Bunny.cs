using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Actor))]
public class Bunny : MonoBehaviour
{

    private float _attackCooldown;

    public int Damage => GetComponent<Actor>().Stats.Damage;
    private Animator _animator;
    private EnemyMovementController _enemyMovementController;
    private NavMeshAgent _navMeshAgent;
    private int _damage;
    private bool _isTwist;

    [SerializeField]
    private GameObject _area; // cube where it is

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
        _attackCooldown = 0;
    }

    void Twist(bool isTwist)
    {
        _isTwist = isTwist;
        Debug.Log(_isTwist);
    }

    void Update()
    {
        bool playerInArea = _area.GetComponent<Collider>().bounds.Contains(_target.transform.position);

        if (playerInArea)
        {
            float distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
            if (_attackCooldown < 0) { _attackCooldown = 0; } else if (_attackCooldown > 0) { _attackCooldown -= Time.deltaTime; }

            if (distanceToTarget <= _attackRange && _isTwist)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("isRunning", false);
                if (_attackCooldown == 0)
                {
                    _attackCooldown = 1f;
                    EventQueueManager.instance.AddEventToQueue(new CmdAttack(_animator, _target.GetComponent<IDamagable>(), _damage));
                    // EventQueueManager.instance.AddEvent( new CmdApplyDamage(_target.GetComponent<IDamagable>(), _damage));
                }

            }
            else if (distanceToTarget > _attackRange && _isTwist)
            {
                _navMeshAgent.isStopped = false;
                EventQueueManager.instance.AddEvent(new CmdMovement(_animator, _enemyMovementController, _target.transform.position));
            }
            else if (!_isTwist)
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("isRunning", false);
            }


        }
    }
}

