using UnityEngine;
using System.Collections;

public class AlphabetDetector : QandAPuzzle {

	//private bool isPlayerDetected;
	private bool isLightOn;
	private Vector3 buttonPosition;
	private GameObject lightOnStep;

	void Start () 
	{
		//isPlayerDetected = false;
		buttonPosition = transform.position;
		isLightOn = false;

		foreach (Transform child in transform) 
		{
			if (child.name.Equals ("Light")) 
			{
				lightOnStep = child.gameObject;
			}
		}
		lightOnStep.SetActive (isLightOn);
		//Debug.Log(buttonPosition.y);
	}

	string DetermineLetter(Vector3 position)
	{
		string letter = null;
		float letterX = position.x;
		float letterZ = position.z;
		if (letterX == A.x && letterZ == A.z) 
		{
			Debug.Log ("The letter is A");
			letter = "a";
		}else if (letterX == B.x && letterZ == B.z) 
		{
			Debug.Log ("The letter is B");
			letter = "b";
		}else if (letterX == C.x && letterZ == C.z) 
		{
			Debug.Log ("The letter is C");
			letter = "c";
		}else if (letterX == D.x && letterZ == D.z) 
		{
			Debug.Log ("The letter is D");
			letter = "d";
		}else if (letterX == E.x && letterZ == E.z) 
		{
			Debug.Log ("The letter is E");
			letter = "e";
		}else if (letterX == F.x && letterZ == F.z) 
		{
			Debug.Log ("The letter is F");
			letter = "f";
		}else if (letterX == G.x && letterZ == G.z) 
		{
			Debug.Log ("The letter is G");
			letter = "g";
		}else if (letterX == H.x && letterZ == H.z) 
		{
			Debug.Log ("The letter is H");
			letter = "h";
		}else if (letterX == I.x && letterZ == I.z) 
		{
			Debug.Log ("The letter is I");
			letter = "i";
		}else if (letterX == J.x && letterZ == J.z) 
		{
			Debug.Log ("The letter is J");
			letter = "j";
		}else if (letterX == K.x && letterZ == K.z) 
		{
			Debug.Log ("The letter is K");
			letter = "k";
		}else if (letterX == L.x && letterZ == L.z) 
		{
			Debug.Log ("The letter is L");
			letter = "l";
		}else if (letterX == M.x && letterZ == M.z) 
		{
			Debug.Log ("The letter is M");
			letter = "m";
		}else if (letterX == N.x && letterZ == N.z) 
		{
			Debug.Log ("The letter is N");
			letter = "n";
		}else if (letterX == O.x && letterZ == O.z) 
		{
			Debug.Log ("The letter is O");
			letter = "o";
		}else if (letterX == P.x && letterZ == P.z) 
		{
			Debug.Log ("The letter is P");
			letter = "p";
		}else if (letterX == Q.x && letterZ == Q.z) 
		{
			Debug.Log ("The letter is Q");
			letter = "q";
		}else if (letterX == R.x && letterZ == R.z) 
		{
			Debug.Log ("The letter is R");
			letter = "r";
		}else if (letterX == S.x && letterZ == S.z) 
		{
			Debug.Log ("The letter is S");
			letter = "s";
		}else if (letterX == T.x && letterZ == T.z) 
		{
			Debug.Log ("The letter is T");
			letter = "t";
		}else if (letterX == U.x && letterZ == U.z) 
		{
			Debug.Log ("The letter is U");
			letter = "u";
		}else if (letterX == V.x && letterZ == V.z) 
		{
			Debug.Log ("The letter is V");
			letter = "v";
		}else if (letterX == W.x && letterZ == W.z) 
		{
			Debug.Log ("The letter is W");
			letter = "w";
		}else if (letterX == X.x && letterZ == X.z) 
		{
			Debug.Log ("The letter is X");
			letter = "x";
		}else if (letterX == Y.x && letterZ == Y.z) 
		{
			Debug.Log ("The letter is Y");
			letter = "y";
		}else if (letterX == Z.x && letterZ == Z.z) 
		{
			Debug.Log ("The letter is Z");
			letter = "z";
		}
			
		return letter;
	}

	void OnTriggerEnter ()
	{
		Debug.Log ("Vleznah");
		//Debug.Log (transform.position);
		isLightOn = true;
		lightOnStep.SetActive (isLightOn);
		//isPlayerDetected = true;
		word += DetermineLetter (buttonPosition);

		Debug.Log (word);
	}

	void OnTriggerExit ()
	{
		Debug.Log ("Izleznah");
		isLightOn = false;
		lightOnStep.SetActive (isLightOn);
		//isPlayerDetected = false;
	}
}
