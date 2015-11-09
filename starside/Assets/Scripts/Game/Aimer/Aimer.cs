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

	// Use this for initialization
	void Start () {
        aimerH.aimerC = aimerC; //initialize center crosshair
        aimerV.aimerC = aimerC; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Aim() {
		StartCoroutine ("AimH");
	}

	IEnumerator AimH() {
        aimerH.speed = aimerSpeed;
        aimerV.speed = aimerSpeed;

        //Set aimerH active and to aiming mode
        aimerH.gameObject.SetActive(true);
        aimerH.aiming = true; 
        while(aimerH.aiming)
        {
            if(GameManager.getInput())
            {
                aimerH.aiming = false; 
            }
            yield return null; 
        }

		yield return new WaitForSeconds (1.0f / 60.0f);
        StartCoroutine("AimV");
	}

	IEnumerator AimV(){
        aimerV.gameObject.SetActive(true);
        aimerC.GetComponent<SpriteRenderer>().enabled = true;
        aimerC.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        aimerV.aiming = true; 
        while(aimerV.aiming || paused)
        {
            if(GameManager.getInput() && !paused && !inputDisabled)
            {
                aimerV.aiming = false; 
            }
            yield return null;
        }
        yield return new WaitForSeconds(1.0f / 60.0f);
        aimed = true; 
	}

    /// <summary>
    /// Returns whether or not the enemy has been hit
    /// </summary>
    /// <returns></returns>
    public bool CheckHit()
    {
        AimerCenter gobj = gameObject.transform.Find("AimerC").GetComponent<AimerCenter>();
        return gobj.OnHit();
    }

    /// <summary>
    /// Return the enemy that was hit
    /// </summary>
    /// <returns></returns>
    public Enemy GetHitEnemy()
    {
        AimerCenter gobj = gameObject.transform.Find("AimerC").GetComponent<AimerCenter>();
        return gobj.enemyHit();
    }
}
