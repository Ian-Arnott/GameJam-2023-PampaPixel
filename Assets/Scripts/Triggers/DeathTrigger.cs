using System.Collections;
using UnityEngine;


    public class DeathTrigger : MonoBehaviour
    {
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.instance.EventGameOver(false);
        }
        Destroy(other.gameObject);
    }
}
