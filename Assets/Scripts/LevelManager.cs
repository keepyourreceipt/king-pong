using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject cup;
	private GameObject[] cupStartingPositions;

	List<string> scoredCupPositions = new List<string>();

	private Ball ball;
	private InGameCanvas inGameCanvas;

	[HideInInspector]
	public enum GameState {
		Ready,
		InPlay,
		HitTable,
		DidScore
	}

	[HideInInspector]
	public GameState currentGameState;

	/***************************************************
	*** Setup table and get required objects
	****************************************************/
	void Start () {				
		if ( cup == null ) {
			Debug.LogError( "Please load a prefab cup into Level Manager" );
		}

		cupStartingPositions = GameObject.FindGameObjectsWithTag( "Cup Position" );
		ball = (Ball) GameObject.Find("Ball").GetComponent(typeof(Ball));
		inGameCanvas = (InGameCanvas) GameObject.Find("Canvas").GetComponent(typeof(InGameCanvas));

		PlaceCups();		
		currentGameState = GameState.Ready;

	}

	/***************************************************
	*** Place a cup at all cup positions
	****************************************************/
	void PlaceCups() {			
			
		GameObject[] positions = GameObject.FindGameObjectsWithTag( "Cup Position" );
		foreach( GameObject position in positions ) {
			if ( ! scoredCupPositions.Contains( position.name ) ) {
				Instantiate( cup, position.transform.position, Quaternion.identity, position.transform );
			}
		}	
	}

	void RackCups() {		
		scoredCupPositions.Clear();
		scoredCupPositions.Add("Cup Position (4)");
		scoredCupPositions.Add("Cup Position (6)");	
	}


	/***************************************************
	*** Remove all cups in the scene
	****************************************************/
	void RemoveCups() {		
		GameObject[] cups = GameObject.FindGameObjectsWithTag( "Cup" );
		foreach ( GameObject cup in cups ) {
			Destroy( cup );
		}
	}

	/***************************************************
	*** Reset table - no score
	****************************************************/
	public void ResetTable() {			
		RemoveCups();
		inGameCanvas.ClearScoreMessage();
		ball.ResetBall();	
		PlaceCups();
		currentGameState = GameState.Ready;
	}

	/***************************************************
	*** Show score HUD element and resrt table
	****************************************************/
	public void PlayerHasScored( string cupPositionName ) {
		currentGameState = GameState.DidScore;
		ball.StopBall();
		inGameCanvas.DisplayScoreMessage();
		StartCoroutine( ClearScoredCup( cupPositionName ) );
	}

	IEnumerator ClearScoredCup( string cupPositionName ) {
		yield return new WaitForSeconds(1f);
		RackCups();
		ResetTable();
	}

}
