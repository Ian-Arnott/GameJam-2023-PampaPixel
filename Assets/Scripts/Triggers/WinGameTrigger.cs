using System.Collections;
using UnityEngine;

    public class WinGameTrigger : MonoBehaviour
    {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GlobalManager.instance.hasObjective) EventManager.instance.EventGameWin();
            //Destroy(this.gameObject);
        }

    }
}