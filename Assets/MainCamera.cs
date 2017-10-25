using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	private Ball ball;
	private LevelManager levelManager;

	/***************************************************
	*** Get references to required objects
	****************************************************/
	void Start () {
		ball = (Ball) GameObject.Find("Ball").GetComponent(typeof(Ball));
		levelManager = (LevelManager) GameObject.Find("Level Manager").GetComponent(typeof(LevelManager));
	}
	
	/***************************************************
	*** Follow the ball until a point, then pull back
	****************************************************/
	void Update () {
	 	if ( ball.transform.position.z < 10f ) {
			transform.position = new Vector3( transform.position.x, transform.position.y, ball.transform.position.z - 10f );
	 	} else if ( ball.transform.position.z > 10f ) {
			transform.Translate(Vector3.back * ( Time.deltaTime * 0.5f ) );			
	 	}		
	}
}
