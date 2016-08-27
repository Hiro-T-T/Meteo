using UnityEngine;
using System.Collections;

public class UFO_move : MonoBehaviour {

    private Vector3 pos;
    private Vector3 start_pos;
    private GameObject player;
    public float fuwa = 0.0f;
    public int fuwa_time = 240;
    public float fuwa_add = 1.0f;
    public float fuwa_range = 2.0f;
    public float fuwa_speed = 0.1f;
    // Use this for initialization
    void Start()
    {
        pos = gameObject.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        pos.z = 50;
        pos.y = player.transform.position.y;
        start_pos = pos;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = pos;
        pos.z -= fuwa_speed;
        fuwa += fuwa_add;
        pos.x = start_pos.x + (Mathf.Sin(Mathf.PI * 2 / fuwa_time * fuwa)) * fuwa_range;
    }
}
