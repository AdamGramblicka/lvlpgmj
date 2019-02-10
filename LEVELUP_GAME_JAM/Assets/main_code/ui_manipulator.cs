using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_manipulator : MonoBehaviour
{

    float t = 0;
    float speed = 0;
    float distance = 0;
    Vector3 position;
    int orientation = 1;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(20,40);
        distance = Random.Range(0,20);
        position = transform.position;
        orientation = Random.Range(0,1) < 0.5f ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (t / speed < 1) {
            transform.position = Vector3.Lerp(position, new Vector3(distance * orientation,position.y,position.z), t / speed);
            t += Time.deltaTime;
        } else
        {
            t = 0;
            speed = Random.Range(20,40);
            distance = Random.Range(0,20);
            position = transform.position;
            orientation = Random.Range(0,1) < 0.5f ? 1 : -1;
        }
    }
}
