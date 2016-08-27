using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {
    Touch touch = Input.GetTouch(0);
    Vector3 pos;
	// Use this for initialization
	void Start () {
        pos = gameObject.transform.position;

	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = pos;

	if(Input.touchCount > 0)
        {
            if(touch.phase == TouchPhase.Moved)
            {
                pos.x = touch.position.x;
            }
        }
	}
}
