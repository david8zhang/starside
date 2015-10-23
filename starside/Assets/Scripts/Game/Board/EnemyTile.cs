using UnityEngine;
using System.Collections;

public class EnemyTile : BoardTile {
    public void deactivate()
    {
        print("Deactivated");
        gameObject.SetActive(false);
    }
}
