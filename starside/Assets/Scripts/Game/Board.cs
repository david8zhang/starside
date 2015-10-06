using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
    
    //Board Attributes
    private const int boardSize = 10;

    //Actual board itself
    private int boardGen;
    private BoardTile[,] gridOutline;

    //Prefab objects
    public GameObject tilePrefab;

    public bool populated = false; 
        
    //Use this for pre-initialization
    void Awake()
    {
        //Initalize the gridOutline
        gridOutline = new BoardTile[boardSize, boardSize];
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
        o.GetComponent<BoardTile>().setBoard(this);
        return o;
    }
}
