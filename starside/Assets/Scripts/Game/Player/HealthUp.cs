using UnityEngine;
using System.Collections;

public class HealthUP : Powerup {
    /** Factor that the HP of the player increases by */
    private int factor;

    public void effect(Player p) {
        p.setHP(p.getHP() + factor);
    }
}
