using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float targetHeight = 1;
	public float floatSpeed = 0.6f;
    void Update()
    {
		if (Input.GetKey(KeyCode.F))
        {
			transform.Translate(Vector3.forward * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.E))
        {
			transform.Translate(Vector3.up * Time.deltaTime * floatSpeed, Space.World);
			}
        // Move the object forward along its z axis 1 unit/second.
        //transform.Translate(Vector3.forward * Time.deltaTime);
		Physics.SyncTransforms();
		
		
		
		
		
	}
	void LateUpdate()
	{
	
		if(transform.position.y <= targetHeight)
		{
			// Move the object upward in world space 1 unit/second.
			transform.Translate(Vector3.up * Time.deltaTime, Space.World);
			
		}
		
		
	}
}