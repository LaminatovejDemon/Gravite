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
	List<Vector3> _positions;

	Vector3 _gravityForce = Vector3.zero;
	Vector3 _inertiaForce = Vector3.zero;

	void Update () {
		phyManager.manager.SpreadGravity (this);

		UpdateTrail ();

		this.transform.position += _resultForces * Time.deltaTime * 3.0f;
		this.transform.Rotate (Vector3.up, _resultRotation * Time.deltaTime);

		_resultRotation = SelfRotation;
		_resultForces = Vector3.zero;
	}

	void UpdateTrail(){
		LineRenderer line_ = GetComponent<LineRenderer> ();
		if (_positions == null) {
			_positions = new List<Vector3>();
		}
		if ( line_ == null) {
			line_ = gameObject.AddComponent<LineRenderer> ();
		}

		if (_positions.Count > 1 && (_positions [_positions.Count - 2] - transform.position).magnitude < 3.0f) {
			_positions [_positions.Count - 1] = transform.position;
		} else {
			_positions.Add (transform.position);
			if (_positions.Count > 100) {
				_positions.RemoveAt (0);
			}
		}

		while (_positions.Count > 10 && (_positions [0] - transform.position).magnitude < 7.0f) {
			_positions.RemoveAt(0);
		}

		if (line_.numPositions != _positions.Count) {
			line_.numPositions = _positions.Count;
		}

		line_.SetPositions (_positions.ToArray());
	}
		
	public void ApplyGravity(phyObject source){
		Vector3 direction_ = (source.transform.position - gameObject.transform.position);
		Vector3 tangent_ = new Vector3 (-direction_.z, 0, direction_.x).normalized;
		float massProduct_ = Mass * source.Mass;
		float force_ = 100.0f * massProduct_ / Mathf.Max(1.0f, Mathf.Pow(direction_.magnitude, 4.0f));

		_gravityForce = direction_.normalized * force_ / Mathf.Pow(Mass, 2f);
		_inertiaForce = 15.0f * source._resultRotation * tangent_ * force_ / Mathf.Pow(Mass, 2f);

		_resultForces += _inertiaForce;
		_resultForces += _gravityForce;
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
