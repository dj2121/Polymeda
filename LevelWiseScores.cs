using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelWiseScores : MonoBehaviour {

	public GameObject ScoreText;
	void Start () {

		string displaystring = "";
		string tempstring;

		int[] Scores = {6, 8, 5, 5, 7, 9, 5, 5, 6, 7, 9};

		int slevelvar = PlayerPrefs.GetInt ("savedLevel");
		int clientID = PlayerPrefs.GetInt("clientID");

		float i, LevelTempScore, temp=0, ZeroFloat=0;
		string tempstring2, LevelHash, LevelTempHash, newtempstring1, newtemphash1;

		ScoreText.GetComponent<Text> ().text = displaystring;

		for(i = 1; i <= slevelvar; i++) {
			LevelTempScore = PlayerPrefs.GetFloat ("High" + i);
			LevelHash = PlayerPrefs.GetString ("HighH" + i);
			tempstring2 = (clientID + LevelTempScore + i + "00");
			LevelTempHash = Md5Sum (tempstring2);

			if (LevelHash == LevelTempHash) {
				tempstring = ("Level " + i + " | Score: " + LevelTempScore + " Max Score: " + Scores [(int)i - 1] + "\n");
				displaystring = string.Concat (displaystring, tempstring);
			} else {
				tempstring = ("Level " + i + " | Score: " + "0" + " Max Score: " + Scores [(int)i - 1] + "\n");
				displaystring = string.Concat (displaystring, tempstring);
			}
		}
		ScoreText.GetComponent<Text> ().text = displaystring;
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
