using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject cup;
	private GameObject ball;
	private Ball ballClass;

	void Start () {
		PlaceCupsOnTableAt( GetCupPositions() );
		ball = GameObject.Find("Ball");
		ballClass = (Ball) ball.GetComponent(typeof(Ball));
	}

	GameObject[] GetCupPositions() {		
		return GameObject.FindGameObjectsWithTag( "Cup Position" );	
	}

	void PlaceCupsOnTableAt( GameObject[] positions ) {		
		foreach( GameObject position in positions ) {
			var newCup = Instantiate( cup, position.transform.position, Quaternion.identity, position.transform );
		}
	}

	void ClearAllCups() {
		GameObject[] cups = GameObject.FindGameObjectsWithTag( "Cup" );
		foreach ( GameObject cup in cups ) {
			Destroy( cup );
		}
	}

	public void PlayerHasScored( string cup ) {
		Debug.Log( "Show score graphic" );
		ClearAllCups();
		Destroy( GameObject.Find( cup ) );
		ballClass.ResetBall();
		PlaceCupsOnTableAt( GetCupPositions() );
	}

}
