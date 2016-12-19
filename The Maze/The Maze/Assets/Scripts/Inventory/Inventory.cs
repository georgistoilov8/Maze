using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour 
{
	public int slotsPerX, slotsPerY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	private bool showInventory;
	private ItemDatabase database;

	private bool showTooltip;
	private string tooltip;

	private CursorLockMode cursorState;

	private bool draggingItem;
	private Item draggedItem;
	private int previousIndex;

	public int tooltipHeight;
	public int tooltipWidth;
	public int draggingItemHeight;
	public int draggingItemWidth;
	public int slotHeight;
	public int slotWidth;
	public int slotOffsetByX;
	public int slotOffsetByY;


	// Use this for initialization
	void Start () 
	{
		for(int count = 0; count < (slotsPerX * slotsPerY); count++)
		{
			slots.Add (new Item ());
			inventory.Add (new Item ());
		}
		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
		inventory[0] = database.items[0];
		inventory[1] = database.items[1];
		AddItem (0);
		print (InventoryContains(1));
		RemoveItem (0);
	}

	void Update ()
	{
		if(Input.GetButtonDown("Inventory"))
		{
			showInventory = !showInventory;
			FirstPersonController.isInventoryOpen = !FirstPersonController.isInventoryOpen;
			cursorState = CursorLockMode.None;
			Cursor.lockState = cursorState;
			SetCursorState ();
		}
	}

	void SetCursorState ()
	{
		Cursor.lockState = cursorState;
		Cursor.visible = (CursorLockMode.Locked != cursorState);
	}

	void OnGUI ()
	{
		tooltip = "";
		GUI.skin = skin;
		if (showInventory) 
		{
			DrawInventory ();
			if (showTooltip) 
				GUI.Box (new Rect (Event.current.mousePosition.x - 15f - tooltipWidth, Event.current.mousePosition.y - tooltipHeight, tooltipWidth, tooltipHeight), tooltip, skin.GetStyle("Tooltip"));
		}
		if(draggingItem)
		{
			GUI.DrawTexture (new Rect (Event.current.mousePosition.x - 2f, Event.current.mousePosition.y - 2f, draggingItemWidth , draggingItemHeight), draggedItem.itemIcon);	
		}


	}

	void DrawInventory ()
	{
		Event e = Event.current;
		int count = 0;
		for (int y = 1; y <= slotsPerY; y++)
		{
			for (int x = 1; x <= slotsPerX; x++) 
			{
				Rect slotBox = new Rect ((Screen.width - (x * slotOffsetByX)), (Screen.height - (y * slotOffsetByY)), slotHeight, slotWidth);
				GUI.Box (slotBox, "", skin.GetStyle("Slot Background"));
				slots [count] = inventory [count];
				if(slots[count].itemName != null)
				{
					GUI.DrawTexture (slotBox, slots[count].itemIcon);
					if (slotBox.Contains (e.mousePosition)) {
						showTooltip = true;
						tooltip = CreateTooltip (slots [count]);
						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) {
							draggingItem = true;
							previousIndex = count;
							draggedItem = slots [count];
							inventory [count] = new Item ();
						}
						if (e.type == EventType.mouseUp && draggingItem) {
							inventory [previousIndex] = inventory [count];
							inventory [count] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					} 
				} else {
					if (slotBox.Contains(e.mousePosition))
					{
						if (e.type == EventType.mouseUp && draggingItem) {
							inventory [count] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				if (tooltip == "") 
				{
					showTooltip = false;
				}
				count++;
			}
		}
	}

	string CreateTooltip (Item item)
	{
		tooltip = "<color=#672222>" + item.itemName + "</color>\n\n" + "<color=#ffffff>" + item.itemDescription + "</color>";
		return tooltip;
	}

	void RemoveItem (int id)
	{
		for (int count = 0; count < inventory.Count; count++) 
		{
			if (inventory [count].itemID == id) 
			{
				inventory [count] = new Item ();
				break;
			}
		}
	}

	void AddItem (int id)
	{
		for (int count = 0; count < inventory.Count; count++) 
		{
			if (inventory [count].itemName == null) 
			{
				for(int i = 0; i < database.items.Count; i++)
				{
					if(database.items[i].itemID == id)
					{
						inventory [count] = database.items [i]; 
					}
				}
				break;
			}
		}
	}

	bool InventoryContains (int id)
	{
		bool result = false;
		for (int count = 0; count < inventory.Count; count++) 
		{
			result = inventory [count].itemID == id;
			if (result) 
				break;
		}
		return result;
	}
}
