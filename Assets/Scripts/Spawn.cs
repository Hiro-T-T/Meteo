using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
    public GameObject enemy;    //敵オブジェクト
    public Transform ground;    //地面オブジェクト
    private float count = 5;     //一度に何体のオブジェクトをスポーンさせるか
    private float interval = 5;  //何秒おきに敵を発生させるか
    private float timer;        //経過時間
                                // Use this for initialization
    void Start()
    {
        Spawn_p();    //初期スポーン
    }

    // Update is called once per frame
    void Update()
    {
        count = Random.Range(1, 5);
        timer += Time.deltaTime;    //経過時間加算
        if (timer >= interval)
        {
            Spawn_p();    //スポーン実行
            interval = Random.Range(4, 8);
            timer = 0;  //初期化
        }
    }
    void Spawn_p()
    {
        for (int i = 0; i < count; i++)
        {
            //スポーン座標指定
            float x = Random.Range(-50f, 50f);
            float y = Random.Range(0f, 100f);
            float z = Random.Range(-50f, 50f);
            //床から浮かしている
            Vector3 pos = new Vector3(x, y, z) + ground.position;
            //Enemy出現
            GameObject.Instantiate(enemy, pos, Quaternion.identity);
            Debug.Log("Spawn");//Debug
        }

    }
}
