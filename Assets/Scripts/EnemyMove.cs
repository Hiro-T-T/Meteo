using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public float enemy_speed = 0.1f;
    public float rx = 5,ry = 15,rz = 11;
    private Spawn spawn;
    private StageManager stage_manager;
    public int up_count = 10;
    public bool initFlag = false;
    private float random_dir;
    // Use this for initialization
    void Start () {
        stage_manager = GameObject.Find("GameController").GetComponent<StageManager>();
        spawn = GameObject.Find("spawn_object").GetComponent<Spawn>();
        if (spawn.spawn_count % up_count == 0 && spawn.spawn_count != 0)
        {
            stage_manager.add_speed *= 1.15f;
            stage_manager.fuwa_add += 0.3f;
            Debug.Log("speedUp");
        }

        random_dir = Random.Range(0.0f, 90.0f);
        transform.Rotate(new Vector3(1, 0, 0), random_dir);

    }
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, 0, -(enemy_speed + stage_manager.add_speed));
        transform.Rotate(new Vector3(1, 0, 0), -rx);
      //  transform.Rotate(new Vector3(0, 0, 0), -ry);
        transform.Rotate(new Vector3(0, 0, 0.5f), -rz);
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Death")
        {
              //  spawn.timer = 0;  //初期化

            Destroy(gameObject);
        }
    }
}
