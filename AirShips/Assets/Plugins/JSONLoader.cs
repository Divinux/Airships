// Add System.IO to work with files!
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONLoader : MonoBehaviour
{
		public GameObject player;
	public Player ps;
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
      gameData.playerinventory  = new List<Item>(ps.inventory);
    }
	[ContextMenu ("pushplayerdatatogame")]
	public void pushData()
    {
        ps.inventory  = new List<Item>(gameData.playerinventory);
		gameData.playerinventory  = new List<Item>();
    }
}