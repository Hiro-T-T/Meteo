using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    public GameObject enemy1;    //敵オブジェクト
    public GameObject enemy2;    //敵オブジェクト
    private Vector3 pos;
    private int pattern;     //一度に何体のオブジェクトをスポーンさせるか
    public float interval = 1;  //何秒おきに敵を発生させるか
    public float timer;        //経過時間
    public float spawn_count = 0.0f;
    int spawn_time = 0;
    int score = 0;
    private ScoreManager score_manager;
    private stageManager stage_manager;
    // Use this for initialization
    void Start()
    {
        Spawn_p();    //初期スポーン
        pos = gameObject.transform.position;
        score_manager = GameObject.Find("GameController").GetComponent<ScoreManager>();
        stage_manager = GameObject.Find("GameController").GetComponent<stageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (spawn_count % 10 == 0 && spawn_count != 0 && enemies.Length == 0)
        {
            stage_manager.backgroundFlag = true;
        }
        if (stage_manager.backgroundFlag == false)
        {
            pattern = Random.Range(1, 3);
            // pattern = 2;
            timer += Time.deltaTime;    //経過時間加算
            if (timer >= interval)
            {
                Spawn_p();    //スポーン実行
            }
          
        }


    }
    void Spawn_p()
    {
        switch (pattern)
        {
            

            case 1:
                     GameObject.Instantiate(enemy1, pos, Quaternion.identity);
                  
                    spawn_init();
                
                break;
            case 2:
                spawn_time++;

                if (spawn_time < 2)
                {
                    GameObject.Instantiate(enemy2, new Vector3(pos.x + 1.8f, pos.y, pos.z), Quaternion.identity);
                    
                }
                
                if(spawn_time > 15)
                {
                    GameObject.Instantiate(enemy2, new Vector3(pos.x - 1.8f, pos.y, pos.z), Quaternion.identity);
             
                    spawn_init();
                }
                
                break;

        }
        /*
    
            //スポーン座標指定
            float x = Random.Range(-50f, 50f);
            float y = Random.Range(0f, 100f);
            float z = Random.Range(-50f, 50f);
            */
            //Enemy出現
         //   GameObject.Instantiate(enemy, pos, Quaternion.identity);
            Debug.Log("Spawn");//Debug
        

    }
    void spawn_init()
    {
        spawn_count++;
        interval = Random.Range(2.5f, 5.0f);
        spawn_time = 0;
        timer = 0;  //初期化
    }

}
