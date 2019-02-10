using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GamepadInput;

public class nestiham : MonoBehaviour
{
    public int NUM = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (NUM > 2) {
             GetComponent<Text>().enabled = true;
             if (GamePad.GetButtonDown(GamePad.Button.RightStick, GamePad.Index.Any)) {
                SceneManager.LoadScene("main_scene", LoadSceneMode.Single); // nemam cas whatever ..
             }
        }
    }
}
