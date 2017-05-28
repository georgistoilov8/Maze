using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnFlashlight : MonoBehaviour {

	Inventory inventory;	

	void Start () 
	{
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory>();
	}

	void Update () 
	{
		if (Input.GetButtonDown ("TurnOnFlashlight")) 
		{
			if (this.gameObject.GetComponent<Light> ().enabled.Equals (true)) 
			{
				this.gameObject.GetComponent<Light> ().enabled = false;
			}else if (inventory.InventoryContains (5)) 
			{
				this.gameObject.GetComponent<Light> ().enabled = true;
			}
		}
	}
}
