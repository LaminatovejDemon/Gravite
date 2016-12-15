using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //float dirX = Input.mousePosition.x;
            //float dirY = Input.mousePosition.y;
            //dirX = dirX / Camera.main.orthographicSize * Camera.main.aspect;
            //dirY = dirY / Camera.main.orthographicSize;
            //Vector3 dir = new Vector3(dirX,0f,dirY);

            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) / Camera.main.orthographicSize;

            //Vector3 dir = Input.mousePosition - this.transform.position;
            //dir = dir.normalized;
            print("CLICK POSITION "+ dir);
            //print("UFO POSITIONIN " + this.transform.position);
            this.GetComponent<Rigidbody>().AddForce(dir * 250);
            

        }


    }

}


