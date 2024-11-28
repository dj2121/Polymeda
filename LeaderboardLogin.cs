using UnityEngine;
using System.Collections;
using Facebook.Unity;
using UnityEngine.UI;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Facebook.MiniJSON;
using System;
using UnityEngine.SceneManagement;

public class LeaderboardLogin : MonoBehaviour {


	List<string> permissions = new List<string>() { "public_profile", "email", "user_friends" };


	void Awake()
	{
		FB.Init(InitCallback, null, null);
		DontDestroyOnLoad(this.gameObject);

	}

	void Start () {

		if (!FacebookAndPlayFabInfo.isLoggedOnPlayFab)
		{
			FB.LogInWithReadPermissions(permissions, LoginFacebookCallBack);
		}
		else
		{
			
		}
	
	}


	public void Login()
	{

		if (!FB.IsLoggedIn)
		{

			FB.LogInWithReadPermissions(permissions, LoginFacebookCallBack);
			SceneManager.LoadSceneAsync ("Leaderboard", LoadSceneMode.Single);


		}
		else
		{
			SceneManager.LoadSceneAsync ("Leaderboard", LoadSceneMode.Single);
		}
	}

	private void InitCallback()
	{
		if (FB.IsInitialized)
		{
			FB.ActivateApp();
		}
	}

	private void LoginFacebookCallBack(ILoginResult loginResult)
	{
		if (loginResult == null)
		{
			Debug.Log("Could not log in to facebook.");

			return;
		}

		if (string.IsNullOrEmpty(loginResult.Error) && !loginResult.Cancelled && !string.IsNullOrEmpty(loginResult.RawResult))
		{
			Debug.Log("Success while logging into Facebook.");
			SceneManager.LoadSceneAsync ("Leaderboard", LoadSceneMode.Single);


			if (!FacebookAndPlayFabInfo.isLoggedOnPlayFab)
			{           
				LoginWithFacebookRequest loginFacebookRequest = new LoginWithFacebookRequest()
				{
					TitleId = PlayFabSettings.TitleId,                  
					AccessToken = Facebook.Unity.AccessToken.CurrentAccessToken.TokenString, 
					CreateAccount = true
				};

				PlayFabClientAPI.LoginWithFacebook(loginFacebookRequest, PlayFabLoginSucessCallBack, PlayFabErrorCallBack);

			}
		}
		else
		{
			Debug.Log("Could not log in to facebook.");

		}

	}

	private void PlayFabErrorCallBack(PlayFabError playfabError)
	{
		Debug.Log(playfabError.ErrorMessage);
		Debug.Log(playfabError.ErrorDetails);
	}

	private void PlayFabLoginSucessCallBack(PlayFab.ClientModels.LoginResult playfabLoginResult)
	{
		Debug.Log("Success Login to PlayFab.");

		FacebookAndPlayFabInfo.userPlayFabId = playfabLoginResult.PlayFabId;
		FB.API("/me", HttpMethod.GET, CollectLoggedUserInfoCallback);

	}

	private void CollectLoggedUserInfoCallback(IGraphResult result)
	{
		if (result == null)
		{
			Debug.Log("Unable to collect user data on Facebook.");
			return;
		}

		if (string.IsNullOrEmpty(result.Error) && !result.Cancelled && !string.IsNullOrEmpty(result.RawResult))
		{
			Debug.Log("Success in collecting user account data on Facebook");

			try
			{
				Dictionary<string, object> dict = Json.Deserialize(result.RawResult) as Dictionary<string, object>;
				string userFacebookName = dict["name"] as string;
				string userFacebookId = dict["id"] as string;

				Debug.Log(dict.ToJson());
				UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest();
				request.DisplayName = userFacebookId;

				PlayFabClientAPI.UpdateUserTitleDisplayName(request, UpdateUserTitleDisplayNameSucessCallback, PlayFabErrorCallBack);

				FacebookAndPlayFabInfo.isLoggedOnPlayFab = true;
				FacebookAndPlayFabInfo.userFacebookId = userFacebookId;
				FacebookAndPlayFabInfo.userName = userFacebookName;
			}
			catch (KeyNotFoundException e)
			{
				Debug.Log("Unable to collect user data on Facebook. Error: " + e.Message);
			}
		}
		else
			Debug.Log("Unable to collect user data on Facebook.");
	}

	private void UpdateUserTitleDisplayNameSucessCallback(UpdateUserTitleDisplayNameResult result)
	{
		Debug.Log("The Display Name field of the user in PlayFab has been updated successfully.");

		SceneManager.LoadSceneAsync ("Leaderboard", LoadSceneMode.Single);
	}




}
