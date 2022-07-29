using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour 
{
	public ItemDatabase idb;
	public GameObject g;
	
	public int respawnTime = 50;
	int respawnCounter;
	int n = 0;
	// Use this for initialization
	void Start () 
	{
		respawnCounter = respawnTime;
		Spawn();
	}
	
	void Spawn()
	{
		n = Random.Range(0,idb.ItemDB.Count);
		g = Instantiate(idb.ItemDB[n].Drop, transform.position, transform.rotation) as GameObject;
		g.GetComponent<PickupItem>().ID = n;
	}
	
	public void Check()
	{
		if(respawnCounter <= 0)
		{
			if(g == null)
			{
				Spawn();
				respawnCounter = respawnTime;
			}
		}else
		{
			respawnCounter--;
		}
	}
	
}
