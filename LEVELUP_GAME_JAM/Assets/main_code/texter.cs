using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texter : MonoBehaviour
{
    void Start() {
       // StartCoroutine(Countdown(5));
    }
    public void Say(string text, Color color)
    {
        GetComponent<Text>().text = text;
        GetComponent<Text>().color = color;
        StartCoroutine(Fader(0,color));
    }

    IEnumerator Fader(float v, Color32 color)
    {
        while (v < 1)
        {
            GetComponentInChildren<Text>().color = Color32.Lerp(color, new Color32(color.r, color.g, color.b, 0), v);
            v += Time.deltaTime;
        }
        yield return null;
    }
    //RIIIP
    IEnumerator Countdown(int seconds) {
        while (0 < seconds+1) {
            seconds--;
            GameObject.Find("Text").GetComponent<texter>().Say(seconds+1.ToString(), new Color32(255,255,255,255));
        } 
        while (seconds <= 0) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("player");
            foreach(GameObject player in players) {
                player.GetComponent<player_controller>().started = true;
            }
        }
        yield return new WaitForSeconds(1);
    }
}
