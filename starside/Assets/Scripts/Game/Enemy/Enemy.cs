using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Author: David Zhang
    /// </summary>
    public int damage;
    public int enemyCode;
    private int health;
    private bool isDead = false;

    /// <summary>
    /// Set the waypoints as a series of booleans tracking the enemy's current position
    /// </summary>
    private Vector3[] waypoints;

    /// <summary>
    /// The current point that the enemy is at, represented in the boolean array
    /// </summary>
    private int currPoint;

    /// <summary>
    /// The current start position
    /// </summary>
    Vector3 startPos; 

    /// <summary>
    /// Generate a waypoint array to determine the path (Default is 4) 
    /// </summary>
	public void Awake(){
        startPos = new Vector3(2.55f, 2.0f, transform.position.z);
        waypoints = new Vector3[4] {
           startPos,
           new Vector3(startPos.x, startPos.y + 5.0f, startPos.z),
           new Vector3(startPos.x + 5.0f, startPos.y + 5.0f, startPos.z),
           new Vector3(startPos.x + 5.0f, startPos.y, startPos.z)
        }; 
	}

    /// <summary>
    /// Set the current point at which the ship is stopped
    /// </summary>
    /// <param name="point"></param>
    public void SetCurrPoint(int point)
    {
        currPoint = point; 
    }

    /// <summary>
    /// Define a custom path for this enemy to travel
    /// </summary>
    /// <param name="waypoints"></param>
    public void SetWaypoints(Vector3[] waypoints)
    {
        this.waypoints = waypoints; 
    }


    public IEnumerator Start()
    {
        var pointA = transform.position; 
        while(true)
        {
            int nextPoint = currPoint + 1; 
            if(nextPoint == waypoints.Length)
            {
                nextPoint = 0;
            }
            yield return StartCoroutine(Patrol(transform, waypoints[currPoint], waypoints[nextPoint], 3.0f));
            currPoint = nextPoint;
        }
    }

    /// <summary>
    /// Patrol logic is defined in here
    /// </summary>
    IEnumerator Patrol(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            if(transform.position == endPos)
            {
                //Define enemy attacking logic here
                yield return new WaitForSeconds(1.5f);
            } 
            yield return null;
        }
    }

    /// <summary>
    /// Attack the player PLAYER
    /// </summary>
    /// <param name="player"></param>
    public void attackPlayer(Player player)
    {
        print("Enemy " + enemyCode + " attacked player!");
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
