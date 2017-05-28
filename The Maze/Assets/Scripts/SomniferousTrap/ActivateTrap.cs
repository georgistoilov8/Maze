using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ActivateTrap : MonoBehaviour {

	public float playerStrength;
	private float playerStrengthCopy;
	public float removeStrength;

	private bool playerDestroyed;
	private bool playerRespawn;
	private bool activateTraps;
	private bool notDead;
	private GameObject player;
	private GameObject playerChild;
	//SmokeDetection detection;

	public GameObject topEyelid;
	public GameObject bottomEyelid; 
	public GameObject topEyelid2;
	public GameObject bottomEyelid2;
	public GameObject playerObj;
	public float timer;
	private float timerCopy;

	private bool stopAnimation;
	private float timerAnimation;
	private bool openEyes;
	private float timerToOpen;

	void Start () {
		activateTraps = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerChild = player.transform.FindChild ("FirstPersonCharacter").gameObject;
		notDead = true;
		timerCopy = timer;
		playerDestroyed = playerRespawn = false;
		stopAnimation = false;
		timerAnimation = 6f;
		openEyes = false;
		timerToOpen = 7f;
		playerStrengthCopy = playerStrength;
	}
	
	// Update is called once per frame
	void Update () {
		if (activateTraps) 
		{
			foreach (Transform child in transform) 
			{
				if(!child.name.Equals("RespawnPoint"))
					child.GetComponent<SmokeDetection> ().Activate ();
			}
			activateTraps = !activateTraps;
		}

		if (playerDestroyed) 
		{
			timer -= Time.deltaTime;
			if (timer <= 0) 
			{
				playerDestroyed = false;
				timer = timerCopy;
			}
		}

		if (playerRespawn) 
		{
			Respawn ();
			playerRespawn = false;
		}

		if (stopAnimation) 
		{
			timerAnimation -= Time.deltaTime;
			player.transform.Translate (Vector3.down * Time.deltaTime * 0.2f);
			if (timerAnimation <= 0) 
			{
				topEyelid.GetComponent<Animator> ().enabled = false;
				bottomEyelid.GetComponent<Animator> ().enabled = false;
				stopAnimation = false;
				timerAnimation = 6f;
				playerRespawn = true;
			}
		}

		if (openEyes) 
		{
			timerToOpen -= Time.deltaTime;
			if (timerToOpen <= 0) 
			{
				topEyelid2.GetComponent<Animator> ().enabled = false;
				bottomEyelid2.GetComponent<Animator> ().enabled = false;
				openEyes = false;
				timerToOpen = 7f;
			}
		}
	}

	public void RemoveStrengthFromPlayer()
	{
		playerStrength -= removeStrength;
		if (playerStrength <= 0 && notDead) 
		{
			Debug.Log ("The player died.");
			FallDown ();
			notDead = false;
		}
		Debug.Log ("playerStrength = " + playerStrength);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Player")) 
		{
			activateTraps = !activateTraps;
		}
	}

	private void FallDown()
	{
		FirstPersonController.isInventoryOpen = !FirstPersonController.isInventoryOpen;
		topEyelid.GetComponent<Animator> ().enabled = true;
		bottomEyelid.GetComponent<Animator> ().enabled = true;
		stopAnimation = true;
	}

	private void Respawn()
	{
		Transform newPosition = null;
		foreach (Transform child in transform) 
		{
			if (child.name.Equals ("RespawnPoint"))
				newPosition = child;
		}
		player.transform.position = new Vector3(newPosition.position.x, newPosition.position.y, newPosition.position.z);

		topEyelid2.GetComponent<Animator> ().enabled = true;
		bottomEyelid2.GetComponent<Animator> ().enabled = true;
		openEyes = true;
		FirstPersonController.isInventoryOpen = !FirstPersonController.isInventoryOpen;
		playerStrength = playerStrengthCopy;
		notDead = true;
		topEyelid.GetComponent<Animator> ().Rebind ();
		bottomEyelid.GetComponent<Animator> ().Rebind ();
	}
}
