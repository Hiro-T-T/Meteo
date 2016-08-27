using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {
   // Touch touch = Input.GetTouch(0);
    private Vector3 pos;
    private Vector3 screenPoint;
    private Vector3 offset;
    // Use this for initialization
    void Start () {
        pos = gameObject.transform.position;

	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = pos;
        if(Input.GetMouseButtonDown(0) == true)
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            pos.x = Input.mousePosition.x;
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, screenPoint.z));

        }
        if(Input.GetMouseButton(0) == true)
        {

            pos.x = Input.mousePosition.x;
            Vector3 currentScreenPoint = new Vector3(pos.x, pos.y, screenPoint.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;

            transform.position = currentPosition;
        }
        
            
                //pos.x = touch.deltaPosition.x;
                Debug.Log("タッチー");
           
        
	}
}
