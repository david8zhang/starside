using UnityEngine;
using System.Collections;

public class MediumPlanet : Planet {

	public GameObject enemPrefab;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake () {
		ENEMY_HEALTH = 25;
		NUM_ENEMIES = 2; 
		ENEMY_DAMAGE = 10;
		ENEMY_PREFAB = enemPrefab;
	}

}
