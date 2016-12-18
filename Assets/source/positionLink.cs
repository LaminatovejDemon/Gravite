using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionLink : MonoBehaviour {

	public GameObject Target;
	Vector3 offset;
	bool _stopped = false;

	void Start() {
		offset = transform.position - Target.transform.position;	
	}

	void Update () {
		if (_stopped) {
			return;
		}
		transform.position = Target.transform.position + offset;
	}

	public void Stop(){
		_stopped = true;
	}
}
