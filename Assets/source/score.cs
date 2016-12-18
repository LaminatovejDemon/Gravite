using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour {

	public float Score = 0.0f;

	public void Add(float amount){
		Score += amount;
	}
	
	void Update () {
		GetComponent<TextMesh> ().text = Score.ToString("F2") + "m";
	}
}
