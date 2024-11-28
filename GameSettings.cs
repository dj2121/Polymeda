using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameSettings : MonoBehaviour {

	public GameObject NoInternetPanel;
	private int levelvar;
	private float highscore;
	private int LevelOffset;
	private int clientID;

	private string Hash1;
	private string Hash2;

	private string temphash1;
	private string temphash2;

	string tempstring1;
	string tempstring2;
	int noInternet=0;



	// Use this for initialization
	void Start () {


		levelvar = PlayerPrefs.GetInt("savedLevel");
		highscore = PlayerPrefs.GetFloat ("HighScore");
		LevelOffset = PlayerPrefs.GetInt ("LevelOffset");
		clientID = PlayerPrefs.GetInt("clientID");
		Hash1 = PlayerPrefs.GetString("Hash1");
		Hash2 = PlayerPrefs.GetString("Hash2");


		if (highscore == 0) {

			PlayerPrefs.SetInt ("savedLevel", 1);
			PlayerPrefs.SetFloat ("HighScore", 0.0f);

			Scene CurrentScene = SceneManager.GetActiveScene ();
			LevelOffset = CurrentScene.buildIndex;

			PlayerPrefs.SetInt ("LevelOffset", LevelOffset);

			clientID = Random.Range(0, 100000000);
			PlayerPrefs.SetInt("clientID", clientID);

			levelvar = PlayerPrefs.GetInt ("savedLevel");
			highscore = PlayerPrefs.GetFloat ("HighScore");
			clientID = PlayerPrefs.GetInt("clientID");

			tempstring1 = (clientID + levelvar + "00");
			tempstring2 = (clientID + highscore + "00");
			Hash1 = Md5Sum (tempstring1);
			Hash2 = Md5Sum (tempstring2);

			PlayerPrefs.SetString("Hash1", Hash1);
			PlayerPrefs.SetString("Hash2", Hash2);

		}


		tempstring1 = (clientID + levelvar + "00");
		tempstring2 = (clientID + highscore + "00");

		temphash1 = Md5Sum (tempstring1);
		temphash2 = Md5Sum (tempstring2);

		if ((temphash1 != Hash1) || (temphash2 != Hash2)) {
			PlayerPrefs.SetInt ("savedLevel", 1);
			PlayerPrefs.SetFloat ("HighScore", 0.0f);

			Scene CurrentScene = SceneManager.GetActiveScene ();
			LevelOffset = CurrentScene.buildIndex;

			PlayerPrefs.SetInt ("LevelOffset", LevelOffset);

			clientID = Random.Range(0, 100000000);
			PlayerPrefs.SetInt("clientID", clientID);

			levelvar = PlayerPrefs.GetInt ("savedLevel");
			highscore = PlayerPrefs.GetFloat ("HighScore");
			clientID = PlayerPrefs.GetInt("clientID");

			tempstring1 = (clientID + levelvar + "00");
			tempstring2 = (clientID + highscore + "00");
			Hash1 = Md5Sum (tempstring1);
			Hash2 = Md5Sum (tempstring2);


			PlayerPrefs.SetString("Hash1", Hash1);
			PlayerPrefs.SetString("Hash2", Hash2);			
		}


			

		Text subtitle = GameObject.Find("High Score").GetComponent<Text>();
		subtitle.text = "Score: " + highscore;

		Text leveltext = GameObject.Find("Level Count").GetComponent<Text>();
		leveltext.text = "Scene " + levelvar;

		string appId = "ca-app-pub-3948482735254543~8193188917";
		MobileAds.Initialize(appId);
	
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

	public void ShowLeaderboard()
	{
		StartCoroutine (LeaderboardPreCheck());
	}

	IEnumerator LeaderboardPreCheck()
	{
		noInternet = 0;
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			noInternet = 1;
		}

		if (noInternet == 0) {
			SceneManager.LoadSceneAsync ("LeaderboardInit", LoadSceneMode.Single);
		} else {
			NoInternetPanel.SetActive (true);
			yield return new WaitForSeconds (5);
			NoInternetPanel.SetActive (false);
		}
		yield return null;
	}

}
