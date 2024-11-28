using UnityEngine;
using System.Collections;

public class RotateTowards : MonoBehaviour {

	public GameObject victim;
	Transform target;
	public float speed;
	public float thrust;
	Rigidbody rb;

	void Start(){
		target = victim.GetComponent<Transform> ();
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		Vector3 targetDir = target.position - transform.position;


		float step = speed * Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
		Debug.DrawRay(transform.position, newDir, Color.red);

		transform.rotation = Quaternion.LookRotation(newDir);
		rb.AddRelativeForce(Vector3.forward * thrust);
	}
}
