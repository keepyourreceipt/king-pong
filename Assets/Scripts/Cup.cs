using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour {

	private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}

	// Reset cup position
	void ResetCup() {
		transform.position = startingPosition;
	}
}
