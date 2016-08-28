using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour {

    public float move_interval = 3.0f; // シーン移動可能になるまでの時間
    private bool move_allow = false; // シーン移動の許可のための変数
    public Text sc_text; //Text用変数
    public Text highSc_text;
    private int resultsc; // resultスコア用変数
    private int highscore;
    private string key = "HIGH SCORE";

    // Use this for initialization
    void Start () {
        resultsc = ScoreManager.getScore();
        highscore = PlayerPrefs.GetInt(key, 0);
        sc_text.text = "Score   ";
        highSc_text.text = highSc_text.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if(resultsc > highscore)
        {
            highscore = resultsc;
            PlayerPrefs.SetInt(key, highscore);
            PlayerPrefs.Save();
        }
        highSc_text.text = "HighScore " + highscore.ToString();
        sc_text.text = "Score   " + resultsc.ToString();
    }
}
