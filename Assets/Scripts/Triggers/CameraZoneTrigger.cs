using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _zone;
    public void OnTriggerExit(Collider other){
        if (other.tag == "Player") _zone.SetActive(false);

    }
}
