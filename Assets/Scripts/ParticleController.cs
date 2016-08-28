using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.rotation = new Quaternion(-90, 180, 0, 1);
    }
}
