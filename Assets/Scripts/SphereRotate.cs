using UnityEngine;
using System.Collections;

public class SphereRotate : MonoBehaviour {

    public float rx = 5, ry = 15, rz = 11;
    private float timeleft;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0.0)
        {
            timeleft = 2.0f;

            rz = Random.Range(-20, 20);//ここに処理
        }
        transform.Rotate(new Vector3(1, 0, 0), rx);
        transform.Rotate(new Vector3(0, 1, 0), -ry);
        transform.Rotate(new Vector3(0, 0, 1), -rz);
    }
}
