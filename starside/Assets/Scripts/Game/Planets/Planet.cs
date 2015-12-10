using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Planet : MonoBehaviour {
    
	/// <summary>
	/// The health of the enemy
	/// </summary>
	public int ENEMY_HEALTH;

	/// <summary>
	/// The number of enemies
	/// </summary>
	public int NUM_ENEMIES;

	/// <summary>
	/// The Enemy Damage
	/// </summary>
	public int ENEMY_DAMAGE;

	/// <summary>
	/// The enemy prefab (for determining sprites)
	/// </summary>
	public GameObject ENEMY_PREFAB;

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

	/// <summary>
	/// Implement the Instantiate Game
	/// </summary>
	public void InstantiateGame()
	{
		GameObject obj = Instantiate(managerPrefab, position, Quaternion.identity) as GameObject;
		manager = obj.GetComponent<GameManager>();
		board = manager.getBoard();
		Vector3[] startpos = setStartPos (); 
		int[] health = setHealth ();
		int[] damage = setDamage ();
		board.InitBoard (startpos, NUM_ENEMIES, health, damage, ENEMY_PREFAB);  
		LevelManager lm = transform.GetComponentInParent<LevelManager>();
		lm.DeactivatePlanets();
	}

	/// <summary>
	/// Sets the start position.
	/// </summary>
	/// <returns>The start position.</returns>
	Vector3[] setStartPos() {
		Vector3[] result = new Vector3[4];
		result [0] = new Vector3 (2.55f, 2f, 0f);
		result [1] = new Vector3 (7.55f, 7f, 0f);
		result [2] = new Vector3 (2.55f, 7f, 0f);
		result [3] = new Vector3 (7.55f, 2f, 0f);
		return result;
	}
	
	/// <summary>
	/// Sets the health.
	/// </summary>
	/// <returns>The health.</returns>
	int[] setHealth() {
		int[] healths = new int[4];
		for(int i = 0; i < 4; i++) {
			healths[i] = ENEMY_HEALTH;
		}
		return healths;
	}
	
	int[] setDamage() {
		int[] damages = new int[4];
		for (int i = 0; i < 4; i++) {
			damages[i] = ENEMY_DAMAGE;
		}
		return damages;
	}
}
