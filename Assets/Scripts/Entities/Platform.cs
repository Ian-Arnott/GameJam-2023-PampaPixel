using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int _startPos; 
    [SerializeField] private int _endPos; 
 
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnTwist += ActionTwist;

    }

    void ActionTwist(bool twist)
    {
        if(twist) transform.position = new Vector3(_endPos,transform.position.y,transform.position.z);
        else transform.position = new Vector3(_startPos,transform.position.y,transform.position.z);
     
    }
}
