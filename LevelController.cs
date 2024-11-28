using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {


	public GameObject LevelSelection;

	public void OnClick () {
		LevelSelection.SetActive (true);
	}
}
