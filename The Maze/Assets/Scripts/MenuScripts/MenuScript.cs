using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas options;
	public Button startText;
	public Button optionText;
	public Button exitText;

	// Use this for initialization
	void Start () 
	{
		quitMenu = quitMenu.GetComponent<Canvas> ();
		options = options.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
		options.enabled = false;
	}

	public void ExitPress()
	{
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
		optionText.enabled = false;
	}

	public void NoPress()
	{
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
		optionText.enabled = true;
	}

	public void StartLevel()
	{
		SceneManager.LoadScene (1);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void OptionPress()
	{
		startText.gameObject.SetActive (false);
		exitText.gameObject.SetActive (false);
		optionText.gameObject.SetActive (false);
		options.enabled = true;
	}

	public void BackPress()
	{
		startText.gameObject.SetActive (true);
		exitText.gameObject.SetActive (true);
		optionText.gameObject.SetActive (true);
		options.enabled = false;
	}
}
