using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveable, IJumpable
{
    private float multiplier = 1;

    #region IMOVEABLE_PROPERTIES
    public float Speed => GetComponent<Actor>().Stats.MovementSpeed;
    public float Force => GetComponent<Actor>().Stats.JumpForce;
    #endregion

    #region IMOVEABLE_METHODS
    public void Move(Animator animator, Vector3 direction)
    {
        animator.SetBool("isWalking",true);
        transform.position += direction * Time.deltaTime * Speed;
    }

    public void setForceMultiplier(float multiplier)
    {
        this.multiplier = multiplier;
    }
    #endregion

    #region IJUMPABLE_METHODS
    public void Jump(CharacterController cc)
    {
        //cc.Move(Vector3.up * multiplier * Force);
        transform.position += Vector3.up * Time.deltaTime * Force * multiplier;
    }
    #endregion
}
