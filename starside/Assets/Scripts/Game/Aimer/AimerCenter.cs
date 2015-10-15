using UnityEngine;
using System.Collections;

public class AimerCenter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Set position methods
    public void setX(float x)
    {
        transform.position = new Vector3(x, transform.position.y);
    }

    public void setY(float y)
    {
        transform.position = new Vector3(transform.position.x, y);
    }
}
