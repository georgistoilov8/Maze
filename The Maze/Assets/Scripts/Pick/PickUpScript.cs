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
