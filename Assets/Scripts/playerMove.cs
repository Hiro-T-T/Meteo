using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {
   // Touch touch = Input.GetTouch(0);
    private Vector3 pos;
    private Vector3 screenPoint;
    private Vector3 offset;
    public Vector3 currentPosition;
    public float move_limited = 1.5f;
    //public GameObject gameobject;
    public MoveResult result;
    public GameObject effect;
    public GameObject se_obj;
    private Vector3 ef_pos;
    private StageManager stage_manager;
    //public AudioClip audioClip;
    //AudioSource audioSource;

    // Use this for initialization
    void Start () {
        pos = gameObject.transform.position;
        ef_pos = effect.transform.position;
        stage_manager = GameObject.Find("GameController").GetComponent<StageManager>();
        //audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = audioClip;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //クリック時（タッチ時）
        if(Input.GetMouseButtonDown(0) == true)
        {
            //screenPoint変数に現在のプレイヤーのワールド座標を保存
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            //変数posのx座標にマウスをクリックした座標を習得
            pos.x = Input.mousePosition.x;
            //offset変数にクリック座標とプレイヤー座標のずれを代入、zは関係ないのでそのまま
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, screenPoint.z));

        }
        //ドラッグ時（スライド時）
        if(Input.GetMouseButton(0) == true)
        {

            pos.x = Input.mousePosition.x;
            //なんとなくこちらで指定
            Vector3 currentScreenPoint = new Vector3(pos.x, pos.y, screenPoint.z);
            //offsetとのずれを加味して座標指定
            currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
            //動け動け(´◉◞౪◟◉)
            transform.position = currentPosition;
            effect.transform.position = currentPosition;
            if(transform.position.x > move_limited)
            {
                transform.position = new Vector3(move_limited, pos.y, currentPosition.z);
                effect.transform.position = new Vector3(move_limited, pos.y, currentPosition.z);
            }
            if(transform.position.x < -move_limited)
            {
                transform.position = new Vector3(-move_limited, pos.y, currentPosition.z);
                effect.transform.position = new Vector3(-move_limited, pos.y, currentPosition.z);
            }

        }

  }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //ゲームオーバー処理
            if(stage_manager.itemFlag == false)
            {
                se_obj.SendMessage("Sound");
                GameOver();
            }
            else if(stage_manager.itemFlag == true)
            {
                col.gameObject.SendMessage("ItemHit");
            }
           
        }
    }

    void GameOver()
    {
        Destroy(gameObject);
        result.resultFlag = true;
    }
}
