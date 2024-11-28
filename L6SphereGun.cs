using UnityEngine;
using System.Collections;

public class L6SphereGun : MonoBehaviour {

	public GameObject bullet;
	public GameObject bulletspawn;

	void Start () {

		Vector3 MyPos = bulletspawn.transform.position;
		StartCoroutine (Killer (bullet, MyPos));
	}


	public IEnumerator Killer(GameObject bullet, Vector3 pos) {
		float delay = 1f;

		int i=1;
		for(i=1;i<30;i++) {
			yield return new WaitForSeconds (delay);
			Instantiate (bullet, pos, Quaternion.identity);
			yield return new WaitForSeconds (delay);
		}

		yield return null;
	}


}


