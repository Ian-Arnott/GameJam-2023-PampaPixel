using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikyBall : MonoBehaviour
{
    [SerializeField] private int _damage;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventQueueManager.instance.AddEventToQueue(new CmdApplyDamage(other.GetComponentInParent<IDamagable>(), _damage));
            Destroy(this);
        }
    }
}
