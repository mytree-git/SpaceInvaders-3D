using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour {

	/*
	This class is determines on how the player and aliens react on the attack of the fireball. If an alien gets hit, score adds up by 10 and if a UFO (bonus alien) gets hit, score adds up by 75.
	*/
	private int addScore = 10;
	private int bigScore = 75;
	private GameObject globalgo;

	// Use this for initialization
	void Start () {
		globalgo = GameObject.Find ("GLOBAL");
		if (globalgo == null)
			Debug.LogError ("Fireball couldn't find GLOBAL.");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//plays sound when an alien is hit by a fireball
	public void enemyExplosion(string gameObjectName){
		GameObject gm = GameObject.Find(gameObjectName);
		if (gm != null)
		{
			AudioSource asource = gm.GetComponent<AudioSource> ();
			if (asource == null) {
				asource = gm.AddComponent<AudioSource> ();
			}
			asource.Play ();
		}
	}

	//destroys the alien when hit by a fireball
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponent<UFOMovement> () != null) {
			globalgo.GetComponent<GameMgr> ().AddToScore (bigScore);
			Destroy (collision.gameObject);
			//enemyExplosion ();
		} else if (collision.gameObject.GetComponent<IsACharacter> () != null && collision.gameObject.GetComponent<IsAEnemyOwned> () != null) {
			globalgo.GetComponent<GameMgr> ().AddToScore (addScore);
			Destroy (collision.gameObject);
		}
		Destroy (gameObject);
	}
}
