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

	private void Start () {
		bottomGlasses = GameObject.FindGameObjectsWithTag ("BottomGlass");
		roundGlasses = GameObject.FindGameObjectsWithTag ("RoundGlass");
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	private GameObject CheckTrapPosition(){
		foreach (GameObject bottomGlass in bottomGlasses) {
			float distance = Vector3.Distance (bottomGlass.transform.position, player.transform.position);
			Debug.Log (distance);
			Debug.Log (bottomGlass.name);
			if (distance < 5 && distance > -5) {
				return bottomGlass;
			}
		}
		return null;
	}

	private GameObject CheckGlassWallsPosition(){
		foreach (GameObject roundGlass in roundGlasses) {
			float distance = Vector3.Distance (roundGlass.transform.position, player.transform.position);
			if (distance < 5 && distance > -5) {
				return roundGlass;
			}
		}
		return null;
	}
		
	private void Update () {
		if (timerStart) {
			SpinT ();
		}
		if (isGlassWallDown) {
			MoveTrapUp ();
		}
	}

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

	private void MoveTrapUp(){
		trapRoundGlasses.transform.position = new Vector3 (transform.position.x, transform.position.y + 2.6f, transform.position.z);
		isGlassWallDown = false;
	}

	private void MoveTrapDown(){
		trapRoundGlasses.transform.position = new Vector3 (transform.position.x, transform.position.y - 2.6f, transform.position.z);
		FirstPersonController.isInventoryOpen = !FirstPersonController.isInventoryOpen;
		cursorState = CursorLockMode.None;
		Cursor.lockState = cursorState;
		SetCursorState ();
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("asdasdasdasd");
		if (other.tag.Equals("Player")) {
			trapBottomGlass = CheckTrapPosition ();
			trapRoundGlasses = CheckGlassWallsPosition ();
			Debug.Log("Enter");
			timerSeconds = 5f;
			isGlassWallDown = true;
			timerStart = true;
			FirstPersonController.isInventoryOpen = !FirstPersonController.isInventoryOpen;
			cursorState = CursorLockMode.None;
			Cursor.lockState = cursorState;
			SetCursorState ();
		}
	}

	private void SetCursorState ()
	{
		Cursor.lockState = cursorState;
		Cursor.visible = (CursorLockMode.Locked != cursorState);
	}
}
