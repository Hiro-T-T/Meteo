using UnityEngine;
using System.Collections;

public class ItemMove : MonoBehaviour {
    private float move_speed = 0.1f;
    private StageManager stage_manager;
    // Use this for initialization
    void Start () {
        stage_manager = GameObject.Find("GameController").GetComponent<StageManager>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0,0,-move_speed);
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Death")
        {
            //  spawn.timer = 0;  //初期化

            Destroy(gameObject);
        }
        if(col.gameObject.tag == "Player")
        {
            stage_manager.itemFlag = true;
            Destroy(gameObject);
        }
    }

}
