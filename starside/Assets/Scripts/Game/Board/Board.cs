using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
    
    //Board Attributes
    public bool populated = false; 
    public const int boardSize = 10;

    //Actual board itself
    private int[,] boardGen;
    private BoardTile[,] gridOutline;

    //Prefab objects
    public GameObject tilePrefab;
        
    //Use this for pre-initialization
    void Awake()
    {
        //Initalize the gridOutline
        gridOutline = new BoardTile[boardSize, boardSize];
        boardGen = new int[boardSize, boardSize];
    }

    //Initalize the board
    public void InitBoard()
    {
        for(int i = 0; i < boardSize; i++)
        {
            for(int j = 0; j < boardSize; j++)
            {
                gridOutline[j, i] = CreateNewTile(tilePrefab, i, j).GetComponent<BoardTile>() as BoardTile;
            }
        }
    }

    //Assign numerical values to all elements on board
    public void PopulateBoard()
    {
        for(int x = 0; x < boardSize; x++)
        {
            for(int y = 0; y < boardSize; y++)
            {
                boardGen[y, x] = 0; 
            }
        }
        populated = true; 

        //Create enemies here
    }

    public bool isPopulated()
    {
        return populated; 
    }
	
    // Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject CreateNewTile(GameObject prefab, int x, int y)
    {
        GameObject o = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
        o.transform.SetParent(this.transform);
        o.GetComponent<BoardTile>().board = this;
        return o;
    }
}
