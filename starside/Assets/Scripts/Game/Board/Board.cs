using UnityEngine;
using System.Collections;
using System;

public class Board : MonoBehaviour {

    //Board Attributes
    public bool populated = false;
    public const int boardSize = 10;

    //Actual board itself
    private int[,] boardCodes;
    private BoardTile[,] gridOutline;

    //Prefab objects
    public GameObject tilePrefab;
    public GameObject enemyTile;

    //Enemy attributes
    private int enemRangeX;
    private int enemRangeY;
    private int numEnemies;

    //Use this for preinitialization
    public void Awake()
    {
        //Initalize the gridOutline
        gridOutline = new BoardTile[boardSize, boardSize];
        boardCodes = new int[boardSize, boardSize];
    }

    //Initalize the board
    public void InitBoard()
    {
        int enemPosX = 1; //Hard coded stuff, just for simplicity sake
        int enemPosY = 1;
        int enemCount = 0;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (enemCount < numEnemies)
                {
                    if (i == enemPosX && j == enemPosY) 
                    {
                        print(j + "," + i);
                        genEnemy(enemCount, i, j, i + enemRangeX, j + enemRangeY);
                        enemPosX = 5;
                        enemPosY = 6; 
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
        populated = true; 
    }

    public void setEnemyRange(int x, int y){
        enemRangeX = x;
        enemRangeY = y; 
    }

    public void genEnemy(int enemCount, int startx, int starty, int endx, int endy)
    {
        for(int i = startx; i < endx; i++)
        {
            for(int j = starty; j < endy; j++)
            {
                boardCodes[j, i] = enemCount + 1; 
                gridOutline[j, i] = CreateNewTile(enemyTile, i, j).GetComponent<BoardTile>() as EnemyTile;
            }
        }
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

    public int[,] getBoardCodes()
    {
        return boardCodes; 
    }
}
