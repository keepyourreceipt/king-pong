using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject cup;
	private Ball ball;

	/***************************************************
	*** Setup table and get required objects
	****************************************************/
	void Start () {		
		if ( cup == null ) {
			Debug.LogError( "Please load a prefab cup into Level Manager" );
		}
		PlaceCups();	
		ball = (Ball) GameObject.Find("Ball").GetComponent(typeof(Ball));
	}

	/***************************************************
	*** Please a cup at all cup positions
	****************************************************/
	void PlaceCups() {			
		GameObject[] positions = GameObject.FindGameObjectsWithTag( "Cup Position" );	
		foreach( GameObject position in positions ) {
			var newCup = Instantiate( cup, position.transform.position, Quaternion.identity, position.transform );
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
	}

	/***************************************************
	*** Reset table - with score
	****************************************************/
	public void ResetTable( string cupPositionName ) {
		RemoveCups();
		ball.ResetBall();	
		Destroy( GameObject.Find( cupPositionName ) );
		PlaceCups();
	}

	/***************************************************
	*** Show score HUD element and resrt table
	****************************************************/
	public void PlayerHasScored( string cupPositionName ) {
		// TODO: show score HUD element
		ResetTable( cupPositionName );
	}

}
