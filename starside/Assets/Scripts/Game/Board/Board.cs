using UnityEngine;
using System.Collections;
using System;

public class Board : MonoBehaviour {
    
    //Board Attributes
    public bool populated = false; 
    public const int boardSize = 10;

    //Actual board itself
    private int[,] boardGen;
    private BoardTile[,] gridOutline;

    //Prefab objects
    public GameObject tilePrefab;
    public GameObject enemyTile; 

    //Enemy attributes
    private int enemRangeX = 3;
    private int enemRangeY = 2;
    private int numEnemies; 

    //Use this for preinitialization
    public void Awake()
    {
        //Initalize the gridOutline
        gridOutline = new BoardTile[boardSize, boardSize];
        boardGen = new int[boardSize, boardSize];
    }

    //Initalize the board
    public void InitBoard()
    {
        int enemPosX = UnityEngine.Random.Range(0, 5);
        int enemPosY = UnityEngine.Random.Range(0, 5);
        int enemCount = 0; 
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (enemCount < numEnemies)
                {
                    if (i == enemPosX && j == enemPosY)
                    {
                        genEnemy(i, j, i + enemRangeX, j + enemRangeY);
                        int newPosX = UnityEngine.Random.Range(5, 10 - enemRangeX);
                        int newPosY = UnityEngine.Random.Range(5, 10 - enemRangeY);
                        enemPosX = newPosX;
                        enemPosY = newPosY; 
                        enemCount++; 
                    }
                    else
                    {
                        gridOutline[j, i] = CreateNewTile(tilePrefab, i, j).GetComponent<BoardTile>() as BoardTile;
                    }
                }

                else
                {
                    gridOutline[j, i] = CreateNewTile(tilePrefab, i, j).GetComponent<BoardTile>() as BoardTile;
                }
            }
        }
    }

    public void genEnemy(int startx, int starty, int endx, int endy)
    {
        for(int i = startx; i < endx; i++)
        {
            for(int j = starty; j < endy; j++)
            {
                gridOutline[j, i] = CreateNewTile(enemyTile, i, j).GetComponent<BoardTile>() as EnemyTile;
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

    public GameObject CreateNewTile(GameObject prefab, int x, int y)
    {
        GameObject o = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
        o.transform.SetParent(this.transform);
        o.GetComponent<BoardTile>().board = this;
        return o;
    }

    public void setEnemies(int numEnemies)
    {
        this.numEnemies = numEnemies; 
    }
}
