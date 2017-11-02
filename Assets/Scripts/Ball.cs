using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float throwSide;
	public float throwVertical;
	public float throwDownField;

	private float timeOnTable;
	private Vector2 touchDeltaPosition;
	private Vector2 touchPosition;
	private Vector3 startingPosition;

	[HideInInspector]
	public Rigidbody rb;

	private LevelManager levelManager;

	/***************************************************
	*** Setup ball and get object references
	****************************************************/
	void Start () {		
		rb = GetComponent<Rigidbody>();
		startingPosition = transform.position;
		timeOnTable = 0f;
		levelManager = (LevelManager) GameObject.Find("Level Manager").GetComponent(typeof(LevelManager));
	}
	
	/***************************************************
	*** Manage user input and move / throw ball
	****************************************************/
	void FixedUpdate () {

		if ( Input.touchCount > 0 && levelManager.currentGameState == LevelManager.GameState.Ready )  {
			if ( Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved ) {
				Touch touch = Input.GetTouch(0);
				Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));		         

				transform.position = new Vector3( touchedPos.x, touchedPos.y, transform.position.z );			
				touchDeltaPosition = Input.GetTouch(0).deltaPosition;			
			}

			if ( Input.GetTouch(0).phase == TouchPhase.Ended ) {				
				Vector3 adjustedThrow;
				rb.useGravity = true;
				levelManager.currentGameState = LevelManager.GameState.InPlay;

				adjustedThrow.x = touchDeltaPosition.x * throwSide;

				adjustedThrow.y = 0f;

				if ( touchDeltaPosition.y > 20f ) {
					adjustedThrow.y = 19f * throwVertical;
				}

				if ( touchDeltaPosition.y > 5f && touchDeltaPosition.y < 15f ) {
					adjustedThrow.y = 16f * throwVertical;
				}

				adjustedThrow.z = touchDeltaPosition.y * throwDownField;

				rb.AddForce( new Vector3( adjustedThrow.x, adjustedThrow.y, adjustedThrow.z ), ForceMode.Impulse  );			
				levelManager.UpdateThrowsRemaining(-1);
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
	}


	void OnCollisionStay( Collision coll ) {				
		if ( coll.gameObject.tag == "Table" ) {	
			timeOnTable += Time.deltaTime;

			if ( timeOnTable > 0.2f ) {
				levelManager.ResetTable();
			}
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
		enabled = true;
		transform.position = startingPosition;
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		timeOnTable = 0f;
	}
}


