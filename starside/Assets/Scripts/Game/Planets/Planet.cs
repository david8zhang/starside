using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Planet : MonoBehaviour {
    
    /// <summary>
    /// The Game Manager instance
    /// </summary>
    public GameManager manager;

    /// <summary>
    /// Game manager prefab
    /// </summary>
    public GameObject managerPrefab;

    /// <summary>
    /// The Board that is associated with this
    /// </summary>
    public Board board;

    /// <summary>
    /// The enemies associated with the HARD PLANET
    /// </summary>
    public List<Enemy> enemies;

    /// <summary>
    /// The position to spawn the game manager
    /// </summary>
    public Vector3 position = new Vector3(4.67f, 3f, 0f);


    // Use this for initialization
    void Start () {

	}

    /// <summary>
    /// Defines how each planet instantiates the game
    /// </summary>
    public abstract void InstantiateGame();
	
	// Update is called once per frame
	void Update () {
	
	}
}
