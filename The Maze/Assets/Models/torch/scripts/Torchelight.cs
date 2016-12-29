using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour {

	public GameObject TorchLight;
	public GameObject MainFlame;
	public GameObject BaseFlame;
	public GameObject Etincelles;
	public GameObject Fumee;

	public float MaxLightIntensity;
	public float IntensityLight;

	public enum colorOfTorch {Blue, Orange};
	public colorOfTorch pickedColor;

	private bool isActivated;

	public AudioClip fireSound;
	private AudioSource fireAudioSource;

	public GUISkin skin;
	public float boxX;
	public float boxY;
	public float boxWidth;
	public float boxHeight;

	private bool canFlame;
	private int count;
	private Rect box;

	Inventory inventory;
	void Start () 
	{
		TorchLight.GetComponent<Light>().intensity=IntensityLight;

		SetParticle ();

		canFlame = false;
		count = 0;
		box = new Rect ((Screen.width / 2) + boxX, (Screen.height / 2) + boxY, boxWidth, boxHeight);

		TorchLight.GetComponent<Light> ().enabled = false;
		StopFlame ();

		isActivated = false;

		fireAudioSource = transform.GetComponent<AudioSource> ();

		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
	}

	void Update () 
	{
		if (canFlame) 
		{
			if (Input.GetButtonDown ("FlameTorch") && inventory.GetIsLighterPicked())
			{

				count++;
				if (count % 2 != 0) 
				{
					isActivated = !isActivated;
					SetLight ();
					StartFlame ();
					TorchPuzzle.updatePuzzle = true;
				}
				if (count % 2 == 0)
				{
					isActivated = !isActivated;
					SetLight ();
					StopFlame ();	
					TorchPuzzle.updatePuzzle = true;
				}
			}
		}

		if (isActivated) {
			if (!fireAudioSource.isPlaying)
				fireAudioSource.PlayOneShot (fireSound, 0.3F);
		}


		if (IntensityLight<0) 
			IntensityLight=0;
		if (IntensityLight>MaxLightIntensity) 
			IntensityLight=MaxLightIntensity;		

		TorchLight.GetComponent<Light>().intensity=IntensityLight/2f+Mathf.Lerp(IntensityLight-0.1f,IntensityLight+0.1f,Mathf.Cos(Time.time*30));
		SetParticle ();

		if (pickedColor.Equals (colorOfTorch.Blue)) 
		{
			TorchLight.GetComponent<Light>().color=new Color(Mathf.Min(IntensityLight/0.5f,0f),Mathf.Min(IntensityLight/2f,0.2f),1f); // blue
		} else {
			TorchLight.GetComponent<Light>().color=new Color(Mathf.Min(IntensityLight/1f,1.5f),Mathf.Min(IntensityLight/2f,1f),0f); // orange
		}
			
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.tag.Equals("Player"))
			canFlame = !canFlame;
	}

	void OnTriggerExit (Collider other)
	{
		if(other.tag.Equals("Player"))
			canFlame = !canFlame;
	}

	void OnGUI()
	{
		if (canFlame) 
		{
			GUI.Box (box, "", skin.GetStyle("ButtonFlame"));
		} 
	}

	private void StartFlame()
	{
		MainFlame.GetComponent<ParticleSystem> ().Play ();
		BaseFlame.GetComponent<ParticleSystem> ().Play ();	
		Etincelles.GetComponent<ParticleSystem> ().Play ();
		Fumee.GetComponent<ParticleSystem> ().Play();
	}

	private void StopFlame()
	{
		MainFlame.GetComponent<ParticleSystem> ().Stop ();
		BaseFlame.GetComponent<ParticleSystem> ().Stop ();
		Etincelles.GetComponent<ParticleSystem> ().Stop ();
		Fumee.GetComponent<ParticleSystem> ().Stop ();
	}

	private void SetLight()
	{
		TorchLight.GetComponent<Light> ().enabled = !TorchLight.GetComponent<Light> ().enabled;
	}

	private void SetParticle()
	{
		MainFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*20f;
		BaseFlame.GetComponent<ParticleSystem>().emissionRate=IntensityLight*15f;
		Etincelles.GetComponent<ParticleSystem>().emissionRate=IntensityLight*7f;
		Fumee.GetComponent<ParticleSystem>().emissionRate=IntensityLight*12f;
	}

	public bool getIsActivated()
	{
		return isActivated;
	}
		
}