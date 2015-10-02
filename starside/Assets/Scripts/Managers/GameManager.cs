using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {
    /** Game manager for initialization. */ 
    private static GameManager instance;

    /** Reference to the Board Object. */
    private Board board;

    /** GameUIManager */
    //TODO: Create GameUIManager

    /** Determine if the player is aiming. */
    private bool aiming;

    /** An List of aimers (size is flexible). */
    private List<Aimer> aimers = new List<Aimer>();

    /** Number of aimers the player currently has. */
    private int numAimers = 2; 
    
    /** the index of the current aimer. */
    private int aimerIndex = 0;

    /** Audio files for SFX. */
    //TODO: Create audio files

    /** Determine if the game is paused. */
    private bool paused;

    /** The current score for the game. */
    private int score; 

    /** Pre-initialization method */ 
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        board = GetComponentInChildren<Board>();
        for (int i = 0; i < numAimers; i++) {
            aimers.Add(transform.FindChild("Aimer1").GetComponent<Aimer>());
        }
    }

    /** Initialization method. */ 
    void Start () {
        board.initBoard(); //TODO: Create Board initialization method
        StartCoroutine("CreateElements");
	}

    /** IEnumerator for creating Elements coroutine. */ 
    private IEnumerator CreateElements() {
        //Populate the board with tiles. 
        board.PopulateBoard();

        //Wait for the board to be populated. 
        while (!board.isPopulated())
            yield return null;

        //wait for user to start the battle or unpause the game
        while (!getInput() || paused)
            yield return null;

        //Wait one frame for to prevent double-click registration
        yield return new WaitForSeconds(1.0f / 60.0f);

        //Initalize the aiming coroutine
        aiming = true;
        StartCoroutine("Aim");
    }

    /** IEnumerator for Player Aiming. */ 
    private IEnumerator Aim() {
        //TODO: Figure out speed incrementing algorithm
        float speed = 6.0f;
        foreach (Aimer a in aimers)
            a.setSpeed(speed);

        //Aim the current aimer
        aimers[aimerIndex].aim();

        //init target values with -1
        int targetX = -1;
        int targetY = -1;
        while (aiming)
        {
            if (aimers[aimerIndex].getAimed())
            {
                aiming = false;
                targetX = aimers[aimerIndex].getTargetX;
                targetY = aimers[aimerIndex].getTargetY; 
            }
            yield return null; 
        }
        //reset the aimer
        aimers[aimerIndex].setAimed(false);

        StartCoroutine(ProcessAim(targetX, targetY));
    }
    
    private IEnumerator ProcessAim(int targetX, int targetY)
    {
        if(board.hasWeakPoint(targetY, targetX) != null)
        {
            aimers[aimerIndex].hitTarget(true);
            BoardTile weakPoint = board.getWeakPoint(targetY, targetX);
            weakPoint.Hit();

            //If the board is waiting for the button to process, stop the coroutine
            //from continuing
            while (board.isWaiting())
                yield return null;

            //If they destroyed the ship
            if (board.checkClear())
            {
                //TODO: Add the looting and experience stuff here
                foreach (Aimer a in aimers)
                    a.disableAimers();

                //TODO: Disable Grid, play some kind of 'warping' animation

                yield return new WaitForSeconds(1.0f);

                board.incrLevel();
                board.setPopulated(false);

                //Restart the entire process
                StartCoroutine("CreateElements");
            }

            //if they just hit one weakpoint
            else
            {
                //If aimer index is 0, set to 1. If not, set back to 0
                aimerIndex = aimerIndex == 0 ? 1 : 0;  //TODO: Create running count of aimers, when player upgrades more guns

                //TODO: HANDLE ENEMY GETTING HIT HERE

                aiming = true;
                StartCoroutine("Aim");
            }
        }

        //TODO: ADD NEW CHECK HERE FOR IF THEY HIT ENEMY, BUT NOT WEAKPOINT
        
        //If they missed completely
        else
        {
            aimers[aimerIndex].hitTarget(false);
            
            //TODO: HANDLE THE PLAYER GETTING HURT HERE
            //TODO: HANDLE ENEMY FIRING BACK HERE
                
        }
    }    
	
	// Update is called once per frame
	void Update () {
        if (paused)
        {
            foreach (Aimer a in aimers)
                a.setPause(true);
        }  else
        {
            foreach (Aimer a in aimers)
                a.setPause(false);
        }
	}

    /** Get User Click Input. */ 
    public static bool getInput()
    {
        float yBorder = (Camera.main.orthographicSize * 2) * (3.0f / 4.0f) - 3.5f;
        return (Input.GetMouseButtonDown(0) &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y < yBorder); 
    }

    /** Getter and Setter Methods. */
    public GameManager getInstance() { return instance; }
    public void setInstance(GameManager manager) { instance = manager; }
    public Board getBoard() { return board; }
    public void setBoard(Board newBoard) { board = newBoard; }
    public bool getAiming() { return aiming; }
    public void setAiming(bool newAim) { aiming = newAim; }
    public List<Aimer> getAimers() { return aimers; }
    public void setAimers(List<Aimer> newAimers) { aimers = newAimers; }
    public int getNumAimers() { return numAimers; }
    public void setNumAimers(int newNum) { numAimers = newNum; }
    public bool getPause() { return paused; }
    public void setPause(bool isPaused) { paused = isPaused; }
    public int getScore() { return score; }
    public void setScore(int newScore) { score = newScore; }
}
