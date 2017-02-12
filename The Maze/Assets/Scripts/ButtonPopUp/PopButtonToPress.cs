using UnityEngine;
using System.Collections;

public class PopButtonToPress : MonoBehaviour {

	public GUISkin skin;
	public float boxX;
	public float boxY;
	public float boxWidth;
	public float boxHeight;

	public string answerForFirstRiddle;
	public string answerForSecondRiddle;
	public string answerForThirdRiddle;

	public GameObject doorToOpen;
	private GameObject leftDoor;
	private GameObject rightDoor;

	private bool isFirstWordGuessed;
	private bool isSecondWordGuessed;
	private bool canClick;
	private bool toWait;
	private bool open;

	public float timeToWait;
	private float countTime;

	private GameObject display;
	private Display displayScript;
	private GameObject puzzle;
	private QandAPuzzle puzzleScript;

	private AudioSource openDoorSource;
	public AudioClip openDoorSound;

	Rect textBox;
	// Use this for initialization
	void Start () 
	{
		canClick = false;
		open = false;

		countTime = timeToWait;

		display = GameObject.Find ("DisplayPuzzle1");
		displayScript = display.GetComponent<Display> ();
		puzzle = GameObject.Find ("AlphabetMechanic");
		puzzleScript = puzzle.GetComponent<QandAPuzzle> ();

		textBox = new Rect ((Screen.width / 2) + boxX, (Screen.height / 2) + boxY, boxWidth, boxHeight);

		isFirstWordGuessed = false;
		isSecondWordGuessed = false;
		toWait = false;

		leftDoor = doorToOpen.transform.FindChild ("Big_Door_L").gameObject;
		rightDoor = doorToOpen.transform.FindChild ("Big_Door_R").gameObject;
		Debug.Log (rightDoor.transform.rotation.y);

		openDoorSource = doorToOpen.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canClick) 
		{
			if (Input.GetButtonDown ("ClickButton")) 
			{
				Debug.Log ("Click");
				CheckWord ();
			}
		}

		if (toWait) 
		{
			if (countTime > 0) 
			{
				countTime -= Time.deltaTime;
			} else {
				puzzleScript.isGuessed = !puzzleScript.isGuessed;
				toWait = !toWait;
				countTime = timeToWait;
			}
		}

		if (open) 
		{
			OpenDoor ();
		}
	}

	void OnGUI()
	{
		if (canClick) 
		{
			GUI.Box (textBox, "", skin.GetStyle("ButtonE"));
		} 
	}

	void CheckWord()
	{
		string word = displayScript.message;
		if (word.Equals (answerForFirstRiddle)) 
		{
			Debug.Log ("The first word is guessed");
			ShowMessage ("Correct");
			isFirstWordGuessed = !isFirstWordGuessed;
		} else {
			if (!isFirstWordGuessed) 
			{
				Debug.Log ("The first word is not guessed");
				ShowMessage ("Wrong");
			}
		}
			
		if (word.Equals (answerForSecondRiddle))
		{
			if (isFirstWordGuessed == true) 
			{
				Debug.Log ("The second word is guessed");
				isSecondWordGuessed = !isSecondWordGuessed;
				ShowMessage ("Correct");
			} else {
				if (!isSecondWordGuessed) 
				{
					Debug.Log ("The second word is not guessed");
					ShowMessage ("Wrong");
				}
			}
		}

		if (word.Equals (answerForThirdRiddle)) 
		{
			if (isSecondWordGuessed == true) 
			{
				Debug.Log ("The third word is guessed");
				ShowMessage ("Correct");
				open = !open;
			} else {
				Debug.Log ("The third word is not guessed");
				ShowMessage ("Wrong");
			}
		}
	}

	void ShowMessage(string message)
	{
		puzzleScript.isTyping = !puzzleScript.isTyping;
		displayScript.setMessage(message);
		toWait = !toWait;
	}

	void OnTriggerEnter()
	{
		canClick = true;
	}

	void OnTriggerExit()
	{
		canClick = false;
	}

	private void OpenDoor()
	{
		
		if (!openDoorSource.isPlaying) 
		{
			openDoorSource.PlayOneShot (openDoorSound, 1F);
		}

		if (leftDoor.transform.rotation.y > 0) 
		{
			leftDoor.transform.Rotate (Vector3.up * Time.deltaTime * 10, Space.World);
		}
		if (rightDoor.transform.rotation.y < 0.7071)
		{
			rightDoor.transform.Rotate (Vector3.down * Time.deltaTime * 10, Space.World);
		}
	}
}
