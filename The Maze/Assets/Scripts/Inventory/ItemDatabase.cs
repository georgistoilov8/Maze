using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour 
{
	public List<Item> items = new List<Item>();

	void Start ()
	{
		items.Add (new Item ("Flashlight", 0, "You can light your way through the darkness.", Item.ItemType.Usable));
		items.Add (new Item ("Lighter", 1, "Whit this lighter you can flame on torches.", Item.ItemType.NotUsable));
		items.Add (new Item ("MapL1N1", 2, "Map of the maze.", Item.ItemType.Usable));
	}
}
