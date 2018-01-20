using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	/*
	This class is designed to move the aliens left and right as well as closer to the player
	direction is based on x position of alien wave, aliens continue to move down regardless of where they are
	going to set onCollisionEnter script on sheilds to check for collisions w/ aliens to end game
	speed is set atm but should be changed based on number of aliens
	possibly every 10 aliens destoryed movementspeed += 0.05f
	*/

	public int dir = 1; //changes direction of aliens
	private float movementSpeed = 0.65f; //controls the speed of the aliens
	private GameMgr gamemgr;

	void EnemyMovementUpdate(){

		transform.Translate (Vector3.right * dir * movementSpeed * Time.deltaTime); //moves aliens based on direction and speed of aliens

		if (transform.position.x >= 3.25) {
			dir = -1; //if the alien wave changes direction based on x position and moves down 0.5f towards player
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
		} else if (transform.position.x <= -3.25) {
			dir = 1; //changes direction of aliens again based on pos of x, also moves aliens down towards player
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
		}
		if (gameObject.GetComponentsInChildren<IsACharacter> ().Length == 0 && gamemgr.State == GameMgr.GameState.IN_GAME) {
			gamemgr.AdvanceState ();
		}
	}

	// Use this for initialization
	void Start () {
		gamemgr = GameObject.Find ("GLOBAL").GetComponent<GameMgr> ();
	}
	
	// Update is called once per frame
	void Update () {
		EnemyMovementUpdate ();
	}
}
