using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_detect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (name == "dA") {
            GetComponentInParent<player_controller>().enabler = 1;
            Debug.Log("a");
        } else if(name == "dB") {
            GetComponentInParent<player_controller>().enabler = 2;
            Debug.Log("b");
        }
    }
     void OnTriggerExit2D(Collider2D col) {
        GetComponentInParent<player_controller>().enabler = 0;
    }
}
