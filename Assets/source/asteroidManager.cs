using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidManager : manager<asteroidManager> {

	asteroid template_;
	public float AsteroidCount = 10.0f;
	float _asteroidCount = 0;

	public void Register(asteroid source){
		++_asteroidCount;
		template_ = source;
		CheckCount ();	
	}

	public void Unregister(asteroid source){
		--_asteroidCount;
		template_ = source;
		CheckCount ();
	}

	void CheckCount(){
		if (_asteroidCount < AsteroidCount) {
			GameObject.Instantiate (template_).name = "Asteroid_" + Time.time;
		}
	}
}
