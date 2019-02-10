using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public void Attack(float angle, float distance) {
        transform.GetChild(0).transform.localScale = new Vector3(9,9,1);
        transform.GetChild(0).transform.localPosition = new Vector3(0,-distance,0);
        transform.rotation = Quaternion.Euler(0,0,angle);
        GetComponentInChildren<Animator>().SetTrigger("attack");
    }
    public void Attack1(float angle) {
        transform.GetChild(0).transform.localScale = new Vector3(11,11,1);
        transform.GetChild(0).transform.localPosition = new Vector3(0,-2,0);
        GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        transform.rotation = Quaternion.Euler(0,0,angle);
        GetComponentInChildren<Animator>().SetTrigger("attack");
    }
}
