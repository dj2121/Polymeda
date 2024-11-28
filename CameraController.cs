using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject[] Player;
	private float dampTime;
	private Vector3 velocity = Vector3.zero;
	private Vector3 offset;
	private Vector3 playerx;
	private GameObject ball;
	
	
	// Use this for initialization
	void Start () {

		dampTime = 0.4f;
		offset = transform.position;

		Player = GameObject.FindGameObjectsWithTag("Ball");
		
		foreach (GameObject hit in Player) {
			ball = hit;
		}

		
	}
	
	// Update is called once per frame
	void Update () {

		playerx = new Vector3(ball.transform.position.x, ball.transform.position.y, 0.0f);
		Vector3 destination = playerx + offset;
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

		
	}
}
