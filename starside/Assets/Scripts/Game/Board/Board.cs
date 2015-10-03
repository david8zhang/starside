using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    /** Number of tiles there are in the board */ 
    private int boardSize = 10;

    /** the 2D array that actually defines the board spaces */
    private int[,] boardSpace;
    private Floor[,] floorTiles;
    private BoardTile[,] board;

    /** Prefab objects that determine this board */
    public GameObject floorPrefab;
    public GameObject weakPointPrefab;
    public GameObject bigWeakPrefab;

    /** See if the board is actually populated */ 
    private bool populated;

    /** Makes the game manager wait until all buttons are finished pressing */ 
    private bool waiting;
    private float waitTimer;
    private int level;

    //Pre-initialization methods 
    void Awake()
    {
        boardSpace = new int[boardSize, boardSize];
        floorTiles = new Floor[boardSize, boardSize];
        board = new BoardTile[boardSize, boardSize];
    }


	// Use this for initialization
	void Start () {
	    
	}

    //Initalize Board with floor tiles
    public void InitBoard()
    {
        for(int x = 0; x < boardSize; x++)
        {
            for(int y = 0; y < boardSize; y++)
            {
                floorTiles[y, x] = CreateNewTile(floorPrefab, x, y).GetComponent<BoardTile>() as Floor;
            }
        }
    }

    //Populate board with things
    public void PopulateBoardFromLayout(int[,] layout)
    {
        boardSpace = layout;
        StartCoroutine("InitBoardAnim");
    }

    public void PopulateBoard()
    {
        //reset the boardSpace
        for(int x = 0; x < boardSize; x++)
        {
            for(int y = 0; y < boardSize; y++)
            {
                boardSpace[y, x] = 0;
            }
        }

        //TODO: DEFINE ENEMY GENERATION NUMBER HERE
        int numWeaks = 20;
        while(numWeaks > 0)
        {
            int randX = Random.Range(0, boardSize);
            int randY = Random.Range(0, boardSize);

            if(boardSpace[randY, randX] == 0)
            {
                if (Random.value < 0.75f)
                    boardSpace[randY, randX] = 1;
                else
                    boardSpace[randY, randX] = 2;

                numWeaks--; 
            }
        }
        StartCoroutine("InitBoardAnim"); 
    }

    IEnumerator InitBoardAnim()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                // if the boardGen is set to something that is not a floor tile
                if (boardSpace[y, x] != 0)
                {
                    // have a random delay before animating the tiles so they don't all animate at the same time
                    float delay = Random.value;
                    GameObject obj = null;

                    // create the correct prefab
                    if (boardSpace[y, x] == 1)
                        obj = CreateNewTile(weakPointPrefab, x, y);
                    else if (boardSpace[y, x] == 2)
                        obj = CreateNewTile(bigWeakPrefab, x, y);

                    obj.SetActive(false);

                    //Coroutine to animate the tiles when board is initialized
                    StartCoroutine(AnimateTile(obj, delay, x, y));

                }
                // else make the floor tile at the coordinate visible
                else
                    floorTiles[y, x].gameObject.SetActive(true);
            }
        }
        populated = true;
        yield return null;
    }

    /**Creates new tiles with the given gameObject Prefabs, and positions */
    public GameObject CreateNewTile(GameObject prefab, int x, int y)
    {
        GameObject o = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
        o.transform.SetParent(this.transform);
        o.GetComponent<BoardTile>().setBoard(this);
        return o;
    }

    IEnumerator AnimateTile(GameObject tile, float delay, int x, int y)
    {
        yield return new WaitForSeconds(delay);

        // make the floor tile invisible and the enemy tile visible
        floorTiles[y, x].gameObject.SetActive(false);
        tile.SetActive(true);

        // add the prefab to the board array and animate tile
        BoardTile weak = tile.GetComponent<BoardTile>();
        board[y, x] = weak;
        weak.animate();
    }

    public bool checkIfBoardClear()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (board[y, x] != null)
                    return false;
            }
        }
        return true;
    }

    public void Wait()
    {
        // if waitTimer coroutine has been started already
        if (waitTimer > 0)
        {
            waitTimer = 1.0f;
        }
        else
        {
            waitTimer = 1.0f;
            StartCoroutine("WaitForButtonProcess");
        }
    }

    IEnumerator WaitForButtonProcess()
    {
        waiting = true;
        while (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            yield return null;
        }
        waiting = false;
        yield return null;
    }

    public void incrLevel()
    {
        level += 1; 
    }

    //Getter and Setter methods
    public void setBoardSpace(int[,] newSpace) { boardSpace = newSpace; }
    public int[,] getBoardSpace() { return boardSpace; }
    public void setFloorTiles(Floor[,] newTiles) { floorTiles = newTiles; }
    public Floor[,] getFloorTiles() { return floorTiles; }
    public void setBoardTile(BoardTile[,] newBoardTile) { board = newBoardTile; }
    public BoardTile[,] getBoardTile() { return board; }
    public void setPopulated(bool isPop) { populated = isPop; }
    public bool getPopulated() { return populated; }
    public void setWaiting(bool newWait) { waiting = newWait; }
    public bool getWaiting() { return waiting; }
    public void setWaitTimer(float newWaitTimer) { waitTimer = newWaitTimer; }
    public float getWaitTimer() { return waitTimer; }
    public void setLevel(int newLevel) { level = newLevel; }
    public int getLevel() { return level; }

    // Update is called once per frame
    void Update () {
	    
	}
}
