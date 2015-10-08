using UnityEngine;
using System.Collections;

public class AimerVertical : MonoBehaviour {

	public Board board;
	int boardSize;
	
	public AimerCenter aimerC;
	
	public bool aiming;
	public bool paused;
	public float speed;
	
	public GameObject prefabTop;
	public GameObject prefabMid;
	public GameObject prefabBottom;
	
	// The x coordinate after this aimer has stopped
	public float targetX;
	
	private float counter = 0.0f;

	void Awake()
	{
		// Create the sprites that make up the bar
		boardSize = Board.boardSize;
		
		CreateAimerPiece (prefabBottom, -boardSize / 2);
		CreateAimerPiece (prefabTop, boardSize / 2 - 1);
		// the starting and stopping i values are +1 and -1 to exclude the left and right pieces
		for (int i = -boardSize / 2 + 1; i < boardSize / 2 - 1; i ++)
		{
			CreateAimerPiece(prefabMid, i);
		}
	}

	void CreateAimerPiece(GameObject prefab, int yPos) {
		// set the World Position to this instance
		GameObject o = Instantiate (prefab, this.transform.position, Quaternion.identity) as GameObject;
		
		// set the Local Position to the xPos specified
		Vector3 localPos = new Vector3(0, yPos, 0);
		
		// set this to be the object's parent and set local position
		o.transform.SetParent(this.transform);
		o.transform.localPosition = localPos;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
