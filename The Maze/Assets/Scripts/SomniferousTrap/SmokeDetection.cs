using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDetection : MonoBehaviour {

	private ParticleSystem smoke;
	private bool isActivated;
	private GameObject smokeDetector;

	public float timer;
	private float originTime;

	private bool boxCollider;

	public AudioClip gasLeakSoundEffect;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		smoke = this.transform.FindChild ("Particle System").GetComponent<ParticleSystem>();
		smoke.Stop ();

		isActivated = false;
		boxCollider = true;
		originTime = timer;

		source = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActivated) 
		{
			if (boxCollider) 
			{
				AddBoxCollider ();
				boxCollider = false;
			}
			if (!smoke.isPlaying) 
			{
				smoke.Play ();
			}
			timer -= Time.deltaTime;
			if (timer <= 0) 
			{
				Deactivate ();
				RemoveBoxCollider ();
				timer = originTime;
			}
			if (!source.isPlaying) 
			{
				source.PlayOneShot (gasLeakSoundEffect, 1F);
			}
		} else 
		{
			if (smoke.isPlaying) 
			{
				smoke.Stop ();
				boxCollider = true;
			}
			if (source.isPlaying) 
			{
				source.Stop ();
			}
		}
	}

	public void Activate()
	{
		isActivated = true;
	}

	public void Deactivate()
	{
		isActivated = false;
	}

	private void AddBoxCollider()
	{
		smokeDetector = this.transform.FindChild ("SmokeDetector").gameObject;
		smokeDetector.AddComponent<BoxCollider> ();
		smokeDetector.GetComponent<BoxCollider> ().isTrigger = true;
	}

	private void RemoveBoxCollider()
	{
		smokeDetector = this.transform.FindChild ("SmokeDetector").gameObject;
		Destroy (smokeDetector.GetComponent<BoxCollider>());
	}
		
}
