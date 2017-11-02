using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvas : MonoBehaviour {

	public Text[] scoreMessages;
	private Canvas canvas;

	void Start () {
		canvas = GetComponent<Canvas>();
	}

	public void DisplayLoseMessage() {
		// TODO: display lose message if number of throws remaining is 0
	}

	public void DisplayWinMessage() {
		// TODO: display win message and buttons to replay level and go to main screen
	}

	public void DisplayScoreMessage() {
		int randomNumber = Random.Range(1, 3); 
		Instantiate( scoreMessages[randomNumber], transform.position, Quaternion.identity, canvas.transform);
	}

	public void ClearScoreMessage() {
		foreach ( Transform scoreMessage in canvas.transform ) {
			if ( scoreMessage.name != "Throw Count" ) {
				GameObject.Destroy( scoreMessage.gameObject );
			}
		}
	}
}
