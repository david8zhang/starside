using UnityEngine;
using System.Collections;

public class AimerBar : MonoBehaviour {

    /** Orientation integer code. */
    private int orientation;

    /** Board and size */
    private Board board;
    private int boardSize;

    /** Aimer Center */
    private AimerCenter aimerC;

    /** aiming variable */
    private bool aiming;

    /** paused boolean. */
    private bool paused; 

    /** speed of the aimer. */ 
    private float speed;

    /**prefabs for the left middle and right sprites */
    public GameObject prefabLeft;
    public GameObject prefabMid;
    public GameObject prefabRight;

    /** The target position */
    private float target;

    /** The float counter for Math.PingPong. */
    private float counter = 8.0f;

    /** Pre-initialization method. */ 
    void Awake()
    {
        //Create the sprites that make up the bar
        
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
