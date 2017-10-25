using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupScoreCollider : MonoBehaviour {

	private GameObject levelManager;
	private LevelManager levelManagerClass;
	private string cupPositionName;

	void Start () {
		levelManager = GameObject.Find("Level Manager");
		levelManagerClass = (LevelManager) levelManager.GetComponent(typeof(LevelManager));
		cupPositionName = gameObject.transform.parent.parent.name;
	}

	void OnTriggerEnter( Collider coll ) {		
		levelManagerClass.PlayerHasScored( cupPositionName );
	}

}
