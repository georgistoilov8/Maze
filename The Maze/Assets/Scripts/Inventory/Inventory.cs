using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour 
{
	public int slotsPerX, slotsPerY;
	public GUISkin skin;
	private GUIStyle guiStyle = new GUIStyle();
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	private List<Item> mapSlots = new List<Item> ();
	private bool showInventory;
	private bool showMaps;
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
	public float slotOffsetByX;
	public float slotOffsetByY;

	private bool isLighterPicked;

	private GameObject player;
	public GameObject lighter;
	public GameObject fLight; 							//flashlight light
	public GameObject flashlight;
	public GameObject MapL1N1;
	public GameObject MapL1N2;
	public GameObject marker;
	public float percentageByX;
	public float percentageByY;

	private bool shouldRemoveItemFromInventory;
	private float markersCount;
	// Use this for initialization
	void Start () 
	{
		isLighterPicked = false;
		showInventory = false;
		shouldRemoveItemFromInventory = true;
		showMaps = false;
		for(int count = 0; count < (slotsPerX * slotsPerY); count++)
		{
			slots.Add (new Item ());
			inventory.Add (new Item ());
		}

		for (int count = 0; count < 6; count++) 
		{
			mapSlots.Add (new Item ());
		}
		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
		player = GameObject.FindGameObjectWithTag ("Player");
		guiStyle.fontSize = 25;
		guiStyle.normal.textColor = Color.red;
		markersCount = 10;
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
		}else if (Input.GetButtonDown ("OpenMap")) 
		{
			mapSlots [0] = new Item ();
			showInventory = !showInventory;
			showMaps = !showMaps;
			FirstPersonController.isInventoryOpen = !FirstPersonController.isInventoryOpen;
			cursorState = CursorLockMode.None;
			Cursor.lockState = cursorState;
			SetCursorState ();

		}
	}

	private void SetCursorState ()
	{
		Cursor.lockState = cursorState;
		Cursor.visible = (CursorLockMode.Locked != cursorState);
	}

	void OnGUI ()
	{
		tooltip = "";
		GUI.skin = skin;
		if (showMaps) 
		{
			ShowMap ();
		}
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

	private void DrawInventory ()
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
					if (slots [count].itemName.Equals ("Markers")) 
					{
						GUI.Label (new Rect ((Screen.width - (x * slotOffsetByX) + 25), (Screen.height - (y * slotOffsetByY) + 25), 30, 30 ), markersCount.ToString(), guiStyle);
					}
					if (slotBox.Contains (e.mousePosition)) {
						showTooltip = true;
						tooltip = CreateTooltip (slots [count]);
						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) { //get item to be dragged
							draggingItem = true;
							previousIndex = count;
							draggedItem = slots [count];
							inventory [count] = new Item ();
						}
						if (e.type == EventType.mouseUp && draggingItem) { //change dragged item with the item under the mouse
							inventory [previousIndex] = inventory [count];
							inventory [count] = draggedItem;
							draggingItem = false;
							draggedItem = null;
							shouldRemoveItemFromInventory = false;
						}
						if (e.button == 1 && e.type == EventType.mouseDown && !draggingItem) 
						{
							if (slots [count].itemName.Equals ("Markers")) 
							{
								markersCount--;
								Vector3 cameraDirection = Camera.main.transform.forward;
								Vector3 newClonePosition = new Vector3 (player.transform.position.x + cameraDirection.x, player.transform.position.y + cameraDirection.y, player.transform.position.z + cameraDirection.z);
								Instantiate (marker, newClonePosition , Quaternion.identity);
								if (markersCount == 0) 
								{
									slots [count] = new Item ();
								}
							}

							if (slots [count].itemName.Contains ("Map")) 
							{
								mapSlots [0] = slots [count];
							}

						}
					} 
				} else {
					if (slotBox.Contains (e.mousePosition)) 
					{
						if (e.type == EventType.mouseUp && draggingItem) {
							inventory [count] = draggedItem;
							draggingItem = false;
							draggedItem = null;
							shouldRemoveItemFromInventory = false;
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

		if (draggingItem && e.type == EventType.MouseUp && shouldRemoveItemFromInventory) 
		{
			Vector3 cameraDirection = Camera.main.transform.forward;
			Vector3 newClonePosition = new Vector3 (player.transform.position.x + cameraDirection.x, player.transform.position.y + cameraDirection.y, player.transform.position.z + cameraDirection.z); //to be placed in front of the player
			Debug.Log ("Should destroy.");
			if (draggedItem.itemName.Equals ("Lighter")) 
			{
				
				GameObject l = Instantiate (lighter, newClonePosition , Quaternion.identity);
				l.gameObject.tag = "Lighter";
				SetIsLighterPicked (false);
			}

			if (draggedItem.itemName.Equals ("MapL1N1")) 
			{
				GameObject map = Instantiate (MapL1N1, newClonePosition, Quaternion.identity);
				map.gameObject.tag = "Map";
			}

			if (draggedItem.itemName.Equals ("MapL1N2")) 
			{
				GameObject map =Instantiate (MapL1N2, newClonePosition, Quaternion.identity);
				map.gameObject.tag = "Map";
			}

			if (draggedItem.itemName.Equals ("Flashlight")) 
			{
				GameObject f = Instantiate (flashlight, newClonePosition, Quaternion.identity);
				f.gameObject.tag = "Flashlight";
				fLight.GetComponent<Light> ().enabled = false;
			}

			draggedItem = null;
			draggingItem = false;
			inventory [previousIndex] = new Item ();
		}

	}

	private void ShowMap()
	{
		Event e = Event.current;
		int count = 0;
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		float slotMapWidth = (screenWidth * percentageByX) / 100;
		float slotMapHeight = (screenHeight * percentageByY) / 100;
		float offsetX = (screenWidth / 2) - (slotMapWidth / 2);
		float offsetY = (screenHeight / 2) - (slotMapHeight / 2);
		Rect slotBox = new Rect (offsetX, offsetY, slotMapWidth, slotMapHeight);
		GUI.Box (slotBox, "", skin.GetStyle("Map Slot Background"));
		GUI.DrawTexture (slotBox, mapSlots[0].itemIcon);
	}

	private string CreateTooltip (Item item)
	{
		tooltip = "<color=#672222>" + item.itemName + "</color>\n\n" + "<color=#ffffff>" + item.itemDescription + "</color>";
		return tooltip;
	}

	public void RemoveItem (int id)
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

	public void AddItem (int id)
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

	public bool InventoryContains (int id)
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

	public void SetIsLighterPicked(bool state)
	{
		isLighterPicked = state;
	}

	public bool GetIsLighterPicked()
	{
		return isLighterPicked;
	}

	public void increaseMarkersCount()
	{
		markersCount++;
	}
}
