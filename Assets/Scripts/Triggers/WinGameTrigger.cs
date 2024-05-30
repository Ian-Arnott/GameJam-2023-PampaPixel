using System.Collections;
using UnityEngine;

    public class WinGameTrigger : MonoBehaviour
    {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.instance.EventGameOver(true);
            //Destroy(this.gameObject);
        }

    }
}