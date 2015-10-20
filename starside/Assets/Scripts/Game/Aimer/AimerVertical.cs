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

    public float offsetX;
    public float offsetY;
	
	// The x coordinate after this aimer has stopped
	private float targetX;
	
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
		Vector3 localPos = new Vector3(0 + offsetX, yPos + offsetY, 0);
		
		// set this to be the object's parent and set local position
		o.transform.SetParent(this.transform);
		o.transform.localPosition = localPos;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(aiming  && !paused)
        {
            counter += speed * Time.deltaTime;
            float xPos = Mathf.PingPong(counter, boardSize - 1) - offsetX;
            setPosition(new Vector3(xPos, transform.position.y));
        }

	}

    //Sets the position of the center piece
    void setPosition(Vector3 pos)
    {
        transform.position = pos;
        aimerC.setX(this.transform.position.x + offsetX);
    }

    //Snap to the position
    public void snap()
    {
        counter = Mathf.Round(counter);
        float xPos = Mathf.Round(transform.position.x);
        targetX = xPos;
        print(targetX);
        StartCoroutine("smoothSnap");
    }

    IEnumerator smoothSnap()
    {
        Vector3 destPos = new Vector3(Mathf.Round(transform.position.x) - offsetX,
                                      Mathf.Round(transform.position.y));
        Vector3 velocity = Vector3.zero;

        float counter = 0.0f;
        while (Vector3.Distance(transform.position, destPos) > Mathf.Epsilon &&
            counter <= 0.05f)
        {
            counter += Time.deltaTime;
            //Smoothens the transition for when the aimer stops
            setPosition(Vector3.SmoothDamp(transform.position, destPos, ref velocity, 0.05f));
            yield return null;
        }
        setPosition(destPos);
        yield return null;
    }

    public float getTargetX()
    {
        return targetX;
    }

    public void setTargetX(float newTarget)
    {
        targetX = newTarget; 
    }
}
