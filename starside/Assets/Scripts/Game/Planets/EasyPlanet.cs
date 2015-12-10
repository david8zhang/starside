using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasyPlanet : Planet {

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake () {
		ENEMY_HEALTH = 10;
		NUM_ENEMIES = 1; 
		ENEMY_DAMAGE = 5;
	}
}
