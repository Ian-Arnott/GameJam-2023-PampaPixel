using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    void Start()
    {
        EventManager.instance.OnObjectivePickup += OpenDoor;

    }

    void OpenDoor()
    {
        int i = 1;
        foreach (GameObject door in doors)
        {
            door.transform.Rotate(new Vector3(0,0,Mathf.Pow(-1,i++)*90));
        }
        GetComponent<BoxCollider>().isTrigger = true;
    }

}
