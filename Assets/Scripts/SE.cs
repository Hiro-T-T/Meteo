using UnityEngine;
using System.Collections;

public class SE : MonoBehaviour {

    public AudioClip audioClip;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Sound()
    {
        audioSource.Play();
    }
}
