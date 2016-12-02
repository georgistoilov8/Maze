using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item
{
	public string itemName;
	public int itemID;
	public string itemDescription;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemSpeed;
	public ItemType itemType;

	public enum ItemType
	{
		Usable,
		NotUsable
	}

	public Item (string name, int id, string description, ItemType type)
	{
		itemName = name;
		itemID = id;
		itemDescription = description;
		itemIcon = Resources.Load<Texture2D> ("Item Icons/" + name);
		itemType = type;
	}

	public Item(){}
}
