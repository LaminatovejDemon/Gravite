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


    }

}


