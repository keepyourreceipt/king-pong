using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScoreCollider : MonoBehaviour {
	
	private LevelManager levelManager;
	private string cupPositionName;

	/***************************************************
	*** Get references to required objects
	****************************************************/
	void Start () {
		levelManager = (LevelManager) GameObject.Find("Level Manager").GetComponent(typeof(LevelManager));
		cupPositionName = gameObject.transform.parent.parent.name;
	}

	/***************************************************
	*** Call player scored function from level manager
	****************************************************/
	void OnTriggerEnter( Collider coll ) {	
		if ( levelManager.currentGameState != LevelManager.GameState.DidScore ) {
			levelManager.PlayerHasScored( cupPositionName );
		}			
	}
}
