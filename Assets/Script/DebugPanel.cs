using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is attached to the DebugPanel in the scene
public class DebugPanel : MonoBehaviour {

	public Text timeDisp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PanelUpdate ();
	}

	private void PanelUpdate() {
		timeDisp.text = string.Format ("{0:N2}", Time.time);
	}
}
