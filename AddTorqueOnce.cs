using UnityEngine;
using System.Collections;

public class AddTorqueOnce : MonoBehaviour {

	public float TMagnitude;
	private Rigidbody rb;


	void Start () {
		rb = GetComponent<Rigidbody>();	
		Vector3 torque = new Vector3(0, 0,TMagnitude);
		rb.maxAngularVelocity = TMagnitude;
		rb.angularVelocity = torque;
	}

}
