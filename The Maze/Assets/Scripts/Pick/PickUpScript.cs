using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

	public GUISkin skin;
	public float boxX;
	public float boxY;
	public float boxWidth;
	public float boxHeight;

	private bool canPick;
	private Rect box;

	Inventory inventory;
	// Use this for initialization
	void Start () 
	{
		canPick = false;
		box = new Rect ((Screen.width / 2) + boxX, (Screen.height / 2) + boxY, boxWidth, boxHeight);
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Pick")) 
		{
			// check if player can pick object
			// if yes add it to the inventory
			// destroy real object
			if (canPick)
			{
				if (this.tag.Equals ("Lighter")) 
				{
					inventory.AddItem (1);
					inventory.SetIsLighterPicked (true);
				} else if (this.tag.Equals ("Map")) 
				{
					if (this.name.Contains ("MapL1N1")) 
					{
						inventory.AddItem (2);
					} else if (this.name.Contains ("MapL1N2")) 
					{
						inventory.AddItem (3);
					} else if (this.name.Contains ("MapL2N1")) 
					{
						inventory.AddItem (6);
					} else if (this.name.Contains ("MapL2N2")) 
					{
						inventory.AddItem (7);
					} else if (this.name.Contains ("MapL3N1")) 
					{
						inventory.AddItem (8);
					} else if (this.name.Contains ("MapL3N2")) 
					{
						inventory.AddItem (9);
					} else if (this.name.Contains ("MapL3N3")) 
					{
						inventory.AddItem (10);
					} else if (this.name.Contains ("MapL3N4")) 
					{
						inventory.AddItem (11);
					}
				} else if (this.name.Contains ("Markers")) 
				{
					inventory.AddItem (4);
				} else if (this.name.Contains ("Marker") && !this.name.Contains ("Markers")) 
				{
					inventory.increaseMarkersCount ();
				} else if (this.tag.Equals ("Flashlight")) 
				{
					inventory.AddItem (5);
				}
				Destroy (this.gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag.Equals("Player"))
			canPick = !canPick;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag.Equals("Player"))
			canPick = !canPick;
	}

	void OnGUI()
	{
		if (canPick) 
		{
			GUI.Box (box, "", skin.GetStyle("ButtonPick"));
		} 
	}
}
