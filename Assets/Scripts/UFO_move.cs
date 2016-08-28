using UnityEngine;
using System.Collections;

public class UFO_move : MonoBehaviour {

    private Vector3 pos;
    private Vector3 start_pos;
    private GameObject player;
    public float fuwa = 0.0f;
    public int fuwa_time = 240;
   
    public float fuwa_range = 2.0f;
    public float fuwa_speed = 0.1f;
    private int fuwa_random = Random.Range(1, 3);
    private Spawn spawn;
    private StageManager stage_manager;
    public int up_count = 10;
    public bool initFlag = false;
    private int direction = 1;
    // Use this for initialization
    void Start()
    {
        stage_manager = GameObject.Find("GameController").GetComponent<StageManager>();
        pos = gameObject.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.Find("spawn_object").GetComponent<Spawn>();
        start_pos = pos;
        if(spawn.spawn_count % up_count == 0 && spawn.spawn_count != 0)
        {
            stage_manager.add_speed *= 1.1f;
            stage_manager.fuwa_add += 0.1f;
        }
       
        stage_manager.fuwa_add_speed = (spawn.spawn_count) / 15;
        if (fuwa_random == 1)
        {
            direction *= -1;
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = pos;
        pos.y = player.transform.position.y;
        pos.z -= (fuwa_speed + stage_manager.add_speed + stage_manager.item_speed);
        fuwa += stage_manager.fuwa_add;
        pos.x = start_pos.x + (Mathf.Sin(Mathf.PI * 2 / fuwa_time * fuwa)) * ((fuwa_range + stage_manager.fuwa_add_speed) * direction);
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Death")
        {
    
              //  spawn.timer = 0;  //初期化
            
            Destroy(gameObject);
        }
    }
    void ItemHit()
    {
        fuwa_speed = -(5 + stage_manager.item_speed);
    }
}
