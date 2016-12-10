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

	void Update () {
		phyManager.manager.SpreadGravity (this);

		this.transform.position += _resultForces * 0.1f;
		this.transform.Rotate (Vector3.up, _resultRotation * Time.deltaTime);

		_resultRotation = SelfRotation;
		_resultForces *= 0.7f;
	}
		
	public void ApplyGravity(phyObject source){
		Vector3 direction_ = (source.transform.position - gameObject.transform.position);
		Vector3 tangent_ = new Vector3 (-direction_.z, 0, direction_.x).normalized;
		float massProduct_ = Mass * source.Mass;
		float force_ = 10.0f * massProduct_ / (0.1f + Mathf.Pow(direction_.magnitude, 2.0f));
		_gravityForce = direction_.normalized * force_ / Mass;
	
		if (source._resultRotation != 0) {
			_inertiaForce = tangent_ * source.SelfRotation * force_ * 5.0f / Mass;
		}

		_resultForces += _inertiaForce * Time.deltaTime;
		_resultForces += _gravityForce * Time.deltaTime;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Vector3 pos_ = transform.position + Vector3.up * transform.localScale.y * 0.5f;
		Gizmos.DrawLine (pos_, pos_ + (_gravityForce * 0.1f));
		Gizmos.color = Color.red;
		Gizmos.DrawLine (pos_, pos_ + (_inertiaForce * 0.1f));
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (pos_, pos_ + (_resultForces * 0.001f));
	}
}
