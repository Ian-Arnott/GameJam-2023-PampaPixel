using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Actor))]
public class Spiky : MonoBehaviour
{

    public int Damage => GetComponent<Actor>().Stats.Damage;
    private UnityEngine.AI.NavMeshAgent _agent;
    [SerializeField]
    private List<GameObject> _waypoints;
    [SerializeField]
    private float _stoppingRange = 2.0f;
    [SerializeField]
    private GameObject _damagable;
    private int index;
    private int _damage;
    void Start()
    {
        index = 0;
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _damage = Damage;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(_waypoints[index].transform.position, transform.position);
        if (distanceToTarget <= _stoppingRange)
        {
            index = (index == 0) ? 1:0;
        }
        else if (distanceToTarget > _stoppingRange)
        {
            _agent.destination = _waypoints[index].transform.position;
        }

    }

    public void OnTriggerEnter(Collider other){
        if (other.tag == "Player") EventQueueManager.instance.AddEvent(new CmdApplyDamage(_damagable.GetComponent<IDamagable>(),_damage));
    }
}
