using UnityEngine;
using System.Collections;
using System;

public class HealthUP : Powerup {
	/** Factor that the HP of the player increases by */
	private int factor;
	
	public void Start() {
		factor = 0;
	}
	
	public override void effect(Player p)
	{
		p.setHP(p.getHP() + factor);
	}
}
