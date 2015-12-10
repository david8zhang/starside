using UnityEngine;
using System.Collections;

public class MediumPlanet : Planet {

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake () {
		ENEMY_HEALTH = 25;
		NUM_ENEMIES = 2; 
		ENEMY_DAMAGE = 10;
	}

}
