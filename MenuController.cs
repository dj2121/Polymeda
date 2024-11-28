using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public GameObject PauseMenu;
	void Start () {
		HideMenu ();
	}


	public void GotoMenu() {
		PauseMenu.SetActive (true);
		Time.timeScale = 0.00001f;
	}

	public void HideMenu() {
		PauseMenu.SetActive (false);
		Time.timeScale = 1.0f;    
	}

	public void ExittoMain() {
		SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
	}
}
