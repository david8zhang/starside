using UnityEngine;
using System.Collections;

public class EasyPlanet : Planet {

    /// <summary>
    /// Constants for the enemies generated
    /// </summary>
    private static int ENEMY_DAMAGE = 5;
    private static int ENEMY_HEALTH = 10;
    private static int NUM_ENEMIES = 1;

    /// <summary>
    /// Implement the Instantiate Game
    /// </summary>
    override
    public void InstantiateGame()
    {
        GameObject obj = Instantiate(managerPrefab, position, Quaternion.identity) as GameObject;
        manager = obj.GetComponent<GameManager>();
        board = manager.getBoard();
        board.setEnemies(NUM_ENEMIES);
        enemies = board.getEnemies();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].setHealth(ENEMY_HEALTH);
            enemies[i].setDamage(ENEMY_DAMAGE);
        }
        LevelManager lm = transform.GetComponentInParent<LevelManager>();
        lm.DeactivatePlanets();
		lm.transform.Find ("Canvas").gameObject.SetActive (true); 
    }


}
