using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed;
	private Vector2 touchDeltaPosition;
	private Vector2 touchPosition;
	private Vector3 startingPosition;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {		
		rb = GetComponent<Rigidbody>();
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
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

				// Throw the ball
				rb.AddForce( new Vector3( touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, touchDeltaPosition.y * speed ), ForceMode.Impulse  );			
			}		   
		 }
	}

	void OnCollisionEnter( Collision coll ) {
		// Check if the ball has hit the floor
		if ( coll.gameObject.tag == "Reset" ) {
			// If so, reset the ball to it's starting position
			ResetBall();
		}

		// Test if the ball has hit the table
		if ( coll.gameObject.tag == "Table" ) {			
			// TODO reset ball if it's just rolling around the table
		}
	}

	public void ResetBall() {
		transform.position = startingPosition;
		rb.useGravity = false;
		rb.velocity = Vector3.zero;
	}
}


