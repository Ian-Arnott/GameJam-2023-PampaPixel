using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveable, IJumpable
{
    #region IMOVEABLE_PROPERTIES
    public float Speed => GetComponent<Actor>().Stats.MovementSpeed;
    public float Force => GetComponent<Actor>().Stats.JumpForce;
    #endregion

    #region IMOVEABLE_METHODS
    public void Move(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * Speed;
    }
    #endregion

    #region IJUMPABLE_METHODS
    public void Jump(Rigidbody rb)
    {
        Debug.Log("Hola");
        rb.AddForce(Vector3.up * Force);
    }
    #endregion
}
