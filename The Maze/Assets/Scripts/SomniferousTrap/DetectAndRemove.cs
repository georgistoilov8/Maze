using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndRemove : MonoBehaviour {

	public GameObject trap;
	ActivateTrap trapScript;

	// Use this for initialization
	void Start () 
	{
		trapScript = trap.GetComponent<ActivateTrap> ();
	}
		
	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals ("Player")) 
		{
			trapScript.RemoveStrengthFromPlayer ();
		}
	}
}
