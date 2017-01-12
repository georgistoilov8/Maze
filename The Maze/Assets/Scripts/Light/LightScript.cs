using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

	private List<GameObject> lamps = new List<GameObject>();
	private GameObject lampPuzzle;
	private GameObject player;

	public float forwardSpace;
	public float backwardSpace;
	public float leftSpace;
	public float rightSpace;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		lampPuzzle = GameObject.FindGameObjectWithTag ("Lamps");
		foreach (Transform child in lampPuzzle.transform)
		{
			Debug.Log (child);
			lamps.Add (child.gameObject);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (GameObject child in lamps) 
		{
			if (child.transform.position.x <= (player.transform.position.x + forwardSpace) && child.transform.position.x >= (player.transform.position.x - backwardSpace) &&
			   child.transform.position.z <= (player.transform.position.z + leftSpace) && child.transform.position.z >= (player.transform.position.z - rightSpace)) {
				child.SetActive (true);
			} else 
			{
				child.SetActive (false);
			}
		}
	}
}
