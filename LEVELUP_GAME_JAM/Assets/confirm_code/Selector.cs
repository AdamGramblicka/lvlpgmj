using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class Selector : MonoBehaviour
{
    GamePad.Index a;
    bool confirmed = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = Color.red;
        switch(tag) {
            case "hb1" :
                a = GamePad.Index.One;
            break;
            case "hb2" :
                a = GamePad.Index.Two;
            break;
            case "hb3" :
                a = GamePad.Index.Three;
            break;
            case "hb4" :
                a = GamePad.Index.Four;
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePad.GetButtonDown(GamePad.Button.LeftStick, a)) {
            GetComponent<Image>().color = Color.green;
            if (!confirmed) {
                GameObject.Find("MAIN").GetComponent<nestiham>().NUM++;
                confirmed = true;
            }
        }
    }
}
