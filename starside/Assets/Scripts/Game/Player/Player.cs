using UnityEngine;
using System.Collections;

/**
 * Class containing player attributes and anything
 * that a player is allowed to do in-game (access menus, power-ups, etc.)
 * @author Kevin Lowe
 * @version 1.0 10-8-2015
 */
public class Player : MonoBehaviour {
    /** Player's HP stat */
    private int HP;
    /** Player's Defense stat */
    private int defense;
    /** Player's Luck stat */
    private int luck;

    /** Player's current accumulatd experience */
    private int EXP;
    /** Amount that the player needs to level up */
    private int totalEXP; 
    /** Player's in-game level */
    private int level;
    /** Power-ups that the player currently has and can use in battles. */
    private List<Powerup> upgrades;
    
    /** Returns the player's HP */
    public int getHP() {
        return HP;
    }
    /** Set's the player's HP
     *  @param newHP: stat that the player's HP is changed to */
    public void setHP(int newHP) {
        HP = newHP;
    }
    /** Returns the player's defense stat */
    public int defense() {
        return defense;
    }
    /** Sets the player's defense stat
     *  @param newDefense: stat that the player's defense is changed to */
    public void setDefense(int newDefense) {
        defense = newDefense;
    }
    /** Returns the player's luck stat */
    public int getLuck() {
        return luck;
    }
    /** Sets the player's luck stat
     *  @param newLuck: stat that the player's luck is changed to */
    public void setLuck(int newLuck) {
        luck = newLuck;
    }
    /** Returns the player's EXP stat */
    public int getEXP() {
        return EXP;
    }
    /** Increases EXP based on the parameter
     *  @param EXPinc: amount of EXP that the player's EXP increases by */
    public void incEXP(int addEXP) {
        EXP += addEXP;
    }
    /** Sets the totalEXP of the player based on the level
     *  @param level: level that the totalEXP is based off of */
    public void setEXP(int level) {
        /** TODO figure out EXP curve equation */
    }
    /** Returns the player's level */
    public int getLevel() {
        return level;
    }
    /** Sets the player's level 
     *  @param newLevel: level that the player's level is changed to */
    public void setLevel(int newLevel) {
        level = newLevel;
    }
}
    


    


    
