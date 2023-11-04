using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Actor))]

public class EnemyMovementController : MonoBehaviour, IMoveable
{
    private NavMeshAgent agent;

    #region IMOVEABLE_PROPERTIES
    public float Speed => GetComponent<Actor>().Stats.MovementSpeed;

    public void Move(Animator animator, Vector3 direction)
    {
        animator.SetBool("isRunning", true);
        agent.destination = direction;
    }
    #endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }
}
