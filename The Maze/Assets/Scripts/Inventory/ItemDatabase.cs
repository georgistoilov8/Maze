using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour 
{
	public List<Item> items = new List<Item>();

	void Start ()
	{
		items.Add (new Item ("Map", 0, "Map of the maze", Item.ItemType.Usable));
		items.Add (new Item ("Map 3", 1, "Map of the maze", Item.ItemType.Usable));
	}
}
