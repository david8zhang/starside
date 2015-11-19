using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractEnemy : MonoBehaviour
{
	/** My current HP. */
	protected int currHP;
	/** The damage that I can inflict. */
	protected int damage;
	/** My enemy code, used to relate to other enemies. */
	protected int enemyCode;
	/** My total health. */
	protected int health;
	/** True if my currHP is <= 0. */
	protected boolean dead;

	/** The locations I will hit in my movement. */
	protected List<Vector3> waypoints;

	/** My current point in the waypoints array. */
	protected int currPoint;
	/** My start position. */
	protected Vector3 startPos;

	/** Start my movement. */
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

	/** Moves to my next location, then pauses. */
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

	/** Returns my current HP. */
	public int getCurrentHP()
	{
		return currHP;
	}

	/** Sets my current HP to the given value NEWHP. */
	public void setCurrentHP(int newHP)
	{
		currHP = newHP;
	}

	/** Returns my damage value. */
	public int getDamage()
	{
		return damage;
	}

	/** Sets my damage value to the given value NEWDAMAGE. */
	public void setDamage(int newDamage) {
		damage = newDamage;
	}

	/** Returns my enemy code. */
	public void getCode()
	{
		return enemyCode;
	}

	/** Sets my enemy code to the given int value CODE. */
	public void setCode(int code)
	{
		enemyCode = code;
	}

	/** Returns my total health. */
	public int getHealth()
	{
		return health;
	}

	/** Sets my total health to the value NEWHEALTH. */
	public void setHealth(int newHealth)
	{
		health = newHealth;
	}

	/** Returns true if I am dead, false othewise. */
	public boolean isDead()
	{
		return dead;
	}

	/** Adds a new location that I will visit in my movement. */
	public void addLocation(Vector3 loc)
	{
		waypoints.Add (loc);
	}

	/** Returns all the locations that I visit in my movement. */
	public List<Vector3> getWaypoints()
	{
		return waypoints;
	}

	/** Sets my waypoints to this custom set given by NEWLOCATIONS. */
	public void setWaypoints(List<Vector3> newLocations) {
		waypoints = newLocations;
	}

	/** Inflicts damage onto some player. */
	public void hitPlayer(Player player)
	{
		player.takeHit (damage);
	}

	/** Takes hit equal to the value TAKEDAMAGE. */
	public void takeHit(int takeDamage)
	{
		if (health - damage <= 0) {
			dead = true; 
		} else {
			health -= damage; 
		}
	}
}

