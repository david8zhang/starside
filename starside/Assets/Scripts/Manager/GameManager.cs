using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Game Manager instance itself
    private static GameManager instance;

    //Game attributes
    private bool aiming; 

    //Game elements
    private Board board; 
    private Aimer[] aimers = new Aimer[2];

    // Pre-initialization method
    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Initialize the board
        board = GetComponentInChildren<Board>();

        //Grab the aimer game objects
        aimers[0] = transform.FindChild("Aimer1").GetComponent<Aimer>();
        aimers[1] = transform.FindChild("Aimer2").GetComponent<Aimer>();
	}

    void Start()
    {
        board.InitBoard();
        StartCoroutine("Init");
    }

    //Initializing the board, (and the enemies) 
    IEnumerator Init()
    {
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
        foreach (Aimer a in aimers)
            a.setSpeed(speed);

        aimers[aimerIndex].aim();

        int targetX = -1;
        int targetY = -1; 

        while (aiming)
        {
            if (aimers[aimerIndex].getAimed())
            {
                aiming = false;
                targetX = aimers[aimerIndex].getTargetX();
                targetY = aimers[aimerIndex].getTargetY();
            }
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
