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

// Constants
    public int NUM_OF_PLAYERS;

    void Start() {
        NUM_OF_PLAYERS = players.Count;
        health = Enumerable.Repeat(400, players.Count).ToList();
    }

    void Update() {
        for(int i = 0; i <NUM_OF_PLAYERS; i++) {
            string name = "hb" + (i+1);
            GameObject.FindGameObjectWithTag(name).GetComponent<Slider>().value = health[i];
        }
    }
}