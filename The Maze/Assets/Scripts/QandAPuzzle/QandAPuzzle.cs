using UnityEngine;
using System.Collections;

public class QandAPuzzle : MonoBehaviour {

	protected static string word;

	public bool isGuessed;
	public bool isTyping;

	GameObject display;
	Display displayScript;

	protected static Vector3 A;
	protected static Vector3 B;
	protected static Vector3 C;
	protected static Vector3 D;
	protected static Vector3 E;
	protected static Vector3 F;
	protected static Vector3 G;
	protected static Vector3 H;
	protected static Vector3 I;
	protected static Vector3 J;
	protected static Vector3 K;
	protected static Vector3 L;
	protected static Vector3 M;
	protected static Vector3 N;
	protected static Vector3 O;
	protected static Vector3 P;
	protected static Vector3 Q;
	protected static Vector3 R;
	protected static Vector3 S;
	protected static Vector3 T;
	protected static Vector3 U;
	protected static Vector3 V;
	protected static Vector3 W;
	protected static Vector3 X;
	protected static Vector3 Y;
	protected static Vector3 Z;

	void Start () 
	{
		word = "";

		display = GameObject.Find ("DisplayPuzzle1");
		displayScript = display.GetComponent<Display> ();
		Debug.Log (displayScript);

		A.x = 69; A.z = 36.05f;
		B.x = 69; B.z = 33.05f;
		C.x = 69; C.z = 30.05f;
		D.x = 69; D.z = 27.05f;
		E.x = 69; E.z = 24.05f;

		F.x = 72; F.z = 36.05f;
		G.x = 72; G.z = 33.05f;
		H.x = 72; H.z = 30.05f;
		I.x = 72; I.z = 27.05f;
		J.x = 72; J.z = 24.05f;

		K.x = 75; K.z = 36.05f;
		L.x = 75; L.z = 33.05f;
		M.x = 75; M.z = 30.05f;
		N.x = 75; N.z = 27.05f;
		O.x = 75; O.z = 24.05f;

		P.x = 78; P.z = 36.05f;
		Q.x = 78; Q.z = 33.05f;
		R.x = 78; R.z = 30.05f;
		S.x = 78; S.z = 27.05f;
		T.x = 78; T.z = 24.05f;

		U.x = 81; U.z = 36.05f;
		V.x = 81; V.z = 33.05f;
		W.x = 81; W.z = 30.05f;
		X.x = 81; X.z = 27.05f;
		Y.x = 81; Y.z = 24.05f;

		Z.x = 83.5f; Z.z = 30.05f;


	}
		
	void Update () 
	{
		//wordToDisplay = word;
		if (isGuessed)
		{
			word = "";	
			isGuessed = !isGuessed;
			isTyping = !isTyping;
		}
		if (isTyping) 
		{
			displayScript.setMessage(word);
		}
	}
}
