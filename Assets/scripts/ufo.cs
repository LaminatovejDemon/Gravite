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
            whereclicked.y = whereclicked.z;
            //PICO NEVIM PROC ALE TY OSY JSOU ZMMMMRRRRDDDLLYYY

            Vector3 ufoposition = this.transform.position;
            ufoposition = Camera.main.WorldToViewportPoint(ufoposition);

            ufoposition.x = ufoposition.x * 2 - 1;
            ufoposition.y = ufoposition.y * 2 - 1;
            ufoposition.z = ufoposition.z * 2 - 1;


         

            Vector3 forcetoufo = whereclicked - ufoposition;

            this.GetComponent<Rigidbody>().AddForce(forcetoufo * 20 * (Time.time - startime));

            //A STEJNE TA PICA JEDE FURT NAHORU KURVA FIX VOLE



        }

        if  (Input.GetMouseButtonUp(0))
        {
            pressed = false;
        }


    }

}


