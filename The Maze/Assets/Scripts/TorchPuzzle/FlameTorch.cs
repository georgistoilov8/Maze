using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTorch : MonoBehaviour {

	private bool canClick;
	private GameObject torch;
	private GameObject currentSensor;

	public GameObject TorchLight;
	public GameObject MainFlame;
	public GameObject BaseFlame;
	public GameObject Etincelles;
	public GameObject Fumee;
	public float MaxLightIntensity;
	public float IntensityLight;
	public enum colorOfTorch {Blue, Orange};
	public colorOfTorch pickedColor;

	public GUISkin skin;
	public float boxX;
	public float boxY;
	public float boxWidth;
	public float boxHeight;

	private int count;
	private Rect box;

	Inventory inventory;
	// Use this for initialization
	void Start () 
	{
		canClick = false;
		count = 0;
		box = new Rect ((Screen.width / 2) + boxX, (Screen.height / 2) + boxY, boxWidth, boxHeight);
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		//currentSensor = this.gameObject;
		//torch = currentSensor.transform.GetChild (0).gameObject;
		//Debug.Log (torch);
		//Debug.Log (currentSensor);

		//TorchLight = this.transform.Find ("Torch_light").gameObject;
		//MainFlame = this.transform.Find ("flam").gameObject;
		//BaseFlame = this.transform.Find ("base_flam").gameObject;
		//Etincelles = this.transform.Find ("etincelles").gameObject;
		//Fumee = this.transform.Find ("fumee").gameObject;

		//torch.GetComponent<Torchelight> ().SetParticles (TorchLight, MainFlame, BaseFlame, Etincelles, Fumee);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canClick) 
		{
			if (Input.GetButtonDown ("FlameTorch") && inventory.GetIsLighterPicked())
			{
				
				count++;
				if (count % 2 != 0) 
				{
					SetLight ();
					StartFlame ();
				}
					//torch.GetComponent<Torchelight> ().SetTurnOn();
				if (count % 2 == 0)
				{
					SetLight ();
					StopFlame ();	
				}
					//torch.GetComponent<Torchelight> ().SetTurnOff();
			}
		}
		TorchLight.GetComponent<Light>().intensity=IntensityLight/2f+Mathf.Lerp(IntensityLight-0.1f,IntensityLight+0.1f,Mathf.Cos(Time.time*30));
		if (pickedColor.Equals (colorOfTorch.Blue)) 
		{
			TorchLight.GetComponent<Light>().color=new Color(Mathf.Min(IntensityLight/0.5f,0f),Mathf.Min(IntensityLight/2f,0.2f),1f); // blue
		} else {
			TorchLight.GetComponent<Light>().color=new Color(Mathf.Min(IntensityLight/1f,1.5f),Mathf.Min(IntensityLight/2f,1f),0f); // orange
		}
	}

	void OnTriggerEnter (Collider other) 
	{
		canClick = !canClick;
	}

	void OnTriggerExit ()
	{
		canClick = !canClick;
	}

	void OnGUI()
	{
		if (canClick) 
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

}

