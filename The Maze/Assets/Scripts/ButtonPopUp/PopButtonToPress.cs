using UnityEngine;
using System.Collections;

public class PopButtonToPress : MonoBehaviour {

	public GUISkin skin;
	public float boxX;
	public float boxY;
	public float boxWidth;
	public float boxHeight;

	public GameObject firstRiddleText;
	public GameObject secondRiddleText;
	public GameObject thirdRiddleText;

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

	public float timerToOpen;

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
		Debug.Log (rightDoor);
		Debug.Log (leftDoor);

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

		if (isSecondWordGuessed && isFirstWordGuessed) 
		{
			if (word.Equals (answerForThirdRiddle))  
			{
				Debug.Log ("The third word is guessed");
				ShowMessage ("Correct");
				open = !open;
			} else {
				Debug.Log ("The third word is not guessed");
				ShowMessage ("Wrong");
			}
		}

		if (isFirstWordGuessed && !isSecondWordGuessed)
		{
			if (word.Equals (answerForSecondRiddle)) 
			{
				Debug.Log ("The second word is guessed");
				isSecondWordGuessed = !isSecondWordGuessed;
				ShowMessage ("Correct");
				secondRiddleText.SetActive (false);
				thirdRiddleText.SetActive (true);
			}

			if (!isSecondWordGuessed)
			{
				Debug.Log ("The second word is not guessed");
				ShowMessage ("Wrong");
			}

		}

		if (!isFirstWordGuessed) 
		{
			if (word.Equals (answerForFirstRiddle)) 
			{
				Debug.Log ("The first word is guessed");
				ShowMessage ("Correct");
				isFirstWordGuessed = !isFirstWordGuessed;
				firstRiddleText.SetActive (false);
				secondRiddleText.SetActive (true);
			} else {
				if (!isFirstWordGuessed) 
				{
					Debug.Log ("The first word is not guessed");
					Debug.Log ("first if");
					ShowMessage ("Wrong");
				}
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
		timerToOpen -= Time.deltaTime;
		if (timerToOpen > 0) 
		{
			if (!openDoorSource.isPlaying) 
			{
				openDoorSource.PlayOneShot (openDoorSound, 1F);
			}

			leftDoor.transform.Rotate (Vector3.up * Time.deltaTime * 10, Space.World);
			rightDoor.transform.Rotate (Vector3.down * Time.deltaTime * 10, Space.World);
		}

	}
}
