using UnityEngine;
using System.Collections;

public class ChangeGravity : MonoBehaviour {

	public float gravityMagnitude;
	Vector3 gravity;

	void Awake () {
		gravity = new Vector3 (0, gravityMagnitude, 0);
		Physics.gravity = gravity;
	}
}
