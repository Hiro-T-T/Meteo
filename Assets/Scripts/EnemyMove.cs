﻿using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public float enemy_speed = 0.1f;
    public float rx = 5,ry = 15,rz = 11;
    private Spawn spawn;
    private stageManager stage_manager;
    public int up_count = 10;
    // Use this for initialization
    void Start () {
        stage_manager = GameObject.Find("GameController").GetComponent<stageManager>();
        spawn = GameObject.Find("spawn_object").GetComponent<Spawn>();
        if (spawn.spawn_count % up_count == 0)
        {
            stage_manager.add_speed *= 1.5f;
            stage_manager.fuwa_add += 0.3f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, 0, -(enemy_speed + stage_manager.add_speed));
        transform.Rotate(new Vector3(1, 0, 0), -rx);
        transform.Rotate(new Vector3(0, 1, 0), -ry);
        transform.Rotate(new Vector3(0, 0, 1), -rx);
    }
}
