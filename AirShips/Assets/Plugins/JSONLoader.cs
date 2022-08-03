// Add System.IO to work with files!
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONLoader : MonoBehaviour
{
	public GameObject player;
	public Player ps;
	public GameObject foundClock;
	
	public ItemDatabase idb;
    // Create a field for the save file.
    string saveFile;
	
    // Create a GameData field.
    public GameData gameData = new GameData();
	public GameObject worldparts;
	public Transform partToColor;
    void Awake()
    {
		worldparts = GameObject.FindWithTag("WorldPartsHolder");
		idb = GetComponent<ItemDatabase>();
        // Update the path once the persistent path exists.
        saveFile = Application.dataPath  + "/gamedata.json";
		
        //saveFile =  "gamedata.json";
		player = GameObject.FindWithTag("Player");
		ps = player.GetComponent<Player>();
	}
	[ContextMenu ("readfile")]
    public void readFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
			
            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            gameData = JsonUtility.FromJson<GameData>(fileContents);
		}
	}
	[ContextMenu ("writefile")]
    public void writeFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(gameData);
		
        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
	}
	[ContextMenu ("getplayerdatafromgame")]
	public void getData()
    {
		foundClock = GameObject.Find("ClockPlace(Clone)");
		
		if(foundClock!=null)
		{
			
			gameData.ship = new List<ShipPart>();
			Traverse(foundClock);
		}
		gameData.playerinventory  = new List<Item>(ps.inventory);
	}
	[ContextMenu ("pushplayerdatatogame")]
	public void pushData()
    {
		
		//load inventory
        ps.inventory  = new List<Item>(gameData.playerinventory);
		gameData.playerinventory  = new List<Item>();
		//reference to the clock
		GameObject goClock = worldparts;
		//reference to any other object being instantiated
		GameObject goAll = null;
		
		//load ship
		foreach(ShipPart s in gameData.ship)
		{
			if(s.ID == 0)
			{
				goClock = Instantiate(idb.ItemDB[0].Place, s.Position, s.Rotation);					
				goClock.transform.parent = null;					
			}
			else if(s.ID != -1)
			{
				goAll = Instantiate(idb.ItemDB[s.ID].Place, s.Position, s.Rotation);
				Debug.Log("Instantiated!" + s.Name);
				if(foundClock == null)
				{
					foundClock = GameObject.Find("ClockPlace(Clone)");
				}
				Transform p = getChildGameObject(foundClock, "Shipparts");
				goAll.transform.parent = p.transform;	
				
			}
			if(goAll != null)
			{
				Debug.Log("," + s.Name);
				if(s.Name != "")
				{
					//color the new part
					Debug.Log("coloring part: " + s.Name);
					
					partToColor = getChildGameObject(goAll, s.Name);
					//partToColor = goAll.transform;
					
					var cRenderer = partToColor.gameObject.GetComponent<Renderer>();
					// Call SetColor using the shader property name "_Color" and setting the color to red
					cRenderer.material.SetColor("_Color", s.Color.cColor);
				}
			}
		}
		/*/color ship
			foreach(ShipPart s in gameData.ship)
			{			
			if(foundClock == null)
			{
			foundClock = GameObject.Find("ClockPlace(Clone)");
			}
			if(s.ID == -1 && s.Name != null)
			{
			Debug.Log("coloring part: " + s.Name);
			
			
			partToColor = getChildGameObject(foundClock, s.Name);
			
			
			var cRenderer = partToColor.gameObject.GetComponent<Renderer>();
			// Call SetColor using the shader property name "_Color" and setting the color to red
			cRenderer.material.SetColor("_Color", s.Color.cColor);
			
			}
			
		}*/
	}
	
	public Transform getChildGameObject(GameObject fromGameObject, string withName) {
		//Author: Isaac Dart, June-13.
		Transform[] ts = fromGameObject.GetComponentsInChildren<Transform>();
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t;
		return null;
	}
	
	void Traverse(GameObject obj)
	{
		Debug.Log (obj.name);
		ShipPart sp = new ShipPart();
		
		PickupItem pu = null;
		pu = obj.GetComponent<PickupItem>();
		if(pu)
		{	
			sp.ID = pu.ID;
		}
		
		// Get the Renderer component from the new cube
		var cRenderer = obj.GetComponent<SetRandomColor>();
		if(cRenderer)
		{
			// Call SetColor using the shader property name "_Color" and setting the color to red
			sp.Color =cRenderer.vCol;
			sp.Name = obj.name;
		}
		sp.Position = obj.transform.position;
		sp.Rotation = obj.transform.rotation;
		Debug.Log("the Name is" + sp.Name + sp.ID);
		if(sp.Name == null && sp.ID == -1)
		{
			Debug.Log("Can be ignored");
		}
		else
		{
			
			gameData.ship.Add(sp);
		}
		
		foreach (Transform child in obj.transform) {
			Traverse (child.gameObject);
		}
		
	}
	
	
	
	
}