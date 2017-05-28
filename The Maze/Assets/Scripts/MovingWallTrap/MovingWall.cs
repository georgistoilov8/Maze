using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MovingWall : MonoBehaviour {

	public enum disposal {Horizontal, Vertical};
	public disposal position;

	//distance
	private float distanceToTake;
	private float distancePerDeltaTime;

	//speed and time
	public float speedOfTranslate;
	public float timeUntilReopen;

	private float timer;

	//wall position
	private Vector3 newPositionHor; // Horizontal movement (the wall going to move horizontally)
	private Vector3 newPositionVer; // Vertical movement (the wall going to move vertically)
	private Vector3 oldPosition; 

	//booleans
	private bool toBlock;
	private bool startTimer;

	//GameObjects which I move or change
	private GameObject wallToMove;
	private GameObject sensor;
	private BoxCollider boxCollider;

	private GameObject player;
	private RigidbodyFirstPersonController playerScript; 
	private GameObject mainCamera;

	//Sound
	public AudioClip sensorSound;
	public AudioClip movingWallSound;

	private AudioSource sensorAudioSource;
	private AudioSource movingWallAudioSource;
	private float volLowRange;
	private float volHighRange;

	void Start () 
	{
		timer = timeUntilReopen;
		distanceToTake = 4f;

		wallToMove = gameObject.transform.GetChild (0).gameObject;
		newPositionVer = new Vector3 (wallToMove.transform.position.x - distanceToTake, wallToMove.transform.position.y, wallToMove.transform.position.z);
		newPositionHor = new Vector3 (wallToMove.transform.position.x, wallToMove.transform.position.y, wallToMove.transform.position.z - distanceToTake);
		oldPosition = wallToMove.transform.position;

		sensor = transform.gameObject;
		boxCollider = sensor.GetComponent<BoxCollider> ();

		startTimer = false;
		toBlock = false;

		player = GameObject.FindGameObjectWithTag ("Player");
		playerScript = player.GetComponent<RigidbodyFirstPersonController> ();
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		volLowRange = 0.5f;
		volHighRange = 1f;
		sensorAudioSource = GetComponent<AudioSource> ();
		movingWallAudioSource = wallToMove.GetComponent<AudioSource> ();
	}

	void FixedUpdate()
	{
		// Block the path
		if (toBlock) 
		{
			if (position.Equals (disposal.Horizontal)) 
			{
				//moving back wall Horizontally
				if (wallToMove.transform.position.z > newPositionHor.z) 
				{
					PlayWallMoveSound ();
					wallToMove.transform.Translate (Vector3.back * speedOfTranslate);
				} else {
					StopWallMoveSound ();
					startTimer = !startTimer;
					toBlock = !toBlock;
				}
			} else {
				//moving back wall Vertically
				if (wallToMove.transform.position.x > newPositionVer.x) 
				{
					PlayWallMoveSound ();
					wallToMove.transform.Translate(Vector3.back * speedOfTranslate);
				} else {
					StopWallMoveSound ();
					startTimer = !startTimer;
					toBlock = !toBlock;
				}
			}
		}

		//Move back
		if (startTimer) 
		{
			if (timer > 0) {
				timer -= Time.deltaTime;
			} else {
				if (position.Equals (disposal.Horizontal)) 
				{
					//moving forward wall Vertically
					if (wallToMove.transform.position.z <= oldPosition.z) 
					{
						PlayWallMoveSound ();
						wallToMove.transform.Translate (Vector3.forward * speedOfTranslate);
					} else {
						StopWallMoveSound ();
						startTimer = !startTimer;
						timer = timeUntilReopen;
						boxCollider.enabled = !boxCollider.enabled;
					}
				} else {
					//moving forward wall Vertically
					if (wallToMove.transform.position.x <= oldPosition.x) {
						PlayWallMoveSound ();
						wallToMove.transform.Translate (Vector3.forward * speedOfTranslate);
					} else {
						StopWallMoveSound ();
						startTimer = !startTimer;
						timer = timeUntilReopen;
						boxCollider.enabled = !boxCollider.enabled;
					}
				}
			}
		}
	}

	void PlayWallMoveSound()
	{
		if (!movingWallAudioSource.isPlaying) 
		{
			movingWallAudioSource.PlayOneShot (movingWallSound, 1F);
		}
	}

	void StopWallMoveSound()
	{
		if (movingWallAudioSource.isPlaying) 
		{
			movingWallAudioSource.Stop ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Player")) 
		{
			Debug.Log ("ENTER MOVING WALL TRAP");
			float randomVolume = Random.Range (volLowRange, volHighRange);
			sensorAudioSource.PlayOneShot (sensorSound, randomVolume);
			boxCollider.enabled = !boxCollider.enabled;
			toBlock = !toBlock;
		}
	}

}