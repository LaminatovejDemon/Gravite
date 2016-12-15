using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : MonoBehaviour
{
    bool pressed = false;
    float startime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (!pressed)
                {
                    startime = Time.time;
                }
            pressed = true;
            

           Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) / Camera.main.orthographicSize;
            
            this.GetComponent<Rigidbody>().AddForce(dir * 20 * (startime - Time.deltaTime));

            print(dir);

        }

        if  (Input.GetMouseButtonUp(0))
        {
            pressed = false;
        }


    }

}


