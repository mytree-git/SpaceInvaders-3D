using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIMgr : MonoBehaviour {

	/*
	This class is designed to control the overall UI of the game. Starting from 'TitleScreen' to 'GameOver' state. When the game ends, UI components display the score and asks to continue to play the game and select a level
	*/
	public enum eUIMgrState
	{
		Invalid = -1,
		None = 0,
		TitleScreen,
		LevelSelect,
		HighScore,
		InGame,
		GameOver
	};

	public List<IsAUIPanel> panels;
	public eUIMgrState state;

	private GameMgr gameMgr;

	// Use this for initialization
	void Start () {
		gameMgr = GetComponent<GameMgr> ();
		if (gameMgr == null)
			Debug.LogError ("UIMgr couldn't find GameMgr.");
	}

	public void ShowCurrentPanel() 
	{
		foreach (IsAUIPanel iap in panels) 
		{
			if (iap.state == state) {
				iap.gameObject.SetActive (true);
			} else {
				iap.gameObject.SetActive (false);
			}

		}
	}

	private void GoTo(eUIMgrState destState) {
		state = destState;
		ShowCurrentPanel ();
	}

	public void GoToTitle() {
		GoTo (eUIMgrState.TitleScreen);
	}

	public void GoToLevelSelect() {
		GoTo (eUIMgrState.LevelSelect);
	}

	public void GoToHighScore() {
		GoTo (eUIMgrState.HighScore);
	}

	public void GoToInGame() {
		GoTo (eUIMgrState.InGame);
	}

	public void GoToGameOver() {
		GoTo (eUIMgrState.GameOver);
	}
}