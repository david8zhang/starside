using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Class containing player attributes and anything
 * that a player is allowed to do in-game (access menus, power-ups, etc.)
 * @author Kevin Lowe
 * @version 1.0 10-8-2015
 */
public class Player : MonoBehaviour
{
    /** Player's HP stat */
    private int HP;
    /** Player's Defense stat */
    private int defense;
    /** Player's Luck stat */
    private int luck;
    /** Player's Attack stat */
    private int attack;

    /** Player's current accumulatd experience */
    private int EXP;
    /** Amount that the player needs to level up */
    private int totalEXP;
    /** Player's in-game level */
    private int level;
    /** Attributes that the player has without any power-ups */
    private Dictionary<string, int> attributes;
    /** Power-ups that the player currently has and can use in battles. */
    private List<Powerup> upgrades;

    /** Returns the player's HP */
    public int getHP()
    {
        return HP;
    }
    /** Set's the player's HP
     *  @param newHP: stat that the player's HP is changed to */
    public void setHP(int newHP)
    {
        HP = newHP;
    }
    /** Player takes hit and decreases based on given damage
     *  @param damage: damage that the player takes */
    public void takeHit(int damage)
    {
        HP -= (damage - defense);
    }
    /** Returns the player's defense stat */
    public int getDefense()
    {
        return defense;
    }
    /** Sets the player's defense stat
     *  @param newDefense: stat that the player's defense is changed to */
    public void setDefense(int newDefense)
    {
        defense = newDefense;
    }
    /** Returns the player's luck stat */
    public int getLuck()
    {
        return luck;
    }
    /** Sets the player's luck stat
     *  @param newLuck: stat that the player's luck is changed to */
    public void setLuck(int newLuck)
    {
        luck = newLuck;
    }

    /** Sets the player's attack stat
    *  @param newAttack: stat that the player's attack is changed to */
    public void setAttack(int newAttack)
    {
        attack = newAttack;
    }

    /** Returns the player's attack stat. */
    public int getAttack()
    {
        return attack; 
    }

    /** Returns the player's EXP stat */
    public int getEXP()
    {
        return EXP;
    }
    /** Increases EXP based on the parameter
     *  @param EXPinc: amount of EXP that the player's EXP increases by */
    public void incEXP(int addEXP)
    {
        EXP += addEXP;
    }
    /** Sets the totalEXP of the player based on the level
     *  @param level: level that the totalEXP is based off of */
    public void setEXP(int level)
    {
        /** TODO figure out EXP curve equation */
    }
    /** Returns the player's level */
    public int getLevel()
    {
        return level;
    }
    /** Sets the player's level 
     *  @param newLevel: level that the player's level is changed to */
    public void setLevel(int newLevel)
    {
        level = newLevel;
    }

    /** Checks to see if the player's EXP is over the total
     *  for a level up */
    public bool isLeveling()
    {
        return EXP >= totalEXP;
    }
    /** Executes a level up
     *  TODO increases stats in some way, adjust in attributes list */
    public void levelUP()
    {
        level += 1;
        this.setEXP(level);
    }

    /** Refreshes the stats after a battle, in case a power up is used */
    public void refresh()
    {
        //HP = attributes.tryGetValue("HP");
    }
}