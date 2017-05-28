using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPuzzle : MonoBehaviour {

	private List<GameObject> torches = new List<GameObject>();

	public static bool updatePuzzle;
	public GameObject puzzle;

	private float countBlueLights;
	public float neededBlueLights;

	private AudioSource openDoorSource;
	public AudioClip openDoorSound;

	public GameObject doorToOpen;
	private GameObject leftDoor;
	private GameObject rightDoor;

	public float timerDoors;

	private bool open = false; 

	void Start () {
		foreach (Transform child in puzzle.transform) 
		{
			torches.Add (child.gameObject);	
		}
		updatePuzzle = true;

		leftDoor = doorToOpen.transform.FindChild ("Big_Door_L").gameObject;
		rightDoor = doorToOpen.transform.FindChild ("Big_Door_R").gameObject;

		openDoorSource = doorToOpen.GetComponent<AudioSource> ();
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
			open = true;
		}

		if (open) 
		{
			OpenDoor ();
		}
	}

	private void OpenDoor()
	{
		timerDoors -= Time.deltaTime;
		if (timerDoors > 0) 
		{
			if (!openDoorSource.isPlaying) 
			{
				openDoorSource.PlayOneShot (openDoorSound, 1F);
			}
			leftDoor.transform.Rotate (Vector3.up * Time.deltaTime * 10, Space.World);
			rightDoor.transform.Rotate (Vector3.down * Time.deltaTime * 10, Space.World);
		}
	}
}
