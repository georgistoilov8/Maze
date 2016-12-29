using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// This class is applied to a single digit. To change the letter displayed, simply change de public variable "public string letter"
// if you want to add a character, add an entry in the dictionnary chList, like this : chList.Add("1", "11111001000000");
// the first parameter is the letter, the second parameter is the code for each part. 0 is off, and 1 on.
//
// The order of the part is: 1/ HORIZONTAL  [bottom, left-middle, right-middle, top]
//                           2/ VERTICAL    [bottom-left, bottom-right, top-left, top-right]
//                           3/ INNER PARTS [bottom /, bottom |, bottom \, top \, top |, top /]

public class Digit : MonoBehaviour
{
	public Color inactiveColor;
	public Color activeColor;
	public string letter;
	public bool forceLower = true;
	public GameObject[] part;
	public Dictionary<string, string> chList;
	string actualLetter = " ";

	// Use this for initialization
	void Start ()
	{
		MeshRenderer[] tmp = GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer rd in tmp)
		{
			rd.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			//rd.castShadows = false;
			rd.receiveShadows = false;
		}
		chList = new Dictionary<string, string>();
		//              "   -  ||   \|/"  |\|/|
		//              " --           "  |- -|
		//              "-   ||  /|\   "  |/|\|
		chList.Add(" ", "00000000000000");
		chList.Add("0", "10011111000000");
		chList.Add("1", "00000101000001");
		chList.Add("2", "11111001000000");
		chList.Add("3", "11110101000000");
		chList.Add("4", "01100111000000");
		chList.Add("5", "11110110000000");
		chList.Add("6", "11111110000000");
		chList.Add("7", "00010000100001");
		chList.Add("8", "11111111000000");
		chList.Add("9", "11110111000000");

		chList.Add("a", "01111111000000");
		chList.Add("b", "11101110000000");
		chList.Add("c", "10011010000000");
		chList.Add("d", "11101101000000");
		chList.Add("e", "11111010000000");
		chList.Add("f", "01011010000000");
		chList.Add("g", "10111110000000");
		chList.Add("h", "01101111000000");
		chList.Add("i", "00000000010010");
		chList.Add("j", "10001101000000");
		chList.Add("k", "01001010001001");
		chList.Add("l", "10001010000000");
		chList.Add("m", "00001111000101");
		chList.Add("n", "00001111001100");
		chList.Add("o", "10011111000000");
		chList.Add("p", "01111011000000");
		chList.Add("q", "01110111000000");
		chList.Add("r", "01111011001000");
		chList.Add("s", "11110110000000");
		chList.Add("t", "00010000010010");
		chList.Add("u", "10001111000000");
		chList.Add("v", "00000101001100");
		chList.Add("w", "00001111101000");
		chList.Add("x", "00000000101101");
		chList.Add("y", "11100111000000");
		chList.Add("z", "10010000100001");

		chList.Add("!", "10100111111000");
		chList.Add("@", "11111111111000");
		chList.Add("#", "11011001010000");

		SetDigit(" ");
	}
	
	void FixedUpdate ()
	{
		SetDigit(letter);
	}

	void SetDigit(string letter)
	{
		if (letter == actualLetter)
			return;
		if (forceLower)
			letter = letter.ToLower();
		if (chList.ContainsKey(letter) == false)
			return;
		for (int i = 0; i < 14; i++)
		{
			if (chList[letter][i] == '0')
				part[i].GetComponent<Renderer>().material.color = inactiveColor;
			else
				part[i].GetComponent<Renderer>().material.color = activeColor;
		}
		actualLetter = letter;
	}
}
