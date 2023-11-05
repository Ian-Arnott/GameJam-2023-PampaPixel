using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> _waypoints;
    private float _speed = 3f;
    private bool _movingToEnd = true;
    void Update()
    {
        Vector3 target = _movingToEnd ? _waypoints[1].transform.position : _waypoints[1].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        if (transform.position == target) _movingToEnd = !_movingToEnd; 
    }
}
