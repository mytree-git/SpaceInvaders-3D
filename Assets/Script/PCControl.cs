using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCControl : MonoBehaviour {

	/*
	This class is designed to control the behaviour of the Player and the fireball. The player moves based on 
	the inputs from the user and the firing of the fireball weapon is also handled here.
	*/
	private Vector3 startPos;
	private Quaternion startRot;
	private bool visible; //for flashing
	private Animator anim;
	private Collider coll;
	private SkinnedMeshRenderer[] renderers;
	private static int animStateHashCast; //short name hash of the "Cast" animation state
	private static int animStateHashDeath;
	private bool hasFired; //set to true when player fires, false when the transition ends

	public float spawnGrace; //invincibility upon respawning
	public float fireballHeightOffset; 
	public float fireballDelay; //in seconds after the fire button is pressed. Tweak to match animation

	public GameObject fireball;
	private GameMgr gamemgr;

	private enum PlayerState {
		INVALID = -1,
		NONE = 0,
		SPAWNING,
		ALIVE,
		DYING
	}
	private PlayerState state;
	private PlayerState State {
		get { return state; }
		set {
			switch (value) {
			case PlayerState.DYING:
				{
					anim.SetTrigger ("Death");
					coll.enabled = false;
					state = PlayerState.DYING;
					break;
				}
			case PlayerState.SPAWNING:
				{
					if (state == PlayerState.SPAWNING)
						break;
					gamemgr.ReduceLives ();
					transform.position = startPos;
					transform.rotation = startRot;
					Invoke ("EndInvFrames", spawnGrace);
					state = PlayerState.SPAWNING;
					break;
				}
			case PlayerState.ALIVE:
				{
					visible = true;
					UpdateVisibility ();
					coll.enabled = true;
					state = PlayerState.ALIVE;
					break;
				}
			default:
				{
					state = value;
					break;
				}
			}
		}
	}

	private void EndInvFrames() {
		State = PlayerState.ALIVE;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (anim == null)
			Debug.LogError (string.Format("{0}: couldn't find an Animator", this.GetType ().ToString ()));
		coll = GetComponent<CapsuleCollider> ();
		if (coll == null)
			Debug.LogError (string.Format("{0}: couldn't find a Collider", this.GetType ().ToString ()));
		gamemgr = GameObject.Find("GLOBAL").GetComponent<GameMgr> ();
		if (gamemgr == null)
			Debug.LogError (string.Format("{0}: couldn't find a GameMgr", this.GetType ().ToString ()));
		renderers = GetComponentsInChildren<SkinnedMeshRenderer> ();
		animStateHashCast = Animator.StringToHash ("Cast");
		animStateHashDeath = Animator.StringToHash ("Death");
		startPos = transform.position;
		startRot = transform.rotation;
		hasFired = false;
		State = PlayerState.ALIVE;
	}
	
	void FixedUpdate() {
		HandleInputs ();
		ListenForRespawn ();
	}

	void Update() {
		HandleFlashing ();
	}


	// Updates player behaviour based on player input.
	private void HandleInputs() {
		anim.SetBool ("walkLeft", Input.GetAxisRaw ("Horizontal") < -0.5);
		anim.SetBool ("walkRight", Input.GetAxisRaw ("Horizontal") > 0.5);
		if (Input.GetButton ("Fire3") || Input.GetButton("Fire2"))
			anim.SetTrigger ("Attack");
		else
			anim.ResetTrigger ("Attack");
		
		if (hasFired && !anim.IsInTransition (0)) {
			hasFired = false;
		}
		if (!hasFired && anim.IsInTransition(0) &&
				anim.GetNextAnimatorStateInfo (0).shortNameHash == animStateHashCast) {
			Invoke ("Fire", fireballDelay);
			hasFired = true;
		}
	}

	//Spawns a fireball just above the player
	private void Fire() {
		Instantiate (fireball, transform.position + Vector3.up * fireballHeightOffset, Quaternion.identity);
		playerShoot("Player");

	}

	public void playerShoot(string gameObjectName){
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

	void OnCollisionEnter(Collision collision) {
		GameObject go = collision.gameObject;
		if (go.GetComponent<IsAProjectile> () != null && go.GetComponent<IsAEnemyOwned> () != null) {
			State = PlayerState.DYING;
		}
	}

	private void HandleFlashing() {
		if (State != PlayerState.SPAWNING)
			return;

		visible = !visible;
		UpdateVisibility ();
	}

	private void ListenForRespawn() {
		if (anim.GetCurrentAnimatorStateInfo (0).shortNameHash == animStateHashDeath &&
		    anim.IsInTransition (0)) {
			State = PlayerState.SPAWNING;
		}
	}

	private void UpdateVisibility() {
		foreach (SkinnedMeshRenderer r in renderers) {
			r.enabled = visible;
		}
	}
}
