using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour {
	
	/*
	This class is designed to control the spawning and movement of the UFO object. It is a bonus alien object 
	that earns extra score to the player.
	*/

	public int rot = 1; //changes direction of enemies
	private float movementSpeed = 3.0f; //controls the speed of the enemies

	void UFOMovementUpdate() {

		transform.Translate (Vector3.left * rot * movementSpeed * Time.deltaTime, Space.World); //moves enemies based on direction and speed of enemies

		if (transform.position.x <= -30 && rot == 1) {
			rot = -1;
			transform.rotation = new Quaternion (0.5f, 0.5f, -0.5f, 0.5f);
		} else if (transform.position.x >= 30 && rot == -1) {
			rot = 1;
			transform.rotation = new Quaternion (-0.5f, 0.5f, -0.5f, -0.5f);
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		UFOMovementUpdate ();
	}

	void OnCollisionEnter(Collision collision) {
		GameObject go = collision.gameObject;
		if (go.GetComponent<IsAProjectile> () != null && go.GetComponent<IsAPlayerOwned> () != null) {
			Destroy (gameObject);
		}
	}
}
