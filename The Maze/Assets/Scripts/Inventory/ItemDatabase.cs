using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour 
{
	public List<Item> items = new List<Item>();

	void Start ()
	{
		items.Add (new Item ("Lighter", 1, "Whit this lighter you can flame on torches.", Item.ItemType.NotUsable));
		items.Add (new Item ("MapL1N1", 2, "Part of the map for the maze.", Item.ItemType.Usable));
		items.Add (new Item ("MapL1N2", 3, "Part of the map for the maze", Item.ItemType.Usable));
		items.Add (new Item ("Markers", 4, "Mark your way through the maze.", Item.ItemType.Usable));
		items.Add (new Item ("Flashlight", 5, "You can light your way through the darkness.", Item.ItemType.Usable));
	}
}
