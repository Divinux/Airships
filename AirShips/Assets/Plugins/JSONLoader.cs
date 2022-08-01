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
	
    // Create a field for the save file.
    string saveFile;
	
    // Create a GameData field.
    public GameData gameData = new GameData();
	
    void Awake()
    {
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
        ps.inventory  = new List<Item>(gameData.playerinventory);
		gameData.playerinventory  = new List<Item>();
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