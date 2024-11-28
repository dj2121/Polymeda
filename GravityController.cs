using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

	public int mass;
	public int range;
	public GameObject sun;
	public GameObject[] pawns;

	Vector3 MyPos;
	Vector3 pawnPos;
	Vector3 direction;
	Vector3 force;


	void FixedUpdate () {
		foreach (GameObject pawn in pawns) {
			pawnPos = pawn.transform.position;
			MyPos = sun.transform.position;
			direction = MyPos - pawnPos;
			if(direction.magnitude > range)
				direction = direction / (direction.magnitude * direction.magnitude * direction.magnitude);
			else
				direction = direction / (direction.magnitude * direction.magnitude);
			force = direction * mass;
			pawn.GetComponent<Rigidbody> ().AddForce (force);
		}
	}
}
