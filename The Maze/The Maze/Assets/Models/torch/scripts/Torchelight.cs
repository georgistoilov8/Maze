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

	public static bool turnOn;
	public static bool turnOff;

	private float mainFlameEmissionRate;

	void Start () 
	{
		TorchLight.GetComponent<Light>().intensity=IntensityLight;
		SetParticle ();

		turnOn = false;
		turnOff = false;

		TorchLight.GetComponent<Light> ().enabled = false;
		StopFlame ();
	}

	void Update () 
	{
		if (turnOn) 
		{
			Debug.Log ("123");
			SetLight ();
			StartFlame ();
			turnOn = !turnOn;
		} 

		if (turnOff) 
		{
			Debug.Log ("456");
			SetLight ();
			StopFlame ();
			turnOff = !turnOff;
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

	public void SetTurnOn(){
		turnOn = !turnOn;
	}

	public void SetTurnOff(){
		turnOff = !turnOff;
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