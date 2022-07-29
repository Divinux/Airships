using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add System.IO to work with files!
using System.IO;
		

[System.Serializable]
public class GameData
{
	public int lives;
	
	public int highScore;
	
	//copy of the player inventory
	public List<Item> playerinventory = new List<Item>();
}
