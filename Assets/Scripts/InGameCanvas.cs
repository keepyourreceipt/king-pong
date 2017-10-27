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
	
	public void DisplayScoreMessage() {
		int randomNumber =  Random.Range(1, 3); 
		Instantiate( scoreMessages[randomNumber], transform.position, Quaternion.identity, canvas.transform);
	}

	public void ClearScoreMessage() {
		foreach ( Transform scoreMessage in canvas.transform ) {
			GameObject.Destroy( scoreMessage.gameObject );
		}
	}
}
