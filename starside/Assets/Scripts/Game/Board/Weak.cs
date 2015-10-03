using UnityEngine;
using System.Collections;

public class Weak : BoardTile {

    public override void Hit()
    {
        //ScoreManager.instance.IncrementButtonsPressed();

        if (Application.loadedLevelName.Equals("Tutorial"))
           // TutorialGameManager.instance.score += pointValue;
        else
            pointValue += 1; 

        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        // set the board at this tile's position to null so this tile no longer registers as enabled
        board.getBoardTile()[y, x] = null;
        gameObject.SetActive(false);

        board.getFloorTiles()[y, x].gameObject.SetActive(true);
        board.getFloorTiles()[y, x].animate();
    }
}
