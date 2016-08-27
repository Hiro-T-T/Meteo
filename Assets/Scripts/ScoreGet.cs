﻿using UnityEngine;
using System.Collections;

public class ScoreGet : MonoBehaviour {
    private ScoreManager score_manager;
    public int add_score = 100;
    private Spawn spawn;
	// Use this for initialization
	void Start () {
        score_manager = GameObject.Find("GameController").GetComponent<ScoreManager>();
        spawn = GameObject.Find("spawn_object").GetComponent<Spawn>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            score_manager.score += add_score;
            Destroy(gameObject);
        }
    }
}
