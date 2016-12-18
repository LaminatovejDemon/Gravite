using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionLink : MonoBehaviour {

	public GameObject Target;
	Vector3 offset;

	void Start() {
		offset = transform.position - Target.transform.position;	
	}

	void Update () {
		transform.position = Target.transform.position + offset;
	}
}
