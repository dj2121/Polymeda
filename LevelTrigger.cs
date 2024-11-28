using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour {

	public int ButtonLevel;
	Button myButton;
	private int clevel;
	private int LevelOffset;

	void Start () {
		clevel = PlayerPrefs.GetInt("savedLevel");
		Scene CurrentScene = SceneManager.GetActiveScene ();
		LevelOffset = CurrentScene.buildIndex;

		myButton = GetComponent<Button>();

		if (ButtonLevel > clevel)
			myButton.interactable = false;
			
	
	}
	
	public void OnClick () {
		SceneManager.LoadSceneAsync ((ButtonLevel + LevelOffset), LoadSceneMode.Single);
	}
}
