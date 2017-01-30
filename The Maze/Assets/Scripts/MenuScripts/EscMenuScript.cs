using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class EscMenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas optionMenu;
	public Button resumeButton;
	public Button optionButton;
	public Button exitButton;
	public Text theMaze;

	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		optionMenu = optionMenu.GetComponent<Canvas> ();
		resumeButton = resumeButton.GetComponent<Button> ();	
		optionButton = optionButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
		quitMenu.enabled = false;
		optionMenu.enabled = false;
		theMaze.enabled = false;
		resumeButton.gameObject.SetActive (false);
		optionButton.gameObject.SetActive (false);
		exitButton.gameObject.SetActive (false);

	}

	public void ExitPress()
	{
		quitMenu.enabled = true;
		resumeButton.enabled = false;
		optionButton.enabled = false;
		exitButton.enabled = false;
	}

	public void NoPress()
	{
		quitMenu.enabled = false;
		resumeButton.enabled = true;
		optionButton.enabled = true;
		exitButton.enabled = true;
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void OptionPress()
	{
		resumeButton.gameObject.SetActive (false);
		optionButton.gameObject.SetActive (false);
		exitButton.gameObject.SetActive (false);
		optionMenu.enabled = true;
		theMaze.enabled = false;
	}

	public void BackPress()
	{
		resumeButton.gameObject.SetActive (true);
		optionButton.gameObject.SetActive (true);
		exitButton.gameObject.SetActive (true);
		optionMenu.enabled = false;
		theMaze.enabled = true;
	}

	public void StartMenu()
	{
		resumeButton.gameObject.SetActive (true);
		optionButton.gameObject.SetActive (true);
		exitButton.gameObject.SetActive (true);
		theMaze.enabled = true;
	}

	public void StopMenu()
	{
		resumeButton.gameObject.SetActive (false);
		optionButton.gameObject.SetActive (false);
		exitButton.gameObject.SetActive (false);
		quitMenu.enabled = false;
		optionMenu.enabled = false;
		theMaze.enabled = false;
	}

	public void ResumePress()
	{
		StopMenu ();
		FirstPersonController.isInventoryOpen = false;
	}
}
