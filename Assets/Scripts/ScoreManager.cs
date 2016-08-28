using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour {

    public MoveResult move_result;
    private static int score = 0;
    public Text score_text;
    public int add_score = 100;

    public static int getScore()
    {
        return score;
    }

	// Use this for initialization
	void Start () {
        score_text.text = "score 0";
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (move_result.resultFlag == false)
        {
            score += 1;
        }
        score_text.text = "score " + score.ToString();
      //  Debug.Log(score);
	}

    public void AddScore()
    {
        if (move_result.resultFlag == false)
        {
            score += add_score;
        }
    }
}
