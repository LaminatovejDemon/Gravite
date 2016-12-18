using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour {

	public float TotalHealth = 1.0f;
	public float ActualHealth;

	void Start () {
		ActualHealth = TotalHealth;
		UpdateVisual ();
	}

	public void Reset(){
		ActualHealth = TotalHealth;
		UpdateVisual ();
	}

	public void Hit(float amount){
		ActualHealth -= amount;	
		if (ActualHealth < 0) {
			ActualHealth = 0;
		}
		UpdateVisual ();
	}
		
	void UpdateVisual(){
		GetComponent<Renderer>().material.SetFloat("_Cutoff", Mathf.Max(0.001f, (TotalHealth - ActualHealth) / TotalHealth)); 	
	}
}
