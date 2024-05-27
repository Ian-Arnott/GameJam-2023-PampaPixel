using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bunny : MonoBehaviour
{

    [SerializeField] private float _attackCooldown;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damage;
    private bool _isTwist;

    [SerializeField] private GameObject _area; // cube where it is
    private bool _isHappy;
    private bool _isActive;

    [SerializeField] private GameObject _target;
    [SerializeField] private float _attackRange = 2.0f; // La distancia a la que el enemigo debe detenerse y atacar

    void Start()
    {
        _isTwist = false;
        _isActive = false;
        EventManager.instance.OnTwist += Twist;
        _attackCooldown = 0;
    }

    void Twist(bool twist)
    {
        _isTwist = twist;
    }

    void Update()
    {
        bool playerInArea = _area.GetComponent<Collider>().bounds.Contains(_target.transform.position);

        if (playerInArea) {
            _animator.SetBool("isActive",true);
            if (_isTwist) 
            {
                _animator.SetBool("isHappy", false);
            }
            else
            {
                _animator.SetBool("isHappy", true);              
            }
        } else {
            _animator.SetBool("isActive", false);
        }
    }
}

