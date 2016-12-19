using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ufo : MonoBehaviour
{
    bool pressed = false;
    float startime;
	public trail _trail;
	public healthBar _health;
	float _deathTimer;
	public score _score;

	Vector3 _touchDirection = Vector3.zero;

	void HideForce(){
		gameObject.GetComponent<LineRenderer> ().enabled = false;
	}

	void DrawForce(){
		gameObject.GetComponent<LineRenderer> ().enabled = true;
		gameObject.GetComponent<LineRenderer> ().SetPosition (0, transform.position);
		gameObject.GetComponent<LineRenderer> ().SetPosition (1, transform.position + _touchDirection);
	}

	void OnCollisionEnter(Collision with){
		if (IsDeath()) {
			return;
		}

		_health.Hit (with.relativeVelocity.magnitude * 0.005f);
		if (_health.ActualHealth <= 0) {
			OnDeath ();
		}
	}

	bool IsDeath(){
		return _health.ActualHealth <= 0;
	}

	void OnDeath(){
		Camera.main.GetComponent<positionLink> ().Stop ();
		_deathTimer = 3.0f;
		HideForce ();
		GetComponent<Rigidbody> ().drag = 0.5f;
	}

	void UpdateDeath(){
		if (_deathTimer > 0) {
			_deathTimer -= Time.deltaTime;
			if (_deathTimer <= 0) {
				SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().name);		
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
		_trail.UpdateTrail ();

		if (_health.ActualHealth <= 0 ) {
			UpdateDeath ();
			return;
		}
		UpdateScore ();

		CatchMouse ();	
		UpdateControls ();

    }

	void UpdateScore(){
		_score.Add (GetComponent<Rigidbody> ().velocity.magnitude * 0.01f);
	}
		
	void CatchMouse(){
		if (Input.GetMouseButton (0)) {
			pressed = true;
		}
		else if (Input.GetMouseButtonUp (0)) {
			pressed = false;
		}
	}

	void UpdateControls(){
//		ApplyForceControllerAlpha();
		ApplyForceControllerBeta();
	}

	void ApplyForceControllerAlpha(){
		Vector3 accumulate_ = Vector3.zero;

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

		this.GetComponent<Rigidbody> ().AddForce (_touchDirection * Time.deltaTime * 200.0f);
		DrawForce ();
	}

	float angle_ = 0;
	void ApplyForceControllerBeta(){
		
		Vector3 accumulate_ = Vector3.zero;

		if (Input.touchCount > 0 ){
			for (int i = 0; i < Input.touchCount; ++i) {
				accumulate_ += new Vector3 (Input.GetTouch (i).position.x, 0, Input.GetTouch(i).position.y);
			}
			accumulate_ /= Input.touchCount;

			Vector3 inputWorld_ = Camera.main.ScreenToWorldPoint (accumulate_);
			_touchDirection = (inputWorld_ - transform.position);
		}

		Vector3 touchPosition_ = Input.touchCount > 0 ? new Vector3(Input.GetTouch(0).position.x, 0, Input.GetTouch(0).position.y) : Vector3.zero;

		if (Input.touchCount == 1 ) {
			Vector3 viewportTouch_ = Camera.main.ScreenToViewportPoint (touchPosition_);
			angle_ += 900.0f * Time.deltaTime * (viewportTouch_.x - 0.5f);
		} else if ( Input.touchCount > 1 ) {
			_touchDirection = Quaternion.AngleAxis (angle_, Vector3.up) * Vector3.forward * _touchDirection.magnitude;
			this.GetComponent<Rigidbody> ().drag = 0.2f;
			this.GetComponent<Rigidbody> ().AddForce (_touchDirection * Time.deltaTime * 50.0f);
		}

		gameObject.GetComponent<LineRenderer> ().endColor = new Color (1, 1, 1, 1);

		Vector3 from_ = transform.position;
		Vector3 to_ = transform.position + (Quaternion.AngleAxis (angle_, Vector3.up) * Vector3.forward * (Input.touchCount > 1 ? 20.0f : 12.0f));
		Debug.Log ("Line from " + from_ + " to " + to_);
		gameObject.GetComponent<LineRenderer> ().SetPosition (0, from_);
		gameObject.GetComponent<LineRenderer> ().SetPosition (1, to_);
	}
}


