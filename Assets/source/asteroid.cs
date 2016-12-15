using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour {

	public float ForceStrengthBase = 200.0f;
	public float ForceStrengthRnd = 200.0f;

	void Start () {
		asteroidManager.get.Register (this);
		SetupPhysics ();
	}

	void Update () {
		CheckOutOfScreen ();
	}

	void Die(){
		asteroidManager.get.Unregister (this);
		Destroy (this.gameObject);
	}

	void SetupPhysics(){
		Rigidbody physics = gameObject.GetComponent<Rigidbody>();
		Vector3 direction_ = Random.onUnitSphere;
		direction_.y = 0; 
		direction_.Normalize();

		physics.isKinematic = true;
		Vector3 cameraPosition_ = Camera.main.transform.position;
		Vector3 asteroidOffset_ = direction_;
		asteroidOffset_.x *= Camera.main.aspect;
		cameraPosition_.y = 0;
		transform.position = cameraPosition_ - (direction_ * transform.localScale.magnitude * 2.0f) - asteroidOffset_ 
			* Camera.main.orthographicSize;
		physics.isKinematic = false;

		float finalForce = ForceStrengthBase + Random.value * ForceStrengthRnd;

		physics.AddForce (direction_ * finalForce, ForceMode.Force);
		physics.AddTorque (Random.onUnitSphere * finalForce, ForceMode.Force);
	}

	void CheckOutOfScreen(){
		Vector3 cameraZero_ = Camera.main.transform.position;
		cameraZero_.y = 0;
		Vector3 distance_ = transform.position - cameraZero_;
		if (Mathf.Abs (distance_.x) - transform.localScale.magnitude * 3.0f > Camera.main.orthographicSize * Camera.main.aspect
			|| Mathf.Abs (distance_.z) - transform.localScale.magnitude * 3.0f > Camera.main.orthographicSize) {
			Die ();
		}
	}
}
