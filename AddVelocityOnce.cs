using UnityEngine;
using System.Collections;

public class AddVelocityOnce : MonoBehaviour {

	public float VMagnitudeX;
	public float VMagnitudeY;
	private Rigidbody rb;


	void Start () {
		rb = GetComponent<Rigidbody>();	
		Vector3 Velocity = new Vector3(VMagnitudeX, VMagnitudeY,0);
		rb.velocity = Velocity;
	}
}
