using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//player inventory
public class Player : MonoBehaviour 
{

	
	//player inventory
	public List<Item> inventory = new List<Item>();
	//reference to item database
	public ItemDatabase idb;
	//inventory help variables
	Item temp;
	//current selected inventoryslot
	public int vSelected = -1;
	//the instance of current held item
	public GameObject HeldItem;
	//position of held object
	public GameObject HandPos;
	
	[System.Serializable]
	public class Item
	{
		public int ID = -1;
		public int amount = 0;
	}
	//[ContextMenu ("additem")]
	//adds an item with db id to inventory
	public void AddItem(int adb)
	{
		//just add if inv is empty
		if(inventory.Count == 0)
		{
			AddNew(adb);
		}
		else
		{
			//check if item with same id is already in inventory
			temp = null;
			foreach(Item i in inventory)
			{
				if(i.ID == adb)
				{
					temp = i;
				}
			}
			if(temp == null)
			{
				//if not, add as new item
				AddNew(adb);
			}
			else
			{
				//else add to old stack
				AddStack(temp);
			}
		}
	}
	//adds a new item to inventory
	void AddNew(int ad)
	{
		temp = new Item();
		temp.ID = ad;
		temp.amount = 1;
		inventory.Add(temp);
	}
	//adds to the stack of an item in invetory
	void AddStack(Item it)
	{
		it.amount += 1;
	}
	
	void Update()
	{
		if(Input.GetKeyUp("tab"))
		{
			vSelected = -1;
			Equip();
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			if(vSelected < inventory.Count-1)
			{
				vSelected++;
				
			}
			else
			{
				vSelected = 0;
			}
			Equip();
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			if(vSelected > 0)
			{
				vSelected--;
				
			}
			else
			{
				vSelected = inventory.Count-1;
			}
			Equip();
		}
	}
	
	//display current item
	void Equip()
	{
		if(vSelected == -1)
		{
			if(HeldItem != null)
			{
				Destroy(HeldItem);
			}
		}
		else if(inventory.Count > 0)
		{
			if(HeldItem != null)
			{
				Destroy(HeldItem);
			}
			HeldItem = Instantiate(idb.ItemDB[inventory[vSelected].ID].Carry, HandPos.transform.position, HandPos.transform.rotation) as GameObject;
			HeldItem.transform.parent = HandPos.transform;
		}
	}
}
