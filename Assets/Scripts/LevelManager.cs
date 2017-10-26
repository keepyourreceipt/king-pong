using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject cup;
	private GameObject[] cupStartingPositions;
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
			
		GameObject[] currentPositions = GameObject.FindGameObjectsWithTag( "Cup Position" );
		if ( currentPositions.Length == 5 ) {			
			foreach( GameObject position in currentPositions ) {
				// Destroy( position );
				// TODO: Rr-rack with the appropriate number of cups
			}
		} else if ( currentPositions.Length != 5 ) {
			GameObject[] positions = GameObject.FindGameObjectsWithTag( "Cup Position" );
			foreach( GameObject position in positions ) {
				Instantiate( cup, position.transform.position, Quaternion.identity, position.transform );
			}	
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
	*** Reset table - no score
	****************************************************/
	public void ResetTable() {			
		RemoveCups();
		ball.ResetBall();	
		PlaceCups();
		currentGameState = GameState.Ready;
	}


	/***************************************************
	*** Reset table - with score
	****************************************************/
	public void ResetTable( string cupPositionName ) {
		RemoveCups();
		ball.ResetBall();	
		Destroy( GameObject.Find( cupPositionName ) );
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
		StartCoroutine( DelayedResetTable() );
	}

	IEnumerator ClearScoredCup( string cupPositionName ) {
		yield return new WaitForSeconds(0.9f);
		// TODO: use delegation / callback to resolve cup clearing race condition
		Destroy( GameObject.Find( cupPositionName ) );
	}

	/***************************************************
	*** Provide an implementation of the ResetTable
	*** method specificially for when a player has
	*** scored a cup
	****************************************************/
	IEnumerator DelayedResetTable() {        		
        yield return new WaitForSeconds(1);
		RemoveCups();
		ball.ResetBall();	
		inGameCanvas.ClearScoreMessage();
		PlaceCups();
		currentGameState = GameState.Ready;
		Debug.Log( GameObject.FindGameObjectsWithTag( "Cup Position" ).Length );
    }


}
