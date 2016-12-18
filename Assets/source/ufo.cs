using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : MonoBehaviour
{
    bool pressed = false;
    float startime;

	Vector3 _touchDirection = Vector3.zero;

    // Use this for initialization
    void OnDrawGizmos()
    {
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + (_touchDirection.normalized * 50f));
    }

    // Update is called once per frame
    void Update()
    {
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
			return;
		}

		Vector3 inputWorld_ = Camera.main.ScreenToWorldPoint (accumulate_);
		_touchDirection = inputWorld_ - transform.position;
		this.GetComponent<Rigidbody>().AddForce(_touchDirection * 10.0f);


        /*if (Input.GetMouseButton(0))
        {
            if (!pressed)
                {
                    startime = Time.time;
                }
            pressed = true;
            

            Vector3 whereclicked = Camera.main.ScreenToWorldPoint(Input.mousePosition) / Camera.main.orthographicSize;
            whereclicked.x = whereclicked.x / Camera.main.aspect;
          
                       
            Vector3 ufoposition = this.transform.position;
            ufoposition = Camera.main.WorldToViewportPoint(ufoposition);
            
            
            ufoposition.x = ufoposition.x * 2 - 1;
            ufoposition.z = ufoposition.y * 2 - 1;
            ufoposition.y = 0;

            
            Vector3 forcetoufo = whereclicked - ufoposition;

            this.GetComponent<Rigidbody>().AddForce(forcetoufo * 250);

            // * (Time.time - startime)


        }

        if  (Input.GetMouseButtonUp(0))
        {
            pressed = false;
        }
*/

    }

}


