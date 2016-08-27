using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {
    public GameObject player;   //プレイヤーを代入
    private playerMove player_move;
    private Vector3 player_pos; //プレイヤー座標
    private Vector3 camera_pos; //カメラ座標
	// Use this for initialization
	void Start () {
        
        camera_pos = gameObject.transform.position;
        player_move = player.GetComponent<playerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        // player.transform.position = player_pos;
        player_pos = player.transform.position;
        gameObject.transform.position = camera_pos;

        camera_pos.x = player_move.currentPosition.x;

	
	}
}
