using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayResponder : MonoBehaviour
{
    //trigger called by raycaster to pick up item
	public void Trigger() 
	{
		Debug.Log("Trigger received");
	}
}
