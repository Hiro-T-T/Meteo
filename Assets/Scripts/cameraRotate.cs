using UnityEngine;
using System.Collections;

public class cameraRotate : MonoBehaviour {
    private stageManager stage_manager;
	// Use this for initialization
	void Start () {
        stage_manager = GameObject.Find("GameController").GetComponent<stageManager>();
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(0.0f, 0.0f, stage_manager.game_rotate);
	}
}
