using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
	
	/*
	This class is designed to make aliens shoot 'enemy bullets' at a specific speed and specific time interval
	*/

	public GameObject enemyBullet;
	public float shootTime;
	public float timeCounter;
	public GameObject enemyBlock;
	private static float bulletOffset = 0.5f;

	void TimeCheck(){
		timeCounter = 0.0f;
		shootTime = Random.Range (3, 15);
	}

	void EnemyShootingUpdate(){
		timeCounter += Time.deltaTime;
		if (timeCounter >= shootTime) {
			if (enemyBlock == null) {
				Instantiate (enemyBullet, transform.position + Vector3.down * bulletOffset, Quaternion.identity);
				TimeCheck ();
			}
		}

	}

	// Use this for initialization
	void Start () {
		TimeCheck ();
	}
	
	// Update is called once per frame
	void Update () {
		EnemyShootingUpdate ();
	}
}
