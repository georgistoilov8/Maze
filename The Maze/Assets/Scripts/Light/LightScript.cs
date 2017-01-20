using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour {

	private List<GameObject> lamps = new List<GameObject>();
	private GameObject lampPuzzle;
	private GameObject player;
	private Plane[] planes;

	public float forwardSpace;
	public float backwardSpace;
	public float leftSpace;
	public float rightSpace;


	// Use this for initialization
	void Start () {
		planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		player = GameObject.FindGameObjectWithTag ("Player");
		lampPuzzle = GameObject.FindGameObjectWithTag ("Lamps");
		foreach (Transform child in lampPuzzle.transform)
		{
			Debug.Log (child);
			lamps.Add (child.gameObject);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 CameraDirectionFrontX = new Vector3 (Camera.main.transform.forward.x + 9f, Camera.main.transform.forward.y, Camera.main.transform.forward.z + 9f);
		//Vector3 CameraDirectionFrontZ = Camera.main.transform.forward.z + 15f;
		//Vector3 CameraDirectionBack = -CameraDirectionFront;
		//Vector3 CameraDirectionRight = Camera.main.transform.right;
		//Vector3 CameraDirectionLeft = -CameraDirectionRight;
		//Debug.Log (CameraDirectionBack + "   " + CameraDirectionFront);
		foreach (GameObject child in lamps) 
		{
			Collider childCollider = child.GetComponent<Collider> ();
			if (GeometryUtility.TestPlanesAABB (planes, childCollider.bounds)) {
				child.SetActive (true);
			} else 
			{
				child.SetActive (false);
			}
			//if (Vector3.SqrMagnitude(child.transform.position - CameraDirectionFront) > -15 && child.transform.position.x >= (CameraDirectionBack.x - backwardSpace) &&
				//child.transform.position.z <= (CameraDirectionLeft.z + leftSpace) && child.transform.position.z >= (CameraDirectionRight.z - rightSpace)) {
				//child.SetActive (true);
			//} else 
			//{
				//child.SetActive (false);
			//
		}
	}
}
