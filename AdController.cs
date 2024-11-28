using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GoogleMobileAds.Api;

public class AdController : MonoBehaviour {

	public GameObject WaitTextPanel;
	public Text AdText;
	int noInternet = 0;

	void Start () {
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			noInternet = 1;
			AdText.text = "Unable to load Advert due to bad connection." + "\n" +
				"Our development efforts are supported by Adverts.";
		}
		StartCoroutine (AdLoad ());
	}
	
	IEnumerator AdLoad()
	{
		if (noInternet == 0) {
			string adUnitId = "ca-app-pub-3948482735254543/7485906969";
			InterstitialAd interstitial = new InterstitialAd (adUnitId);
			AdRequest request = new AdRequest.Builder ().Build ();
			interstitial.LoadAd (request);
			yield return new WaitForSeconds (5);
			interstitial.Show ();
			yield return new WaitForSeconds (2);
			WaitTextPanel.SetActive (false);
			yield return new WaitForSeconds (30);
			interstitial.Destroy ();			
		} else {
			yield return new WaitForSeconds (5);
			WaitTextPanel.SetActive (false);
		}
		yield return null;
	}
}
