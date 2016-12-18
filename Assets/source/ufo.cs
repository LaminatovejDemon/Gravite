using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : MonoBehaviour
{
    bool pressed = false;
    float startime;
	public trail _trail;

	Vector3 _touchDirection = Vector3.zero;

    // Use this for initialization
    void OnDrawGizmos()
    {
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + (_touchDirection.normalized * 50f));
    }

	void HideForce(){
		gameObject.GetComponent<LineRenderer> ().enabled = false;
	}

	void DrawForce(){
		gameObject.GetComponent<LineRenderer> ().enabled = true;
		gameObject.GetComponent<LineRenderer> ().SetPosition (0, transform.position);
		gameObject.GetComponent<LineRenderer> ().SetPosition (1, transform.position + _touchDirection);
	}

    // Update is called once per frame
    void Update()
    {
		_trail.UpdateTrail ();

		Vector3 accumulate_ = Vector3.zero;

		if (Input.GetMouseButton (0)) {
			pressed = true;
		}
		else if (Input.GetMouseButtonUp (0)) {
			pressed = false;
		}

		if (pressed) {
			accumulate_ += Input.mousePosition;
		} else if (Input.touchCount > 0 ){
			for (int i = 0; i < Input.touchCount; ++i) {
				accumulate_ += new Vector3 (Input.GetTouch (i).position.x, 0, Input.GetTouch(i).position.y);
			}
			accumulate_ /= Input.touchCount;
		}
		else {
			HideForce ();
			return;
		}

		Vector3 inputWorld_ = Camera.main.ScreenToWorldPoint (accumulate_);
		_touchDirection = (inputWorld_ - transform.position);

		this.GetComponent<Rigidbody>().AddForce(_touchDirection * Time.deltaTime * 200.0f);

		DrawForce ();

    }
}


