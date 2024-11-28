using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LeaderboardRefresh : MonoBehaviour {

	public void ReloadLeaderboard()
	{
		SceneManager.LoadSceneAsync("Leaderboard", LoadSceneMode.Single);
	}
}
