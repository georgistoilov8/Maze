using UnityEngine;
using System.Collections;

public class Display : MonoBehaviour
{
	/// <summary>
	/// to change the message displayed, simply change the public variable below
	/// </summary>
	public string message;
	/// <summary>
	/// You can add as many digits as you want here (use the inspector)
	/// </summary>
	public Digit[] digit;

	void Start ()
	{
	}

	public void setMessage(string m)
	{
		message = m;
	}

	void FixedUpdate ()
	{
		int i = 0;
		for (i = 0; i < digit.Length && i < message.Length; i++)
		{
			digit[i].letter = message[i].ToString();
		}
		for (i = i; i < digit.Length; i++)
		{
			digit[i].letter = " ";
		}
	}
}
