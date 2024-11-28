using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AdContinue : MonoBehaviour {

	public void ExittoMain() {
		SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
	}
}
