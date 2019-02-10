using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terraformer : MonoBehaviour
{

    private Color32 colorA;
    // Start is called before the first frame update
    void Start()
    {
        colorA = GetComponent<SpriteRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Appear() {
        Color32.Lerp(new Color32(colorA.r,colorA.g,colorA.b,0), colorA, Time.deltaTime);
    }
}
