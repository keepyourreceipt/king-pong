using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed;

	private Vector2 touchDeltaPosition;
	private Vector2 touchPosition;
	private Vector3 startingPosition;
	private Rigidbody rb;
	private LevelManager levelManager;

	/***************************************************
	*** Setup ball and get object references
	****************************************************/
	void Start () {		
		rb = GetComponent<Rigidbody>();
		startingPosition = transform.position;
		levelManager = (LevelManager) GameObject.Find("Level Manager").GetComponent(typeof(LevelManager));
	}
	
	/***************************************************
	*** Manage user input and move / throw ball
	****************************************************/
	void FixedUpdate () {

		if ( Input.touchCount > 0 )  {
			if ( Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved ) {
				Touch touch = Input.GetTouch(0);
				Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));		         

				transform.position = new Vector3( touchedPos.x, touchedPos.y, transform.position.z );			
				touchDeltaPosition = Input.GetTouch(0).deltaPosition;			
			}

			if ( Input.GetTouch(0).phase == TouchPhase.Ended ) {
				rb.useGravity = true;
				rb.AddForce( new Vector3( touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, touchDeltaPosition.y * speed ), ForceMode.Impulse  );			
			}		   
		 }
	}

	/***************************************************
	*** Check collisions and respond accordingly
	****************************************************/
	void OnCollisionEnter( Collision coll ) {		
		if ( coll.gameObject.tag == "Reset" ) {			
			levelManager.ResetTable();
		}

		if ( coll.gameObject.tag == "Table" ) {			
			// TODO: reset ball if it's just rolling around the table
		}
	}

	/***************************************************
	*** Stop ball from moving
	****************************************************/
	public void StopBall() {
		rb.velocity = Vector3.zero;
	}

	/***************************************************
	*** Rest ball to starting position
	****************************************************/
	public void ResetBall() {
		transform.position = startingPosition;
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
	}
}


