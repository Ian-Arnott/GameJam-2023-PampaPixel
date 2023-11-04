using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bunny : MonoBehaviour
{
    private Animator _animator;
    private EnemyMovementController _enemyMovementController;
    private NavMeshAgent _navMeshAgent;
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
        _isChasing = true; // Inicialmente el enemigo está en modo persecución
    }

    void Update()
    {
        // Calcula la distancia al objetivo
        float distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);

        // Si el objetivo está dentro del rango de ataque y estamos en modo persecución
        if (distanceToTarget <= _attackRange && _isChasing)
        {
            // Detén al enemigo y prepara el ataque
            _navMeshAgent.isStopped = true;
            _animator.SetBool("isRunning", false);
            _animator.SetTrigger("Attack"); // Asumiendo que tienes una animación de ataque
            _isChasing = false; // Cambia el estado a no perseguir
        }
        // Si el objetivo está fuera del rango de ataque y no estamos en modo persecución
        else if (distanceToTarget > _attackRange && !_isChasing)
        {
            // Reanuda la persecución
            _navMeshAgent.isStopped = false;
            EventQueueManager.instance.AddEvent(new CmdMovement(_animator,_enemyMovementController,_target.transform.position));
            _isChasing = true; // Cambia el estado a perseguir
        }

        if (distanceToTarget <= _attackRange)
        {
            _animator.SetTrigger("Attack"); // Asumiendo que tienes una animación de ataque
        }

        // Si estamos en modo persecución, actualiza el destino del agente de navegación
        if (_isChasing)
        {
            EventQueueManager.instance.AddEvent(new CmdMovement(_animator, _enemyMovementController, _target.transform.position));
        }
    }
}

