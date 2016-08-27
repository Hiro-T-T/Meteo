using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    public GameObject enemy1;    //敵オブジェクト
    public GameObject enemy2;    //敵オブジェクト
    private Vector3 pos;
    private int pattern;     //一度に何体のオブジェクトをスポーンさせるか
    private float interval = 5;  //何秒おきに敵を発生させるか
    private float timer;        //経過時間
                                // Use this for initialization
    void Start()
    {
        Spawn_p();    //初期スポーン
        pos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pattern = Random.Range(1, 3);
        timer += Time.deltaTime;    //経過時間加算
        if (timer >= interval)
        {
            Spawn_p();    //スポーン実行
            interval = Random.Range(3, 6);
            timer = 0;  //初期化
        }
    }
    void Spawn_p()
    {
        switch (pattern)
        {
            case 1:
                GameObject.Instantiate(enemy1, pos, Quaternion.identity);
                break;
            case 2:
                GameObject obj = GameObject.Instantiate(enemy2, new Vector3(pos.x + 1.5f,pos.y,pos.z), Quaternion.identity)as GameObject;
                UFO_move ufo_move = obj.GetComponent<UFO_move>();
                ufo_move.fuwa_range = -ufo_move.fuwa_range;
                GameObject obj2 = GameObject.Instantiate(enemy2, new Vector3(pos.x - 1.5f, pos.y, pos.z), Quaternion.identity) as GameObject;
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
}
