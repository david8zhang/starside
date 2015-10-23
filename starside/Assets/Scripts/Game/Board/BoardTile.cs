using UnityEngine;
using System.Collections;

public class BoardTile : MonoBehaviour {

    public Board board;
    public RandomGen randomGen;
    protected Animator anim;

    public int pointValue;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void animate()
    {
        anim.SetTrigger("Hit");
    }

    public void deactivate()
    {
        print("Deactivated");
        gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
