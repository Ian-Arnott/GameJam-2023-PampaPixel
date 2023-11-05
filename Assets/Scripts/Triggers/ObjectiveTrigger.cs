using System.Collections;
using UnityEngine;


    public class ObjectiveTrigger : MonoBehaviour
    {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.instance.EventObjectivePickup();
            GlobalVictory.instance.isVictory = true;
            Destroy(this.gameObject);
        }

    }
}
