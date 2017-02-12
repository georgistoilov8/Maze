using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Spin : MonoBehaviour {
	protected bool timerStart = false;
	protected bool isGlassWallDown = false;
	protected float timerSeconds = 0f;
	GameObject trapBottomGlass;
	GameObject trapRoundGlasses;
	GameObject[] bottomGlasses;
	GameObject[] roundGlasses;
	GameObject player;
	public int TrapSpinSpeed;
	public int PlayerSpinSpeed;
	private CursorLockMode cursorState;

	// Initialize some variables
	private void Start () {
		bottomGlasses = GameObject.FindGameObjectsWithTag ("BottomGlass");
		roundGlasses = GameObject.FindGameObjectsWithTag ("RoundGlass");
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Check the position of the bottom glass of the trap. 
	// I use this function to activate the right trap
	private GameObject CheckTrapPosition(){
		foreach (GameObject bottomGlass in bottomGlasses) {
			float distance = Vector3.Distance (bottomGlass.transform.position, player.transform.position);
			if (distance < 5 && distance > -5) {
				return bottomGlass;
			}
		}
		return null;
	}

	// Same as the function above it. But this time I check for walls of the trap.
	private GameObject CheckGlassWallsPosition(){
		foreach (GameObject roundGlass in roundGlasses) {
			float distance = Vector3.Distance (roundGlass.transform.position, player.transform.position);
			if (distance < 5 && distance > -5) {
				return roundGlass;
			}
		}
		return null;
	}

	// Main loop of the script
	private void Update () {
		if (timerStart) {
			SpinT ();
		}
		if (isGlassWallDown) {
			MoveTrapUp ();
		}
	}

	// Function for rotating the whole trap with small angle.
	// Called many times before the time runs out.
	private void SpinT(){
		timerSeconds -= Time.deltaTime;
		trapBottomGlass.transform.Rotate (Vector3.up *TrapSpinSpeed* Time.deltaTime, Space.World);
		player.transform.Rotate (Vector3.up * PlayerSpinSpeed * Time.deltaTime, Space.World);

		if (timerSeconds < 0) {
			MoveTrapDown ();
			timerStart = false;
			Destroy(this);
		}
	}

	// Because the trap is under the map, this function shows it.
	private void MoveTrapUp(){
		trapRoundGlasses.transform.position = new Vector3 (transform.position.x, transform.position.y + 2.6f, transform.position.z);
		isGlassWallDown = false;
	}

	// This function is used to bring back down the trap.
	private void MoveTrapDown(){
		trapRoundGlasses.transform.position = new Vector3 (transform.position.x, transform.position.y - 2.6f, transform.position.z);
		FirstPersonController.isInventoryOpen = !FirstPersonController.isInventoryOpen;
		cursorState = CursorLockMode.None;
		Cursor.lockState = cursorState;
		SetCursorState ();
	}

	// This function is for detecting the player entering the sensor
	void OnTriggerEnter(Collider other){
		if (other.tag.Equals("Player")) {
			Debug.Log("Enter");
			trapBottomGlass = CheckTrapPosition ();
			trapRoundGlasses = CheckGlassWallsPosition ();
			timerSeconds = 5f;
			isGlassWallDown = true;
			timerStart = true;
			FirstPersonController.isInventoryOpen = !FirstPersonController.isInventoryOpen;
			cursorState = CursorLockMode.None;
			Cursor.lockState = cursorState;
			SetCursorState ();
		}
	}

	// This function is for stoping the movement of the player
	private void SetCursorState ()
	{
		Cursor.lockState = cursorState;
		Cursor.visible = (CursorLockMode.Locked != cursorState);
	}
}
