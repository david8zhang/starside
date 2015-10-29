using UnityEngine;
using System.Collections;

public class DefenseUp : Powerup {
	/** Factor that the attribute increases by */
	public int factor;
	
	public void Start() {
		factor = 0;
	}
	
	public override void effect(Player p) {
		p.setDefense(p.getDefense() + factor);
	}
}


