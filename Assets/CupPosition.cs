using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupPosition : MonoBehaviour {

	/***************************************************
	*** Draw visible marker for position
	****************************************************/
	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere( transform.position, 0.5f );
	}
}
