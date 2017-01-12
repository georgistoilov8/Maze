using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour {

	private bool increasing;
	private bool decreasing;

	private Light spotlightFlashing;

	public float increasingRangeValue;
	public float decreasingRangeValue;
	public float maximumRange;
	// Use this for initialization
	void Start () {
		spotlightFlashing = this.transform.GetComponent<Light> ();
		increasing = true;
		decreasing = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (spotlightFlashing.range <= 0) 
		{
			decreasing = false;
			increasing = true;
		}

		if (spotlightFlashing.range >= maximumRange) 
		{
			decreasing = true;
			increasing = false;
		}

		if (spotlightFlashing.range >= 0 && decreasing) 
		{
			spotlightFlashing.range -= decreasingRangeValue;
		}

		if (spotlightFlashing.range <= maximumRange && increasing) 
		{
			spotlightFlashing.range += increasingRangeValue;
		}
	}
}
