using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bunny : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private bool _isTwist;
    [SerializeField] private GameObject _area; // cube where it is
    [SerializeField] private GameObject _target;

    void Start()
    {
        _isTwist = GlobalManager.instance.hasObjective;
        EventManager.instance.OnTwist += Twist;
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

