using UnityEngine;
using System.Collections;

public class ScoreGet : MonoBehaviour {
    private ScoreManager score_manager;
    private Spawn spawn;
	// Use this for initialization
	void Start () {
        score_manager = GameObject.Find("GameController").GetComponent<ScoreManager>();
        spawn = GameObject.Find("spawn_object").GetComponent<Spawn>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            score_manager.AddScore();
        }
    }
}
