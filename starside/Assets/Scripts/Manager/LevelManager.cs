using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    /// <summary>
    /// An array of levels, each with unique battle scenes.
    /// </summary>
    private Planet[] levels;

    /// <summary>
    /// The prefabricated gameObject to instantiate
    /// </summary>
    public GameObject easyPrefab;

    /// <summary>
    /// Medium Planets
    /// </summary>
    public GameObject mediumPrefab;


    /// <summary>
    /// Hard planets
    /// </summary>
    public GameObject hardPrefab; 

    /// <summary>
    /// The index of the current level
    /// </summary>
    private int index;

    /// <summary>
    /// The number of levels in this level manager
    /// </summary>
    private int numLevels = 3; 

    /// <summary>
    /// The camera
    /// </summary>
    private Transform focus;

    /// <summary>
    /// Preinitialization
    /// </summary>
    void Awake()
    {
        focus = transform.Find("Focus");
        levels = new Planet[numLevels]; //hard coded right now, change later
        for(int i = 0; i < numLevels; i++)
        {
            if(i % 3 == 0)
            {
                GameObject obj = GeneratePlanet(easyPrefab, i);
                Planet planet = obj.GetComponent<EasyPlanet>();
                levels[i] = planet;
            }
            else if(i % 3 == 1)
            {
                GameObject obj = GeneratePlanet(mediumPrefab, i);
                Planet planet = obj.GetComponent<MediumPlanet>();
                levels[i] = planet; 
            } else
            {
                GameObject obj = GeneratePlanet(hardPrefab, i);
                Planet planet = obj.GetComponent<HardPlanet>();
                levels[i] = planet;
            }

        }
    }

    /// <summary>
    /// Generate a specific planet
    /// </summary>
    /// <param name="planetPrefab"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    GameObject GeneratePlanet(GameObject planetPrefab, int i)
    {
        GameObject obj = Instantiate(planetPrefab, transform.position, transform.rotation) as GameObject;
        obj.transform.SetParent(transform);
        return obj;
    }

	// Use this for initialization
	void Start () {
        UpdatePlanetFocus(); 
	}

    /// <summary>
    /// Update the current camera's focus on a planet 
    /// </summary>
    void UpdatePlanetFocus()
    {
        levels[index].transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y, 0);
        if (index == 0)
        {
            levels[index + 1].gameObject.SetActive(true);
            levels[levels.Length - 1].gameObject.SetActive(true);
            levels[index + 1].transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y + 3, 0);
            levels[levels.Length - 1].transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y - 3, 0);           
        }
        else if (index == levels.Length - 1)
        {
            levels[0].gameObject.SetActive(true);
            levels[index - 1].gameObject.SetActive(true);
            levels[0].transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y + 3, 0);
            levels[index - 1].transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y - 3, 0);
        } else
        {
            levels[index + 1].gameObject.SetActive(true);
            levels[index - 1].gameObject.SetActive(true);
            levels[index + 1].transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y + 3, 0);
            levels[index - 1].transform.position = new Vector3(focus.transform.position.x, focus.transform.position.y - 3, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            int nextIndex = index - 1;
            Debug.Log(nextIndex);
            if(nextIndex == -1)
            {
                Debug.Log("Cycled down");
                nextIndex = levels.Length - 1; 
            }
            index = nextIndex;
            Debug.Log(index);
            UpdatePlanetFocus(); 
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            int nextIndex = index + 1;
            Debug.Log(nextIndex);
            if(nextIndex == levels.Length)
            {
                Debug.Log("Cycled up");
                nextIndex = 0; 
            }
            index = nextIndex;
            Debug.Log(index);
            UpdatePlanetFocus();
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            levels[index].InstantiateGame();
        }
	}

    /// <summary>
    /// Deactivates all the planets
    /// </summary>
    public void DeactivatePlanets()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].gameObject.SetActive(false);
        }
        focus.position = new Vector3(4.45f, 0.7f, -11f);
        Camera camera = focus.gameObject.GetComponent<Camera>();
        camera.orthographicSize = 10.89541f;
    }
}
