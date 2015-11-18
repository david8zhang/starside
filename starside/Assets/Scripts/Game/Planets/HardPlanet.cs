using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HardPlanet : Planet {

    /// <summary>
    /// Constants for the enemies generated
    /// </summary>
    private static int ENEMY_DAMAGE = 25;
    private static int ENEMY_HEALTH = 50;
    private static int NUM_ENEMIES = 2; 

    /// <summary>
    /// Instantiate the game and set the board attributes
    /// </summary>
    public override void InstantiateGame()
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
    }
}
