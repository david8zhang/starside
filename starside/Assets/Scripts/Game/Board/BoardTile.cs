using UnityEngine;
using System.Collections;

public class BoardTile : MonoBehaviour {

    private Board board;
    protected Animator anim;

    private int pointValue;

    public Board getBoard() { return board; }
    public void setBoard(Board newBoard) { board = newBoard; }
    public int getPoint() { return pointValue; }
    public void setPoint(int newPoint) { pointValue = newPoint; }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void animate()
    {
        anim.SetTrigger("Hit");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
