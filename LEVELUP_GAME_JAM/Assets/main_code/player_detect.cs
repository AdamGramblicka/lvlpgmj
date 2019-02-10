using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_detect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("ground")) {
            if (name == "dA") {
                GetComponentInParent<player_controller>().enabler = 1;
                //GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
                Debug.Log("a");
            } else if(name == "dB") {
                GetComponentInParent<player_controller>().enabler = 2;
                //GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
                Debug.Log("b");
            } 
        }
        if (col.CompareTag("player") && name == "au") {
            GetComponentInParent<player_controller>().grounded = true;
        }
    }
     void OnTriggerExit2D(Collider2D col) {
        if (name != "au") {
            GetComponentInParent<player_controller>().enabler = 0;
        }
        if (col.CompareTag("player") && name == "au") {
            GetComponentInParent<player_controller>().grounded = false;
        }
    }
}
