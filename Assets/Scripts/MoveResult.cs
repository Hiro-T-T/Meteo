using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveResult : MonoBehaviour {

    public bool resultFlag = false;
    public float moveTime_R = 1.0f;


	// Use this for initialization
	void Start () {
        resultFlag = false;
	}

    // Update is called once per frame
    void end()
    {
        SceneManager.LoadScene("result");
    }

    // Update is called once per frame
    void Update()
    {
        if (resultFlag)
        {
            Invoke("end", 2.0f);
        }
    }
}
