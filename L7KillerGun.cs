using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L7KillerGun : MonoBehaviour {

	public int speed;
	public GameObject BulletSphere;
	public GameObject bulletspawn;
	public GameObject bulletDir;
	public GameObject Base;
	RaycastHit hit;
	Vector3 MyPos;



	void Update(){

		if (Physics.Raycast (bulletspawn.transform.position, (bulletDir.transform.position - bulletspawn.transform.position), out hit, 100)) {
			if (hit.collider != null && hit.collider.tag == "Ball") {
				StartCoroutine (Killer (BulletSphere));
			}

		}
	}


	public IEnumerator Killer(GameObject bullet) {
		Vector3 pos = bulletspawn.transform.position;
		GameObject Bullet;
		Bullet = Instantiate (bullet, pos, Quaternion.identity) as GameObject;
		Bullet.transform.rotation = Base.transform.rotation;
		Bullet.GetComponent<Rigidbody> ().AddRelativeForce (speed, 0, 0);
		yield return null;
	}

}
