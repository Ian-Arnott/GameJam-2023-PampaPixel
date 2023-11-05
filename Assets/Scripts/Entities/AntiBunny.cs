using System.Collections;
using UnityEngine;
using UnityEngine.AI;


    public class AntiBunny : MonoBehaviour
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
    [SerializeField] private GameObject _startPoint; // starting position
    private bool _isHappy;

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
    }

    void Update()
    {
        bool atStartPoint = _startPoint.GetComponent<Collider>().bounds.Contains(transform.position);
        bool playerInArea = _area.GetComponent<Collider>().bounds.Contains(_target.transform.position);


        if (_isTwist)
        {
            if (atStartPoint)
            {
                // set happy
                if (!_isHappy)
                {
                    _navMeshAgent.isStopped = true;
                    _animator.SetBool("isRunning", false);
                    _animator.SetBool("isHappy", true);
                    _isHappy = true;
                }
                if (playerInArea) { transform.LookAt(_target.transform.position); }

                // transform rotation facing _target.transform.position

            }
            else
            {
                // go to startPoint
                _animator.SetBool("isHappy", false);
                _isHappy = false;
                _navMeshAgent.isStopped = false;
                EventQueueManager.instance.AddEvent(new CmdMovement(_animator, _enemyMovementController, _startPoint.transform.position));

            }
        }
        else
        {
            if (_isHappy)
            {
                _animator.SetBool("isHappy", false);
                _isHappy = false;
            }

            _animator.SetBool("isHappy", false);

            if (playerInArea)
            {
                float distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
                if (_attackCooldown < 0) { _attackCooldown = 0; } else if (_attackCooldown > 0) { _attackCooldown -= Time.deltaTime; }

                if (distanceToTarget <= _attackRange && !_isTwist)
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
                else if (distanceToTarget > _attackRange && !_isTwist)
                {
                    _navMeshAgent.isStopped = false;
                    EventQueueManager.instance.AddEvent(new CmdMovement(_animator, _enemyMovementController, _target.transform.position));
                }
                else if (_isTwist)
                {
                    _navMeshAgent.isStopped = true;
                    _animator.SetBool("isRunning", false);
                }


            }
        }



    }
}
