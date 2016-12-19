using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPuzzle : MonoBehaviour {

	private List<GameObject> torches = new List<GameObject>();

	public static bool updatePuzzle;
	public GameObject puzzle;

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
		/*if (updatePuzzle) 
		{
			foreach (GameObject torch in torches) 
			{
				Debug.Log(torch.GetComponent<Torchelight> ().pickedColor);
			}
			updatePuzzle = !updatePuzzle;
		}*/
	}
}
