using UnityEngine;
using System.Collections;

public class Aimer : MonoBehaviour {

	// Aimer Prefabs: Horizontal, Vertical, Center
	public AimerHorizontal aimerH;
	public AimerVertical aimerV;
	public AimerCenter aimerC;

	// time in seconds to wait before the aimer moves
	private float aimerSpeed = 8.0f;

	private bool aimed = false;

	private int targetX;
	private int targetY;
	
	private bool paused;
	
	public bool inputDisabled = false;

	//Getter and setter methods
	public void setSpeed(float newSpeed){ aimerSpeed = newSpeed; }
	public float getSpeed(){ return aimerSpeed; }
	
	public void setAimed(bool newAim){ aimed = newAim; } 
	public bool getAimed() { return aimed; }
	
	public void setTargetX(int newX){ targetX = newX; }
	public int getTargetX(){ return targetX; }
	
	public void setTargetY(int newY){ targetY = newY; }
	public int getTargetY(){ return targetY; } 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Aim() {
		StartCoroutine ("AimH");
	}

	IEnumerator AimH() {
		print ("started aiming horiz"); 
		yield return new WaitForSeconds (1.0f / 60.0f);
	}

	IEnumerator AimV(){
		print ("started aiming vert");
		yield return null;
	}
}
