using UnityEngine;
using System.Collections;

public class Aimer : MonoBehaviour {

    /** Horizontal crosshair, */
    private AimerBar aimerH; //TODO: Get rid of redundant horizontal and vertical code

    /** Vertical crosshair. */
    private AimerBar aimerV;

    /** Center crosshair. */
    private AimerCenter aimerC;

    /** Time in seconds before aimer moves. */
    private float aimerSpeed = 8.0f;

    /** Checks whether the aimers are stopped or not */
    private bool aimed = false; 

    /**Positions of the respective aimers */ 
    private int targetX;
    private int targetY;

    /** Checks if the game is paued */
    private bool paused;

    private static readonly int HORIZONTAL = 0; 
    private static readonly int VERTICAL = 1; 

    /**Initaliziation method. */ 
    void Start () {
        aimerH.setAimerC(aimerC).setOrientation(HORIZONTAL);
        aimerV.setAimerC(aimerC).setOrientation(VERTICAL); 
	}

    /** Coroutine for aiming */ 
    public void Aim() {
        StartCoroutine("AimH");
    }

    /** Update the gamestate per frame */ 
	void Update () {
        if (paused)
        {
            aimerH.setPaused(true);
            aimerV.setPaused(true);
        }
        else
        {
            aimerH.setPaused(false);
            aimerV.setPaused(false);
        }
	}

    /** Aim the horizonal piece. */ 
    IEnumerator AimH()
    {
        //Initalize the speed
        aimerH.setSpeed(aimerSpeed);
        aimerV.setSpeed(aimerSpeed);

        //Set aimer active and to aiming mode
        aimerH.gameObject.SetActive(true);
        aimerH.aiming = true;
        
        //if the mouse button isn't pressed or if paused, do nothing
        while(aimerH.getAiming() || paused)
        {
            if(GameManager.getInput() && !paused)
            {
                aimerH.setAiming(false);
            }
            yield return null; 
        }
        yield return new WaitForSeconds(1.0f / 60.0f); 
         
    }
}


