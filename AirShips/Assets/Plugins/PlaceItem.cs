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
					GameObject newpart = Instantiate(prefab, hit.transform.position, hit.transform.rotation);
					//Destroy the player held item
					string oldtag = gameObject.tag;
					Destroy(gameObject);
					//Use up one inventory of the item
					ps.Use();
					
					
					//find the root of the given part
					Debug.Log(FindParentWithTag(hit.transform.gameObject, "Root"));
					GameObject shippart = FindParentWithTag(hit.transform.gameObject, "Root");
					
					if(oldtag == "Clockcarry")
					{
					//temp store for parent object search
					Transform targetparent = null;
					foreach (Transform child  in newpart.transform)
					{
						Debug.Log(child.tag);
						if(child.tag == "Partsholder")
						{
							
							targetparent = child;
						}
					}
					if(targetparent!=null)
					{
						//parent that root to the placed item
						shippart.transform.parent = targetparent;
					}
					else
					{
						shippart.transform.parent = newpart.transform;
					}
					}
					else
					{
						newpart.transform.parent = hit.transform;
						}
				}
			}
			
			
		}
		
		
		
	}
	
	
	//helper classes
	
	
	public GameObject FindParentWithTag(GameObject childObject, string tag)
	{
		Transform t = childObject.transform;
		while (t.parent != null)
		{
			if (t.parent.tag == tag)
			{
				return t.parent.gameObject;
			}
			t = t.parent.transform;
		}
		return null; // Could not find a parent with given tag.
	}
}
