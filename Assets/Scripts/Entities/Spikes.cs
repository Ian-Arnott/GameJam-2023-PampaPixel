using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Spikes : MonoBehaviour
{

    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _target;
    private bool _playerInArea;
    private float _attackDuration;

    void Start()
    {
        _playerInArea = false;
        _attackDuration = 0;
    }

    void Update()
    {
        if (_attackDuration > 0) _attackDuration -= Time.deltaTime;
        if (_attackDuration < 0) _attackDuration = 0;
        if (_playerInArea) {
            if (_attackDuration == 0) {
                _attackDuration = _attackCooldown;
                EventQueueManager.instance.AddEventToQueue(new CmdApplyDamage(_target.GetComponent<IDamagable>(), _damage));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerInArea=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerInArea=false;
        }
    }

    
}

