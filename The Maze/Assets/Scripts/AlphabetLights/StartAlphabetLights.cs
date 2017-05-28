using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAlphabetLights : MonoBehaviour {

	public GameObject lamps;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Player")) 
		{
			lamps.SetActive (true);
		}
	}
}
