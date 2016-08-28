using UnityEngine;
using System.Collections;

public class earthRotate : MonoBehaviour {

    public float earth_rotate = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 1, 0), earth_rotate);
    }
}
