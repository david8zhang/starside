using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HardPlanet : Planet {

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake () {
		ENEMY_HEALTH = 50;
		NUM_ENEMIES = 4; 
		ENEMY_DAMAGE = 25;
	}
}
