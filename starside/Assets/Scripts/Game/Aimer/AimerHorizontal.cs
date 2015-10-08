using UnityEngine;
using System.Collections;

public class AimerHorizontal : MonoBehaviour {

	// References to Board object properties
	public Board board;
	int boardSize;
	
	// Reference to the AimerCenter object to set its position
	public AimerCenter aimerC;
	
	public bool aiming;		// whether this aimer bar is aiming
	public bool paused;
	public float speed;		// speed of the aimer (higher = faster)

    public float offsetX = .3f;
    public float offsetY = 1.01f; 
	
	// prefabs for left, middle, and right sprites
	public GameObject prefabLeft;
	public GameObject prefabMid;
	public GameObject prefabRight;
	
	// The y coordinate after this aimer has finished aiming
	public float targetY;

    /* Custom timer for Mathf.PingPong when the aimer is aiming
	 * Counter will stop when the aimer stops, thus conserving the
	 * position of the aimer so when it calls Mathf.PingPong again,
	 * it will continue from its current position
	 * 
	 * This is needed because Mathf.PingPong is a simple function
	 * */

    private float counter = 0.0f;


    void Awake(){
		// Create the sprites that make up the bar
		boardSize = Board.boardSize;
		//Create the outer aimer pieces
		CreateAimerPiece (prefabLeft, -boardSize/2);
		CreateAimerPiece (prefabRight, boardSize/2 - 1);
		// the starting and stopping i values are +1 and -1 to exclude the left and right pieces
		for (int i = -boardSize / 2 + 1; i < boardSize / 2 - 1; i ++) {
			CreateAimerPiece(prefabMid, i);
		}
	}

	void CreateAimerPiece(GameObject prefab, int xPos) {
		// set the World Position to this instance
		GameObject o = Instantiate (prefab, this.transform.position, Quaternion.identity) as GameObject;
		
		// set the Local Position to the xPos specified
		Vector3 localPos = new Vector3(xPos + offsetX, 0 + offsetY, 0);
		
		// set this to be the object's parent and set local position
		o.transform.SetParent(this.transform);
		o.transform.localPosition = localPos;
	}

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
        if (aiming && !paused)
        {
            counter += speed * Time.deltaTime;
            float yPos = Mathf.PingPong(counter, (boardSize - 1)) - offsetY;
            setPosition(new Vector3(transform.position.x, yPos));
        }
    }

    //Sets the position of the center piece
    void setPosition(Vector3 pos)
    {
        transform.position = pos;
        aimerC.setY(this.transform.position.y + offsetY);
    }
}
