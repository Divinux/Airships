using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour 
{

	public string reticleName = "Default";
	public Texture2D[] reticleTexture;
	public string[] tags = {"Default Reticle", "Enemy", "Pickup", "DoorEnter"};
	//16-34 for the circle reticle
	//16-50 for the crosshair
	public float minSize = 32;
	public float maxSize = 96;
	public float maxDistance = 100;
	public bool  visible = true;

	private RaycastHit hit;
	private Rect rectangle;
	private Texture2D currentTexture;
	private int screenHeight;
	private int screenWidth;
	float lerpsize = 0f;
	public float lerpspeed = 0.1f;

	float distCache;
	public float distPickup = 10f;
	//note how long a new object is seen
	int timer = 0;
	public string  GetName ()
	{
		return reticleName;
	}

	void  Start ()
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		//Sets draw location for reticle
		rectangle = new Rect(screenWidth/2,screenHeight/2,minSize,minSize);
	}
	
//
	void  LateUpdate()
	{
		
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		//Checks center of screen for raycast.
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenWidth/2,screenHeight/2)); 
		//Runs check to see if there is a raycast collision within our maximum distance.
		if (Physics.Raycast(ray,out hit,  maxDistance)) 
		{
			distCache = hit.distance;
			Reset();
			int i= 0;
			int b = 0;
			for(i = 0; i < tags.Length; i++)
			{
				//Dynamically checks for added tags and sets the reticle if it finds that tag collided
				if(hit.collider.tag == tags[i]) 	
				{
					timer++;
					
					currentTexture = reticleTexture[i];	
					b = i;
					
					
					
				}
			}
			lerpsize = Map(distCache, 0f, maxDistance, maxSize, minSize);
			rectangle.height = Mathf.Lerp(rectangle.height, lerpsize, lerpspeed);
			//this is a maping system to resize values to a different size. 5 out 10 would be resized to 50 out of 100 if it were map(5, 0, 10, 0, 100);
			//Without the above line, our reticle wouldn't resize hardly ever, and it also only scales the size between max and min value. Max is first otherwise the reticle would get larger with the further distance.
			rectangle.width = rectangle.height;		
			if(rectangle.width < minSize)
			{
				//if distance draws it smaller then our minimum size, then resize it to minimum
				rectangle.width = rectangle.height = minSize; 
			}
			if(rectangle.width > maxSize)
			{
				//if distance draws it larger then our maximum size, then resize it to maximum
				rectangle.width = rectangle.height = maxSize; 
			}
			//code for picking up items and entering doors
			//if within distance
			if(distCache < distPickup)
			{
				//check for input
			if(Input.GetButtonUp("Use"))
			{
				//Debug.Log(hit.collider.tag);
				//check for tag 
				if(hit.collider.tag == "Pickup" || hit.collider.tag == "DoorEnter")
				{
					hit.collider.SendMessageUpwards("Trigger", SendMessageOptions.DontRequireReceiver);
				}
			}
			}
			else
			{
				//check if interactable, but out of range
				if(b == 2 || b == 3)
				{
					Reset();
				}
			}
		}
		else
		{
			//if no collision within the maximum distance is found then we simply draw the smallest size.
			rectangle.width = rectangle.height = minSize; 
			currentTexture = reticleTexture[0];
		}
		//These two lines change the position of the reticle to be based on the center of the texture rather then the top left.
		//They are always needed, so it's outside of the if statements above.
		rectangle.x = screenWidth/2 - rectangle.width/2; 
		rectangle.y = screenHeight/2 - rectangle.height/2; 
		
	} 
void Reset()
{
			//Resets reticle in case there is no collision with a tagged object
	currentTexture = reticleTexture[0]; 
}
	void  OnGUI ()
	{
		if(visible)
		{
			GUI.DrawTexture(rectangle, currentTexture);
		}
	}

	float  Map ( float value ,   float fromLow ,   float fromHigh ,   float toLow ,   float toHigh  ){
		return toLow + (toHigh - toLow) * ((value - fromLow) / (fromHigh - fromLow));
	}
}