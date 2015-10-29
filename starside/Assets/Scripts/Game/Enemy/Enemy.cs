using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Author: David Zhang
    /// </summary>
    private int damage;
    private int enemyCode;
    private int health;
    private bool isDead; 

	public void Awake(){
		damage = 10;
		health = 20;
		isDead = false;
	}

    /// <summary>
    /// Attack the player PLAYER
    /// </summary>
    /// <param name="player"></param>
    public void attackPlayer(Player player)
    {
        player.takeHit(damage);
    }

    /// <summary>
    /// Enemy takes a hit
    /// </summary>
    /// <param name="damage"></param>
    public void takeHit(int damage)
    {
        if(health - damage <= 0)
        {
            isDead = true; 
        } else
        {
            health -= damage; 
        }
    }

    public bool getDead()
    {
        return isDead; 
    }

    /// <summary>
    /// Set the damage to DAMAGE
    /// </summary>
    /// <param name="damage"></param>
    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    /// <summary>
    /// Return the damage of this player
    /// </summary>
    /// <returns></returns>
    public double getDamage()
    {
        return damage;
    }

    /// <summary>
    /// Set the enemy identification code to CODE
    /// </summary>
    /// <param name="code"></param>
    public void setCode(int code)
    {
        enemyCode = code;
    }

    /// <summary>
    /// Return the enemy identification code
    /// </summary>
    /// <returns></returns>
    public int getCode()
    {
        return enemyCode;
    }

    /// <summary>
    /// Set the enemy health to NEWHEALTH
    /// </summary>
    /// <param name="newHealth"></param>
    public void setHealth(int newHealth)
    {
        health = newHealth;
    }



    /// <summary>
    /// Return this health
    /// </summary>
    /// <returns></returns>
    public int getHealth()
    {
        return health;
    }

}
