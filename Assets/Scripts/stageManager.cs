using UnityEngine;
using System.Collections;

public class stageManager : MonoBehaviour {
    public float game_rotate = 0.0f;
    private int rotate_time = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        game_rotate = (90 - game_rotate) / 30;
	}
}
