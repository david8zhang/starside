using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Board : MonoBehaviour {

    //Board Attributes
    public bool populated = false;
    public const int boardSize = 10;

    //Actual board itself
    private int[,] boardCodes;
    private BoardTile[,] gridOutline;
    private EnemyTile[,] enemyOutline; 

    //Prefab objects
    public GameObject tilePrefab;
    public GameObject enemyTile;
    public GameObject enemyPrefab; 

    //Enemy attributes
    private int enemRangeX;
    private int enemRangeY;
    private int numEnemies;
    private List<Enemy> enemyList = new List<Enemy>(); 

    //Use this for preinitialization
    public void Awake()
    {
        //Initalize the gridOutline
        gridOutline = new BoardTile[boardSize, boardSize];
        enemyOutline = new EnemyTile[boardSize, boardSize];
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
                        genEnemy(enemCount, i, j, i + enemRangeX, j + enemRangeY);
                        enemPosX = 5;
                        enemPosY = 6;
                        enemCount++;
                    }
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
        int enemyCode = enemCount + 1; 
        for(int i = startx; i < endx; i++)
        {
            for(int j = starty; j < endy; j++)
            {
                boardCodes[j, i] = enemyCode; 
            }
        }
        float xPos = (startx - 0.9f + endx) / 2;
        float yPos = (starty + endy) / 2; 
        GameObject o = Instantiate(enemyPrefab, new Vector3(xPos, yPos), Quaternion.identity) as GameObject;
        Enemy e = o.GetComponent<Enemy>();
        e.setHealth(2 * (enemRangeX * enemRangeY));
        e.setDamage(25);
        enemyList.Add(e);
    }
    

    public Enemy getEnemy(int enemyCode)
    {
        foreach(Enemy e in enemyList)
        {
            if(e.getCode() == enemyCode)
            {
                return e; 
            }
        }
        return null; 
    }
    
    public List<Enemy> getEnemies()
    {
        return enemyList; 
    }

    public void setEnemies(List<Enemy> enemies)
    {
        enemyList = enemies; 
    }

    public void removeEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        enemyList.Remove(enemy);
    }

    public bool isPopulated()
    {
        return populated; 
    }

    public GameObject CreateNewTile(GameObject prefab, int x, int y)
    {
        GameObject o = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
        o.transform.SetParent(this.transform);
        o.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        o.GetComponent<BoardTile>().board = this;
        return o;
    }

    public void setEnemies(int numEnemies)
    {
        this.numEnemies = numEnemies; 
    }

    public int getNumEnemies()
    {
        return numEnemies; 
    }

    public int[,] getBoardCodes()
    {
        return boardCodes; 
    }
}
