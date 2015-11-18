using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //Game Manager instance itself
    private static GameManager instance;

    //Game attributes
    private bool aiming; 

    //Game elements
    private Board board; 
    private Aimer aimer;
	public Player player;
    private int aimerIndex;

    /// <summary>
    /// Getter and setters
    /// </summary>
    /// <param name="newPlayer"></param>
    public void setPlayer(Player newPlayer) { player = newPlayer;  }
    public Player getPlayer () { return player;  }
    public void setBoard(Board newBoard) { board = newBoard; }
    public Board getBoard() { return board; }

    /// <summary>
    /// Pre-iniitalization
    /// </summary>
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Initialize the board
        board = GetComponentInChildren<Board>();

		//Initialize player
		player = transform.FindChild ("Player").GetComponent<Player> ();

        //Grab the aimer game objects
        aimer = transform.FindChild("Aimer").GetComponent<Aimer>();

        //Set the Player Attributes
		player.setAttack(10);
    }

    void Start()
    {
        board.setEnemies(2); //Hard coded
        board.setEnemyRange(4, 3); //Hard Coded
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

        aimer.Aim();
        Enemy e = null; 
        bool hit = false;
        while (aiming)
        {
            if (aimer.getAimed())
            {
                aiming = false;
                hit = aimer.CheckHit();
                e = aimer.GetHitEnemy();
            }
            yield return null;
        }
        aimer.setAimed(false);
        StartCoroutine(ProcessAim(hit, e));
    }
    
    //Process the aimed down location
    IEnumerator ProcessAim(bool hit, Enemy e)
    {
        if (hit)
        {
            e.takeHit(player.getAttack());
            print("Enemy " + e.getCode() + " Health: " + e.getHealth());
            if (e.getDead())
            {
                board.removeEnemy(e);
            }
        }
        else
        {
            List<Enemy> enemies = board.getEnemies(); 
            foreach(Enemy enem in enemies)
            {
//                enem.attackPlayer(player);
                print("Player Health: " + player.getCurrHP());
            }
        }
        yield return new WaitForSeconds(10.0f / 60.0f);
        aiming = true;
        if (board.getEnemies().Count == 0)
        {
            Application.LoadLevel("GameOver");
        } else
        {
            StartCoroutine("Aim");
        }
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
