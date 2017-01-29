using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPuzzle : MonoBehaviour {

	private List<GameObject> torches = new List<GameObject>();

	public static bool updatePuzzle;
	public GameObject puzzle;

	private float countBlueLights;
	public float neededBlueLights;
	// Use this for initialization
	void Start () {
		foreach (Transform child in puzzle.transform) 
		{
			torches.Add (child.gameObject);	
		}
		updatePuzzle = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (updatePuzzle) 
		{
			countBlueLights = 0;
			foreach (GameObject torch in torches) 
			{
				if (torch.GetComponent<Torchelight> ().pickedColor.Equals(Torchelight.colorOfTorch.Blue)) 
				{
					if (torch.GetComponent<Torchelight> ().getIsActivated()) 
					{
						countBlueLights++;
					}
				}
			}
			updatePuzzle = !updatePuzzle;
		}

		if (countBlueLights == neededBlueLights) 
		{
			Debug.Log ("You find all.");
			countBlueLights = 0;
		}
	}
}
