using UnityEngine;
using System.Collections;

public class AimerCenter : MonoBehaviour {

    /// <summary>
    /// Contains information about the enemy that was hit
    /// </summary>
    RaycastHit enemHit;

    /// <summary>
    /// Determines whether or not an enemy was actually hit
    /// </summary>
    private bool isHit;

    /// <summary>
    /// Ray positions for the raycasts
    /// </summary>
    Vector3 rayPos1;
    Vector3 rayPos2;
    Vector3 rayPos3;
    Vector3 rayPos4; 


    /// <summary>
    /// Enemy that was hit
    /// </summary>
    Enemy target = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

    /// <summary>
    /// Determine if the collider has hit something
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Is Hit!");
        isHit = true;
        target = collider.gameObject.GetComponent<Enemy>(); 
    }

    /// <summary>
    /// Determine if the something has left the collider's range
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerExit2D(Collider2D collider)
    {
        isHit = false; 
    }
    
    /// <summary>
    /// Returns if the enemy was hit
    /// </summary>
    /// <returns></returns>
    public bool OnHit()
    {
        return isHit; 
    }

    /// <summary>
    /// Return the enemy that was actually hit
    /// </summary>
    /// <returns></returns>
    public Enemy enemyHit()
    {

        return target;
    }

    /// <summary>
    /// Set the x position of this gameObject
    /// </summary>
    /// <param name="x"></param>
    public void setX(float x)
    {
        transform.position = new Vector3(x, transform.position.y, -0.1f);
    }

    /// <summary>
    /// Set the y position of this gameObject
    /// </summary>
    /// <param name="y"></param>
    public void setY(float y)
    {
        transform.position = new Vector3(transform.position.x, y, -0.1f);
    }
}
