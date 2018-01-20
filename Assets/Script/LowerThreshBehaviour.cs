using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerThreshBehaviour : MonoBehaviour {

	private GameMgr gamemgr;

	// Use this for initialization
	void Start () {
		gamemgr = GameObject.Find ("GLOBAL").GetComponent<GameMgr> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		GameObject go = collider.gameObject;
		if (go.GetComponent<IsACharacter> () != null && go.GetComponent<IsAEnemyOwned> () != null &&
				gamemgr.State == GameMgr.GameState.IN_GAME) {
			gamemgr.AdvanceState ();
		}
	}
}
