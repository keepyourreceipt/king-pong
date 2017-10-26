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

	/***************************************************
	*** Adjust list items to manage position of cups
	*** added to the scene on PlaceCups call
	****************************************************/
	void RackCups( int numberOfCupsRemaining ) {		
		scoredCupPositions.Clear();

		if ( numberOfCupsRemaining == 4 ) {
			scoredCupPositions.Add("Cup Position (4)");
			scoredCupPositions.Add("Cup Position (6)");
		}

		if ( numberOfCupsRemaining == 3 ) {
			scoredCupPositions.Add("Cup Position (4)");
			scoredCupPositions.Add("Cup Position (5)");
			scoredCupPositions.Add("Cup Position (6)");
		}

		if ( numberOfCupsRemaining == 1 ) {
			scoredCupPositions.Add("Cup Position (2)");
			scoredCupPositions.Add("Cup Position (3)");
			scoredCupPositions.Add("Cup Position (4)");
			scoredCupPositions.Add("Cup Position (5)");
			scoredCupPositions.Add("Cup Position (6)");
		}
	
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
	*** Reset table
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
		StartCoroutine( ClearAndRackCups( cupPositionName ) );
	}

	/***************************************************
	*** Clear scored cup and rerack as needed
	****************************************************/
	IEnumerator ClearAndRackCups( string cupPositionName ) {
		yield return new WaitForSeconds(1f);
		var numberOfCupsRemaining = GameObject.FindGameObjectsWithTag( "Cup").Length - 1;
		if ( numberOfCupsRemaining == 4 ) {
			RackCups(4);
		} else if ( numberOfCupsRemaining == 3 ) {
			RackCups(3);
		} else if ( numberOfCupsRemaining == 1 ) {
			RackCups(1);
		} else {
			scoredCupPositions.Add( cupPositionName );
		}
		ResetTable();
	}

}
