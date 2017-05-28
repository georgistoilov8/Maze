using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

	private List<GameObject> lamps = new List<GameObject>();
	public GameObject lampsInMaze;
	private GameObject player;

	public float forwardSpace;
	public float backwardSpace;
	public float leftSpace;
	public float rightSpace;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		foreach (Transform child in lampsInMaze.transform)
		{
			lamps.Add (child.gameObject);
		}
	}

	void FixedUpdate () {
		foreach (GameObject child in lamps) 
		{
			if (child.transform.position.x < (player.transform.position.x + forwardSpace) && 
				child.transform.position.x > (player.transform.position.x - backwardSpace) &&
					child.transform.position.z > (player.transform.position.z - leftSpace) && 
						child.transform.position.z < (player.transform.position.z + rightSpace)) 
			{
				child.SetActive (true);
			} else {
				if(child.activeSelf.Equals(true))
					child.SetActive (false);
			}
		}
	}
}
