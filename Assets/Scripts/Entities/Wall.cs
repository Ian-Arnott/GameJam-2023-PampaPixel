using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnTwist += ActionTwist;

    }

    void ActionTwist(bool twist)
    {
        if(twist) transform.position = new Vector3(transform.position.x,-10,transform.position.z);
        else transform.position = new Vector3(transform.position.x,0,transform.position.z);
     
    }
}
