using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyBallSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private GameObject _spikyBall;
    [SerializeField] private GameObject _area;
    [SerializeField] private GameObject _target;
    private float _spawnCurrent;

    void Start()
    {
        _spawnCurrent = 0;
    }

    void Update()
    {
        bool playerInArea = false;
        if(_target!=null) 
            playerInArea = _area.GetComponent<Collider>().bounds.Contains(_target.transform.position);
        if (playerInArea)
        {
            if (_spawnCurrent > 0) _spawnCurrent -= Time.deltaTime;
            if (_spawnCurrent < 0) _spawnCurrent = 0;
            if (_spawnCurrent == 0)
            {
                _spawnCurrent = _spawnRate;
                Instantiate(_spikyBall,transform.position,transform.rotation);
            }
        }
    }
}
