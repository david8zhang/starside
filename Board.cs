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

    /*Initalize the board
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
    }*/
	//Sorry if some of my syntax is completely wrong
	public void InitBoard(int[,] enemyList){//A 2D list of enemies, each row is (width,height, 0, 0) I want those last 2 for later and i don't want to make a new list
		int minWidth = boardSize;//setting this to something bigger than any ship's width
		int minHeight = boardSize;//I dont even end up using the mins but maybe they will be useful someday
		int maxWidth = 0;
		int maxHeight = 0;
		
		for(int x=0; x< enemyList.Length; x++){//not sure how to get size of array in c# i think this is it
			if(minWidth>enemyList[x,0])
				minWidth=enemyList[x,0];
			if(maxWidth<enemyList[x,0])
				maxWidth=enemyList[x,0];
			if(minHeight>enemyList[x,1])
				minHeight=enemyList[x,1];
			if(maxHeight<enemyList[x,1])
				maxHeight=enemyList[x,1];
		}//okay so now we have information about what kinds of ship sizes we are dealing with
		//Note this algorithm will result in small ships not often being very close to each other when there are also big ships in play
		//If this is a problem i can help fix it later
		
		//Only two rules about the partition dimensions. They must be at least big enough to fit the biggest width/height. 
		//They can't be too big that there arent enough spots for all the enemy ships. Here I just made it the size of the biggest dimensions+a constant buffer
		//you can change these up as much as you want as long as those 2 requirements are met nothing should break
		int partitionWidth = maxWidth+2;
		int partitionHeight = maxHeight+2;
		int validPartitionsX = boardWidth/partitionWidth; //The number of full partition widths that could fit
		int validPartitionsY = boardHeight/partitionHeight; //The number of full partition heights that could fit
		bool[,] takenSpots= new bool[validPartitionsX,validPartitionsY];//values that are already taken
		//Note im assuming that takenSpots is all initialized to FALSE in c#
		//Also note that this loop will infinite loop if you have more ships than partition spots!!!!
		int currX;
		int currY;
		for(int x=0; x< enemyList.Length; x++){//Find actual places to put all the enemies
			do{//this is fairly inefficient. Also it will infinite loop if there are no spaces left.
				currX = UnityEngine.Random.Range(0, validPartitionsX);
				currY = UnityEngine.Random.Range(0, validPartitionsY);			
			}while(takenSpots[currX,currY]);
			if(currX==validPartitionsX-1){//Edge case if partitions dont fit perfectly into board, give extra tiles to right side and bottom row
				enemyList[x,2]=currX*partitionWidth+pickSpot(0,partitionWidth+boardWidth%partitionWidth,enemyList[x,0]);			
			}else{
				enemyList[x,2]=currX*partitionWidth+pickSpot(0,partitionWidth,enemyList[x,0]);			
			}
			if(currY==validPartitionsY-1){//Edge case if partitions dont fit perfectly into board, give extra tiles to right side and bottom row
				enemyList[x,3]=currY*partitionHeight+pickSpot(0,partitionHeight+boardHeight%partitionHeight,enemyList[x,1]);				
			}else{
				enemyList[x,3]=currY*partitionHeight+pickSpot(0,partitionHeight,enemyList[x,1]);			
			}
			takenSpots[currX,currY]=true;//mark this partition spot as taken			
		}	
		
		//You now have a list of lists called enemyList. Each list in enemyList contains a list [<enemywidth>,<enemyheight>,<boardxpos>,<boardypos>]
		//wasn't sure exactly how you wanted to go about putting them onto the board but this is where they all go
	}
	public int pickSpot(int min, int max, int width){//Picks a random spot between min and max where returnSpot+width is also still between min and max
		return UnityEngine.Random.Range(min, max-width);
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
