using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class stat_vault : MonoBehaviour
{
    [SerializeField]
    public List<Transform> players = new List<Transform>(); // all players
    
    public List<int> health = new List<int>();

    public int DEATHS = 0;

    public List<Color32> colors = new List<Color32>(); 

// Constants
    public int NUM_OF_PLAYERS;

    void Start() {
        NUM_OF_PLAYERS = players.Count;
        health = Enumerable.Repeat(200, players.Count).ToList();
        colors.Add(new Color32(120,119,255,255));
        colors.Add(new Color32(144,253,120,255));
        colors.Add(new Color32(219,139,139,255));
    }

    void Update() {
        for(int i = 0; i <NUM_OF_PLAYERS; i++) {
            string name = "hb" + (i+1);
            GameObject.FindGameObjectWithTag(name).GetComponent<Slider>().value = health[i];
        }
    }
}