using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    //Game elements
    private Board board; 

    // Pre-initialization method
    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Initialize the board
        board = GetComponentInChildren<Board>();

	}

    void Start()
    {
        board.InitBoard();
        StartCoroutine("Init");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
