using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phyObject : MonoBehaviour {

	public float SelfRotation = 0.0f;
	public float Mass = 1.0f;

	// Sun: 1.989 * 10^30 ~ 1989000
	// Moon: 7.345 * 10^22 ~ 0.07345
	// Earth: 5.972 * 10^24 ~ 5.972

	float _resultRotation = 0;
	Vector3 _resultForces = Vector3.zero;


	Vector3 _gravityForce = Vector3.zero;
	Vector3 _inertiaForce = Vector3.zero;

	public void Start(){
		phyManager.get.Initialise (this);
		gameObject.AddComponent<trail> ();
	}

	public void Cycle() {
		GetComponent<trail>().UpdateTrail();

		this.transform.position += _resultForces * timeManager.get.deltaTime * 3.0f;
		this.transform.Rotate (Vector3.up, _resultRotation * timeManager.get.deltaTime);

		_resultRotation = SelfRotation;
		_resultForces = Vector3.zero;
	}
		
	public void ApplyGravity(phyObject source){
		Vector3 direction_ = (source.transform.position - gameObject.transform.position);
		Vector3 tangent_ = new Vector3 (-direction_.z, 0, direction_.x).normalized;
		float massProduct_ = Mass * source.Mass;
		float inertiaForce_ = 100.0f * massProduct_ * source.Mass / Mathf.Max(1.0f, Mathf.Pow(direction_.magnitude, 4.0f));

		_inertiaForce = source._resultRotation * tangent_ * inertiaForce_ / Mathf.Pow(Mass, 4.0f); 

		_resultForces += _inertiaForce * 1f;
		_resultForces += _gravityForce * 1f;
	}

	void OnDrawGizmos(){
/*		Vector3 pos_ = transform.position + Vector3.up * transform.localScale.y * 0.5f;

		Gizmos.color = Color.green;
		Gizmos.DrawLine (pos_, pos_ + (_gravityForce * 10f));
		Gizmos.color = Color.red;
		Gizmos.DrawLine (pos_, pos_ + (_inertiaForce * 10f));
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (pos_, pos_ + (_resultForces * 10f));*/
	}


}
