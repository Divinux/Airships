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
		
		GameObject p = GameObject.FindWithTag("Player");
		pls = p.GetComponent<Player>();
		//Debug.Log("picked up item");
		pls.AddItem(ID);
		Destroy(gameObject);
		
		//}
		
	}
}
