using UnityEngine;
using System.Collections;

public abstract class BoardTile : MonoBehaviour {

    protected Board board; 
    protected Animator anim;
    protected int pointValue;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void animate()
    {
        anim.SetTrigger("Hit");
    }

    public abstract void Hit();


    //Getters and setters for private variables
    public void setBoard(Board newBoard) { board = newBoard; }
    public Board getBoard() { return board; }

    public void setValue(int newValue) { pointValue = newValue; }
    public int getValue() { return pointValue; }
}
