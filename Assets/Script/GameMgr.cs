using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameMgr : MonoBehaviour {

	/*
	 * This class is designed to keep track of the overall Game State. It keeps track of the score and life 
	   of the player. 
	 */
	private int playerScore;
	private int playerLives; //value to keep track of number of lives for player
	
	private UIMgr uiMgr;
	
	public Text scoreText, goScoreText;
	public Text LivesText; //text to display those lives
	public GameObject GOPanel;

	public void AddToScore(int score){
		playerScore += score;
	}

	void Awake(){
		playerScore = 0;
		playerLives = 3; //initalize playerLives
	}

	void Update(){
		scoreText.text = goScoreText.text = "SCORE: " + playerScore;
		LivesText.text = "Lives: " + playerLives; //sets text to show int value for player lives
	}

	public void ReduceLives(){
		playerLives--;
		if (playerLives <= 0) {
			AdvanceState ();
		}
	}
  
	public enum GameState {
		INVALID = -1,
		NONE = 0,

		TITLE_MENUS,
		IN_GAME,
		GAME_OVER
	}

	private GameState state;
	public GameState State {
		private set { state = value; }
		get { return state; }
	}

	void Start() {
		uiMgr = GetComponent<UIMgr> ();
		if (uiMgr == null)
			Debug.LogError ("GameMgr couldn't find UIMgr.");
		AdvanceState ();
	}


	public void AdvanceState() { 
		switch (State) {
		case GameState.INVALID:
			{
				Debug.LogError ("Attempted to advance game state while current state was " + State);
				break;
			}
		case GameState.GAME_OVER:
			{
				SceneManager.LoadScene ("Global");
				break;
			}
		case GameState.NONE:
			{
				Time.timeScale = 0;
				uiMgr.GoToTitle ();
				state = GameState.TITLE_MENUS;
				break;
			}
		case GameState.TITLE_MENUS:
			{
				Time.timeScale = 1;
				uiMgr.GoToInGame ();
				state = GameState.IN_GAME;
				break;
			}
		case GameState.IN_GAME:
			{
				Time.timeScale = 0;
				uiMgr.GoToGameOver ();
				state = GameState.GAME_OVER;
				break;
			}
		default:
			{
				Debug.LogError ("GameMgr.AdvanceState() encountered unhandled current state: " + State);
				break;
			}
		}
	}

	public void GoToLevel(int level) {
		if (State != GameState.TITLE_MENUS) {
			Debug.LogError ("Attempted to load a level from somewhere other than the title menus.");
			return;
		}

		AdvanceState ();
		
	}
}
