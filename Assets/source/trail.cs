using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trail : MonoBehaviour {

	List<Vector3> _positions;
	public GameObject Target;
	public int Length = 10;

	public void UpdateTrail(){
		if (GetComponent<LineRenderer> () == null) {
			gameObject.AddComponent<LineRenderer>();
			GetComponent<LineRenderer> ().endColor = new Color (0.5f, 0.2f, 0.05f, 0);
		}

		LineRenderer line_ = GetComponent<LineRenderer> ();
		if (_positions == null) {
			_positions = new List<Vector3>();
		}
		if ( line_ == null) {
			line_ = gameObject.AddComponent<LineRenderer> ();
		}

		if (_positions.Count > 1 && (_positions [_positions.Count - 2] - Target.transform.position).magnitude < 3.0f) {
			_positions [_positions.Count - 1] = Target.transform.position;
		} else {
			_positions.Add (Target.transform.position);
			if (_positions.Count > Length) {
				_positions.RemoveAt (0);
			}
		}

		while (_positions.Count > 10 && (_positions [0] - Target.transform.position).magnitude < 7.0f) {
			_positions.RemoveAt(0);
		}

		if (line_.numPositions != _positions.Count) {
			line_.numPositions = _positions.Count;
		}

		line_.SetPositions (_positions.ToArray());
	}
}
