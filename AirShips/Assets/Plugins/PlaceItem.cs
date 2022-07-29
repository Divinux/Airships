using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
	public GameObject prefab;
	public int maxDistance = 1000;
	private int screenHeight;
	private int screenWidth;
	private RaycastHit hit;
	float distCache;
	public GameObject player;
	public Player ps;
    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindWithTag("Player");
		ps = player.GetComponent<Player>();
        screenHeight = Screen.height;
		screenWidth = Screen.width;
		//Sets draw location for reticle
		//rectangle = new Rect(screenWidth/2,screenHeight/2,minSize,minSize);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        

		if(Input.GetButtonUp("Use"))
			{
				screenWidth = Screen.width;
		screenHeight = Screen.height;
		//Checks center of screen for raycast.
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenWidth/2,screenHeight/2));
				//Runs check to see if there is a raycast collision within our maximum distance.
		if (Physics.Raycast(ray,out hit,  maxDistance)) 
		{
			distCache = hit.distance;
			
				//Dynamically checks for added tags and sets the reticle if it finds that tag collided
				if(hit.collider.tag == "Socket") 	
				{
					//Instantiate the buildable 
					Instantiate(prefab, hit.transform.position, hit.transform.rotation);
					//Destroy the player held item
					Destroy(gameObject);
					//Use up one inventory of the item
					ps.Use();
				}
		}
				
				
			}

		
		
    }
}
