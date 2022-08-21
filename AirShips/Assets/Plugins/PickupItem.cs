using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour 
{
	
	public int ID;
	
	public Player pls;
	
	//trigger called by raycaster to pick up item
	public void Trigger() 
	{
		//if(Input.GetButtonUp("Use"))
		//{
		//Debug.Log("triggered");
		//find the player object
		GameObject p = GameObject.FindWithTag("Player");
		pls = p.GetComponent<Player>();
		//Debug.Log("picked up item");
		//add item to inventroy
		pls.AddItem(ID);
		
		//check if player is parented to the object being picked up
		//(is the player standing on it)
		Player pl = GetComponentInChildren<Player>();
		//if so, unparent the player before deleting the object
        if (pl != null)
        {
            p.transform.parent = null;
		}
		
		//
		//temp store for parent object search
		Transform targetparent = null;
		
		//look through all children and find the parts holder
		foreach (Transform child  in transform)
		{
			if(child.tag == "Partsholder")
			{
				targetparent = child;
			}
		}
		//if a parts holder was found, parent all children to the world parts holder
		if(targetparent!=null)
		{
			foreach (Transform child  in targetparent)
			{
				GameObject wp = GameObject.FindWithTag("WorldPartsHolder");
				child.parent = wp.transform;
			}
		}
		
		
		transform.parent = null;
		//finally destroy the world object
		Destroy(gameObject);
		
		//}
		
	}
}
