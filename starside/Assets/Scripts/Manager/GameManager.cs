using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Game Manager instance itself
    private static GameManager instance;

    //Game attributes
    private bool aiming; 

    //Game elements
    private Board board; 
    private Aimer aimer;
	private int aimerIndex; 

    // Pre-initialization method
    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Initialize the board
        board = GetComponentInChildren<Board>();

        //Grab the aimer game objects
        aimer = transform.FindChild("Aimer").GetComponent<Aimer>();
	}

    void Start()
    {
        board.InitBoard();
        StartCoroutine("Init");
    }

    //Initializing the board, (and the enemies) 
    IEnumerator Init()
    {
		//Populate the board
		board.PopulateBoard ();

        while (!board.isPopulated())
        {
            yield return null; 
        } 
        while (!getInput())
        {
            yield return null; 
        }
        yield return new WaitForSeconds(1.0f / 60.0f);

        //The next time Init is called, the board should be empty
        aiming = true; 
        StartCoroutine("Aim");
    }

    //Allowing user to aim the crosshairs
    IEnumerator Aim()
    {
        float speed = (3.0f * Mathf.Log10(1 + 1.0f) + 3.5f); //Always gives the same constant

        aimer.Aim();

        int targetX = -1;
        int targetY = -1; 

        while (aiming)
        {
            yield return null;
        }
        StartCoroutine(ProcessAim(targetX, targetY));
    }
    
    //Process the aimed down location
    IEnumerator ProcessAim(int x, int y)
    {
        Debug.Log("X: " + x + " ," + " Y: " + y);
        yield return null; 
    }

    //Getting user touch/click input
    public static bool getInput()
    {
        float yBorder = (Camera.main.orthographicSize * 2) * (3.0f / 4.0f) - 3.5f;
        return (Input.GetMouseButtonDown(0) &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y < yBorder); 
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
