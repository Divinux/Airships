using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class ItemDatabase : MonoBehaviour 
{
	public List<Item> ItemDB = new List<Item>();

	[System.Serializable]
	public struct Item 
	{
		//name of the object
		public string name;
		//description of the object
		public string Desc;
		//the gameobject that you would pick up
		public GameObject Drop;
		//the object you would hold in your hand
		public GameObject Carry;
		//base price of the object
		public int Price;
	}	
}
