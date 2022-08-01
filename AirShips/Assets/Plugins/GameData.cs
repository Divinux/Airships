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
	
	public int Exp;
	
	public int ExpToLvlUp;
	
	public int Lvl;
	
	//copy of the player inventory
	public List<Item> playerinventory = new List<Item>();
	public List<ShipPart> ship = new List<ShipPart>();
}
[System.Serializable]
public class ShipPart
{
	public string Name;
	public Vector3 Position;
	public Quaternion Rotation;
	public int ID = -1;
	public vColor Color;
}
