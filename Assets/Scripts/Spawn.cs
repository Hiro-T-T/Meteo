using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    public GameObject[] enemy;    //敵オブジェクト
    private Vector3 pos;
    private int pattern;     //一度に何体のオブジェクトをスポーンさせるか
    public float interval = 1;  //何秒おきに敵を発生させるか
    public float timer;        //経過時間
    public int spawn_count = 0;
    int spawn_time = 0;
    int score = 0;
    private ScoreManager score_manager;
    private StageManager stage_manager;
    private int middle_time = 52;
    private int last_time = 102;
    private float dec_interval;
    // Use this for initialization
    void Start()
    {
       // Spawn_p();    //初期スポーン
        pos = gameObject.transform.position;
        score_manager = GameObject.Find("GameController").GetComponent<ScoreManager>();
        stage_manager = GameObject.Find("GameController").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(spawn_count);



        if (spawn_count % 10 == 0 && spawn_count != 0 && enemies.Length == 0)
        {
            stage_manager.backgroundFlag = true;
        }
        if (stage_manager.backgroundFlag == false)
        {
           
            // pattern = 2;
            timer += Time.deltaTime;    //経過時間加算
            if (timer >= interval)
            {
                Spawn_p(pattern);    //スポーン実行
            }
            else pattern = Random.Range(1, 10);

        }

    }
    void Spawn_p(int spawn_pattern)
    {
        switch (spawn_pattern)
        {
            

            case 1:
                  GameObject.Instantiate(enemy[0], pos, Quaternion.identity);
                  spawn_init();
                  break;

            case 2:
                GameObject.Instantiate(enemy[1], new Vector3(pos.x + 1.8f, pos.y, pos.z), Quaternion.identity);
                GameObject.Instantiate(enemy[1], new Vector3(pos.x - 1.8f, pos.y, pos.z), Quaternion.identity);
                spawn_init();
                break;

            case 3:
                GameObject.Instantiate(enemy[1], new Vector3(pos.x + 1.8f, pos.y, pos.z), Quaternion.identity);
                GameObject.Instantiate(enemy[1], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                spawn_init();
                break;

            case 4:
                GameObject.Instantiate(enemy[1], new Vector3(pos.x - 1.8f, pos.y, pos.z), Quaternion.identity);
                GameObject.Instantiate(enemy[1], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                spawn_init();
                break;

            case 5:
                spawn_time++;
                if(spawn_time == 2) GameObject.Instantiate(enemy[1], new Vector3(pos.x - 1.8f, pos.y, pos.z), Quaternion.identity);
                if(spawn_time == middle_time) GameObject.Instantiate(enemy[1], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                if(spawn_time == last_time) GameObject.Instantiate(enemy[1], new Vector3(pos.x + 1.8f, pos.y, pos.z), Quaternion.identity);
                if(spawn_time > last_time + 3) spawn_init();
                break;

            case 6:
                spawn_time++;
                if (spawn_time == 2) GameObject.Instantiate(enemy[1], new Vector3(pos.x + 1.8f, pos.y, pos.z), Quaternion.identity);
                if (spawn_time == middle_time) GameObject.Instantiate(enemy[0], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                if (spawn_time == last_time) GameObject.Instantiate(enemy[1], new Vector3(pos.x - 1.8f, pos.y, pos.z), Quaternion.identity);
                if (spawn_time > last_time + 3) spawn_init();
                break;
            case 7:
                spawn_time++;
                if (spawn_time == 2) GameObject.Instantiate(enemy[0], new Vector3(pos.x + 3.0f, pos.y, pos.z), Quaternion.identity);
                if (spawn_time == middle_time) GameObject.Instantiate(enemy[1], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                if (spawn_time == last_time) GameObject.Instantiate(enemy[0], new Vector3(pos.x - 3.0f, pos.y, pos.z), Quaternion.identity);
                if (spawn_time > last_time + 3) spawn_init();
                break;
            case 8:
                spawn_time++;
                if (spawn_time == 2) GameObject.Instantiate(enemy[0], new Vector3(pos.x + 3.5f, pos.y, pos.z), Quaternion.identity);
                if (spawn_time == middle_time + 10) GameObject.Instantiate(enemy[0], new Vector3(pos.x - 3.5f, pos.y, pos.z), Quaternion.identity);
                if (spawn_time == last_time + 10) GameObject.Instantiate(enemy[0], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                if (spawn_time > last_time + 13) spawn_init();
                break;
            case 9:
                
                GameObject.Instantiate(enemy[0], new Vector3(pos.x + 2.0f, pos.y, pos.z), Quaternion.identity);
                GameObject.Instantiate(enemy[0], new Vector3(pos.x - 2.0f, pos.y, pos.z), Quaternion.identity);
                spawn_init();
                break;


        }
        
       
        

    }
    void spawn_init()
    {
        spawn_count++;

        if (spawn_count % 10 == 0)
        {
            interval = 5.0f;
            dec_interval = spawn_count / 20;
            middle_time = middle_time - spawn_count / 2;
            if (middle_time < 12) middle_time = 12;
            last_time = last_time - spawn_count / 2;
            if (last_time < 22) last_time = 22;

        }
        else
        {
            interval = 2.0f - dec_interval;
            if (interval < 0.5) interval = 0.5f;
        }

        

        spawn_time = 0;
        timer = 0;  //初期化
    }

}
