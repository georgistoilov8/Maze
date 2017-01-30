using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WaitingEscScript : MonoBehaviour {

	public GameObject menu;
	private EscMenuScript s;

	private CursorLockMode cursorState;

	void Start()
	{
		s = menu.GetComponent<EscMenuScript> ();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Esc")) 
		{
			if (FirstPersonController.isInventoryOpen.Equals (true)) 
			{
				s.StopMenu ();
				FirstPersonController.isInventoryOpen = false;
				cursorState = CursorLockMode.None;
				Cursor.lockState = cursorState;
				SetCursorState ();
			} else 
			{
				FirstPersonController.isInventoryOpen = true;
				cursorState = CursorLockMode.None;
				Cursor.lockState = cursorState;
				SetCursorState ();
				s.StartMenu ();
			}
			
		}
	}

	private void SetCursorState ()
	{
		Cursor.lockState = cursorState;
		Cursor.visible = (CursorLockMode.Locked != cursorState);
	}
}
