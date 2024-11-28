using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TouchControlsKit;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jump;
	public float coinTarget;


	public GameObject[] endprops;
	public GameObject[] controller;
	public GameObject[] score;
	public GameObject startparticle;
	public GameObject CoinWarningPanel;
	public GameObject CoinWarning;

	public float count = 0;
	public float count2 = 0;
	public float count3 = 0;
	float maxRange;
	RaycastHit hit;

	AudioSource pickup;
	AudioSource death;
	AudioSource backdround;
	AudioSource spawn;

	private float nextfire;
	private float firerate = 0.05f;

	private MeshRenderer playermesh;
	private MeshRenderer coinmesh;
	private Rigidbody rb;

	private int levelvar;
	private int slevelvar;
	private int leveloffset;
	private float highscore;
	private float verticalAdd;

	float h;
	float axis;
	float v;
	float vS;

	bool isJump;


	void Awake() {
		Application.targetFrameRate = 70;
	}


	void Start()
	{

		Text WarningText = CoinWarning.GetComponent<Text>();
		WarningText.text = "Collect at Least " + coinTarget + " Coins";

		startparticle.SetActive (true);
		maxRange = transform.lossyScale.x / 1.6f;


		endprops = GameObject.FindGameObjectsWithTag("EndMenu");
		controller = GameObject.FindGameObjectsWithTag("MobileControls");

		foreach (GameObject respawn in endprops) {
			respawn.SetActive(false);
		}

		CoinWarningPanel.SetActive(false);

		score = GameObject.FindGameObjectsWithTag("Points");

		foreach (GameObject scorer in score) {
			Text pointer = scorer.GetComponent<Text>();
			pointer.text = "0/" + coinTarget;
		}


		rb = GetComponent<Rigidbody>();
		playermesh = GetComponent<MeshRenderer>();
		AudioSource[] audios = GetComponents<AudioSource>();
		pickup = audios[0];
		death = audios[1];
		backdround = audios [2];
		spawn = audios [3];

		playermesh.enabled = false;
		rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
		StartCoroutine(SetInactive(startparticle, 2));
	}

	void FixedUpdate()	{


		if (SystemInfo.deviceType == DeviceType.Desktop) 
		{
		h = 0.0f;
		h = Input.GetAxis ("Horizontal");
		vS= Input.GetAxis ("Vertical");
		isJump = Input.GetButton ("Fire1");
		} 

		else 
		
		{
			h = TCKInput.GetAxis( "Joystick0", EAxisType.Horizontal );
			vS= TCKInput.GetAxis( "Joystick0", EAxisType.Vertical );
			isJump = TCKInput.GetAction( "JumpButton0", EActionEvent.Down );
		}

		v = 0.0f;

		Vector3 transformPlus = new Vector3 (transform.position.x + (maxRange/4), transform.position.y, transform.position.z );
		Vector3 transformMinus = new Vector3 (transform.position.x - (maxRange/4), transform.position.y, transform.position.z );

		Vector3 ground1 = new Vector3 (transform.position.x, transform.position.y - 10f, transform.position.z );
		Vector3 ground11 = new Vector3 (transform.position.x + (maxRange/4), transform.position.y - 10f, transform.position.z );
		Vector3 ground12 = new Vector3 (transform.position.x - (maxRange/4), transform.position.y - 10f, transform.position.z );
		Vector3 ground2 = new Vector3 (transform.position.x - 10f, transform.position.y - 10f, transform.position.z );
		Vector3 ground3 = new Vector3 (transform.position.x + 10f, transform.position.y - 10f, transform.position.z );

		Vector3 ground4 = new Vector3 (transform.position.x, transform.position.y + 10f, transform.position.z );
		Vector3 ground5 = new Vector3 (transform.position.x + 10f, transform.position.y + 10f, transform.position.z );
		Vector3 ground6 = new Vector3 (transform.position.x - 10f, transform.position.y + 10f, transform.position.z );

		Vector3 ground7 = new Vector3 (transform.position.x - 10f, transform.position.y, transform.position.z );
		Vector3 ground8 = new Vector3 (transform.position.x + 10f, transform.position.y, transform.position.z );


		verticalAdd = rb.velocity.y;
		if (verticalAdd <= 0)
			verticalAdd = 0;


		if(Physics.Raycast(transform.position, (ground1 - transform.position), out hit, maxRange))
		{
			if(hit.collider != null && playermesh.enabled == true && hit.collider.tag != "Coin" && hit.collider.tag != "Taken" && hit.collider.tag != "ToggleWarning" && hit.collider.tag != "Won")
			{
				
				if (isJump && Time.time > nextfire) {
					nextfire = Time.time + firerate;
					v = jump;
					rb.velocity = new Vector3(rb.velocity.x, verticalAdd + v + vS, 0);

				}

			}
		}


		else if(Physics.Raycast(transformPlus, (ground11 - transformPlus), out hit, maxRange))
		{

			if(hit.collider != null && playermesh.enabled == true && hit.collider.tag != "Coin" && hit.collider.tag != "Taken" && hit.collider.tag != "ToggleWarning" && hit.collider.tag != "Won")
			{

				if (isJump && Time.time > nextfire) {
					nextfire = Time.time + firerate;
					v = jump;
					rb.velocity = new Vector3(rb.velocity.x, verticalAdd + v, 0);
				}

			}

		}


		else if(Physics.Raycast(transformMinus, (ground12 - transformMinus), out hit, maxRange))
		{

			if(hit.collider != null && playermesh.enabled == true && hit.collider.tag != "Coin" && hit.collider.tag != "Taken" && hit.collider.tag != "ToggleWarning" && hit.collider.tag != "Won")
			{

				if (isJump && Time.time > nextfire) {
					nextfire = Time.time + firerate;
					v = jump;
					rb.velocity = new Vector3(rb.velocity.x, verticalAdd + v, 0);
				}

			}

		}


		else if(Physics.Raycast(transform.position, (ground2 - transform.position), out hit, maxRange))
		{

			if(hit.collider != null && playermesh.enabled == true && hit.collider.tag != "Coin" && hit.collider.tag != "Taken" && hit.collider.tag != "ToggleWarning" && hit.collider.tag != "Won")
			{
				
				if (isJump && Time.time > nextfire) {
					nextfire = Time.time + firerate;
					v = jump/2;
					rb.velocity = new Vector3(rb.velocity.x, verticalAdd + v, 0);
				}

			}

		}

		else if(Physics.Raycast(transform.position, (ground3 - transform.position), out hit, maxRange))
		{

			if(hit.collider != null && playermesh.enabled == true && hit.collider.tag != "Coin" && hit.collider.tag != "Taken" && hit.collider.tag != "ToggleWarning" && hit.collider.tag != "Won")
			{


				if (isJump && Time.time > nextfire) {
					nextfire = Time.time + firerate;
					v = jump/2;
					rb.velocity = new Vector3(rb.velocity.x, verticalAdd + v, 0);
				}

			}

		}







		if(Physics.Raycast(transform.position, (ground1 - transform.position), out hit, maxRange) && hit.collider != null && playermesh.enabled == true && hit.collider.tag == "Death")
		{
					backdround.Stop ();
					death.Play();
					playermesh.enabled = false;
					rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
					foreach (GameObject respawn in endprops) {
						respawn.SetActive(true);
					}
				
		}


		else if(Physics.Raycast(transform.position, (ground2 - transform.position), out hit, maxRange) && hit.collider != null && playermesh.enabled == true && hit.collider.tag == "Death")
		{
			backdround.Stop ();
			death.Play();
			playermesh.enabled = false;
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			foreach (GameObject respawn in endprops) {
				respawn.SetActive(true);
			}

		}

		else if(Physics.Raycast(transform.position, (ground3 - transform.position), out hit, maxRange) && hit.collider != null && playermesh.enabled == true && hit.collider.tag == "Death")
		{
			backdround.Stop ();
			death.Play();
			playermesh.enabled = false;
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			foreach (GameObject respawn in endprops) {
				respawn.SetActive(true);
			}

		}

		else if(Physics.Raycast(transform.position, (ground4 - transform.position), out hit, maxRange) && hit.collider != null && playermesh.enabled == true && hit.collider.tag == "Death")
		{
			backdround.Stop ();
			death.Play();
			playermesh.enabled = false;
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			foreach (GameObject respawn in endprops) {
				respawn.SetActive(true);
			}

		}

		else if(Physics.Raycast(transform.position, (ground5 - transform.position), out hit, maxRange) && hit.collider != null && playermesh.enabled == true && hit.collider.tag == "Death")
		{
			backdround.Stop ();
			death.Play();
			playermesh.enabled = false;
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			foreach (GameObject respawn in endprops) {
				respawn.SetActive(true);
			}

		}

		else if(Physics.Raycast(transform.position, (ground6 - transform.position), out hit, maxRange) && hit.collider != null && playermesh.enabled == true && hit.collider.tag == "Death")
		{
			backdround.Stop ();
			death.Play();
			playermesh.enabled = false;
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			foreach (GameObject respawn in endprops) {
				respawn.SetActive(true);
			}

		}

		else if(Physics.Raycast(transform.position, (ground7 - transform.position), out hit, maxRange) && hit.collider != null && playermesh.enabled == true && hit.collider.tag == "Death")
		{
			backdround.Stop ();
			death.Play();
			playermesh.enabled = false;
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			foreach (GameObject respawn in endprops) {
				respawn.SetActive(true);
			}

		}

		else if(Physics.Raycast(transform.position, (ground8 - transform.position), out hit, maxRange) && hit.collider != null && playermesh.enabled == true && hit.collider.tag == "Death")
		{
			backdround.Stop ();
			death.Play();
			playermesh.enabled = false;
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			foreach (GameObject respawn in endprops) {
				respawn.SetActive(true);
			}

		}






		if (h == 0 && rb.velocity.magnitude < 2)
			h = -rb.velocity.x / 20;
		else
			h = h / (Mathf.Sqrt(rb.velocity.magnitude) + 0.3f);

		vS = vS / 5;

		Vector3 movement = new Vector3 (h, vS, 0.0f);
		rb.AddForce (movement * speed);




	}

	void OnTriggerEnter (Collider other) {
		
		if(other.gameObject.tag == "Coin" )
		{
			pickup.Play();
			coinmesh = other.gameObject.GetComponent<MeshRenderer>();
			coinmesh.enabled = false;
			other.gameObject.tag = "Taken";
			count = count + 0.5f;
			count2 = count * 5;
			count3 = count * 4;

			foreach (GameObject scorer in score) {
				Text pointer = scorer.GetComponent<Text>();
				pointer.text = "" + count + "/" + coinTarget;
			}

		}

		else if(other.gameObject.tag == "Death" )
		{
			death.Play();
			playermesh.enabled = false;
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;

			//foreach (GameObject control in controller) {
			//	control.SetActive(false);
			//}

			foreach (GameObject respawn in endprops) {
				respawn.SetActive(true);
			}


		}



		else if(other.gameObject.tag == "Won" )
		{

			if (count < coinTarget) {
				CoinWarningPanel.SetActive (true);
			} 

			else {
				playermesh.enabled = false;
				slevelvar = PlayerPrefs.GetInt ("savedLevel");

				Scene CurrentScene = SceneManager.GetActiveScene ();

				if ((count != (count2/5)) || (count != (count3/4))) {
					count = 3;					
				}

				leveloffset = CurrentScene.buildIndex;
				int BuildOffset = PlayerPrefs.GetInt ("LevelOffset");

				levelvar = leveloffset - BuildOffset;

				int clientID = PlayerPrefs.GetInt("clientID");

				int nextlevel = levelvar + 1;
				string leveltempstring1 = (clientID + nextlevel + "00");
				string Hash1 = Md5Sum (leveltempstring1);

				if(PlayerPrefs.GetFloat ("High" + levelvar) == 0)
					{
					PlayerPrefs.SetInt ("savedLevel", nextlevel);
						PlayerPrefs.SetString("Hash1", Hash1);
					}

				string tempstring1 = (clientID + count + levelvar + "00");
				string CountHash = Md5Sum (tempstring1);

				PlayerPrefs.SetFloat ("High" + levelvar, count);
				PlayerPrefs.SetString ("HighH" + levelvar, CountHash);
			
				float i, LevelTempScore, temp=0, ZeroFloat=0;
				string tempstring2, LevelHash, LevelTempHash, newtempstring1, newtemphash1;

				for(i = 1; i <= slevelvar; i++) {
					LevelTempScore = PlayerPrefs.GetFloat ("High" + i);
					LevelHash = PlayerPrefs.GetString ("HighH" + i);
					tempstring2 = (clientID + LevelTempScore + i + "00");
					LevelTempHash = Md5Sum (tempstring2);

					if (LevelHash == LevelTempHash) {
						temp = temp + LevelTempScore;
					} else {
						PlayerPrefs.SetFloat ("High" + i, ZeroFloat);
						newtempstring1 = (clientID + ZeroFloat + i + "00");
						newtemphash1 = Md5Sum (newtempstring1);
						PlayerPrefs.SetString ("HighH" + i, newtemphash1);
					}
				}

				string tempstring3 = (clientID + temp + "00");
				string Hash2 = Md5Sum (tempstring3);

				PlayerPrefs.SetFloat ("HighScore", temp);
				PlayerPrefs.SetString("Hash2", Hash2);

				PlayerPrefs.Save ();
				Vector3 gravity = new Vector3 (0, -6, 0);
				Physics.gravity = gravity;
				SceneManager.LoadSceneAsync ("Advert", LoadSceneMode.Single);
			}
		}

		else if(other.gameObject.tag == "ToggleWarning" )
		{
			if(CoinWarningPanel.activeSelf)
			CoinWarningPanel.SetActive (false);
		}




	}



	IEnumerator SetInactive(GameObject victim, int time) {
		float sound_delay = 1.5f;
		yield return new WaitForSeconds (sound_delay);
		spawn.Play ();
		playermesh.enabled = true;
		yield return new WaitForSeconds (time - sound_delay);
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		victim.SetActive (false);
		yield return null;
	}


	public  string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}


}
