using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLogic : MonoBehaviour {

	/*
	 * This class is designed to destory the enemybullet when it collides with anything
	 * will check to see if bullet hits: shields or Player
	 * if shield - destory a part of shield where it was hit
	 * if player - reduce a life, respawn player 
	 */

	public GameObject enemyBullet;

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.GetComponent<IsAShield> () != null) {
			//destory one block per sheild
		} else if (collision.gameObject.GetComponent<IsACharacter> () != null 
			&& collision.gameObject.GetComponent<IsAPlayerOwned>() != null) {
			//check game manager for player lives,
			//reduce number of lives for player,
			//respawn player in middle of screen 
		}
		//playerDead ();
		Destroy (gameObject); //gameobject is destroyed when it collides with anything 
	}

	void playerDead(string gameObjectName){
		GameObject gm = GameObject.Find(gameObjectName);
		if (gm != null) {

			AudioSource asource = gm.GetComponent<AudioSource> ();
			if (asource == null) {

				asource = gm.AddComponent<AudioSource> ();
			}

			asource.Stop();
		}
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
