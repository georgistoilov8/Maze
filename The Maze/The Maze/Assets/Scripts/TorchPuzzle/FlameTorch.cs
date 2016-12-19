using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTorch : MonoBehaviour {

	private bool canClick;
	private GameObject torch;

	public GUISkin skin;
	public float boxX;
	public float boxY;
	public float boxWidth;
	public float boxHeight;

	private int count;
	private Rect box;
	// Use this for initialization
	void Start () 
	{
		canClick = false;
		count = 0;
		box = new Rect ((Screen.width / 2) + boxX, (Screen.height / 2) + boxY, boxWidth, boxHeight);
		Debug.Log (torch);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canClick) 
		{
			if (Input.GetButtonDown ("FlameTorch"))
			{
				
				count++;
				Debug.Log (count);
				Debug.Log (torch);
				if(count % 2 != 0)
					torch.GetComponent<Torchelight> ().SetTurnOn();
				if (count % 2 == 0)
					torch.GetComponent<Torchelight> ().SetTurnOff();
			}
		}

	}

	void OnTriggerEnter () 
	{
		canClick = !canClick;
		torch = this.transform.GetChild (0).gameObject;
	}

	void OnTriggerExit ()
	{
		canClick = !canClick;
	}

	void OnGUI()
	{
		if (canClick) 
		{
			GUI.Box (box, "", skin.GetStyle("ButtonFlame"));
		} 
	}

}

