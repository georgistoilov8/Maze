using UnityEngine;
using System.Collections;

public class AlphabetDetector : QandAPuzzle {

	private bool isPlayerDetected;
	private bool isLightOn;
	private Vector3 buttonPosition;
	private GameObject lightOnStep;

	// Use this for initialization
	void Start () 
	{
		isPlayerDetected = false;
		buttonPosition = transform.position;
		isLightOn = false;
		foreach (Transform child in transform) 
		{
			if (child.name.Equals ("Light")) 
			{
				lightOnStep = child.gameObject;
			}
		}
		lightOnStep.SetActive (isLightOn);
		Debug.Log(buttonPosition.y);
	}

	// Update is called once per frame
	void Update () 
	{
		if (isPlayerDetected) 
		{
			if (buttonPosition.y > 0.09f)   //Checking position relative to the world (спрямо света, а не обекта в който се намира този обект)
			{
				buttonPosition = new Vector3 (buttonPosition.x, buttonPosition.y - 0.01f, buttonPosition.z);
			}	
		} else if(!isPlayerDetected){
			if (buttonPosition.y <= 0.13f) {
				buttonPosition = new Vector3 (buttonPosition.x, buttonPosition.y + 0.01f, buttonPosition.z);
			}
		}
	}

	string DetermineLetter(Vector3 position)
	{
		string letter = null;
		float letterX = position.x;
		float letterZ = position.z;
		if (letterX == A.x && letterZ == A.z) 
		{
			Debug.Log ("The letter is A");
			letter = "a";
		}else if (letterX == B.x && letterZ == B.z) 
		{
			Debug.Log ("The letter is B");
			letter = "b";
		}else if (letterX == C.x && letterZ == C.z) 
		{
			Debug.Log ("The letter is C");
			letter = "c";
		}


		return letter;
	}

	void OnTriggerEnter ()
	{
		Debug.Log ("Vleznah");
		isLightOn = true;
		lightOnStep.SetActive (isLightOn);
		isPlayerDetected = true;
		word += DetermineLetter (buttonPosition);
		Debug.Log (word);
	}

	void OnTriggerExit ()
	{
		Debug.Log ("Izleznah");
		isLightOn = false;
		lightOnStep.SetActive (isLightOn);
		isPlayerDetected = false;
	}
}
