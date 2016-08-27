using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public float enemy_speed = 0.1f;
    public float rx = 5,ry = 15,rz = 11;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, 0, -enemy_speed);
        transform.Rotate(new Vector3(1, 0, 0), -rx);
        transform.Rotate(new Vector3(0, 1, 0), -ry);
        transform.Rotate(new Vector3(0, 0, 1), -rx);
    }
}
