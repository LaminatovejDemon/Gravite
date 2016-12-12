using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : manager<timeManager> {
	private float time_;
	public float time{
		get {
			return time_;
		}
		private set{
			time_ = value;
		}
	}

	public float deltaTime {
		get {
			return Time.deltaTime;
		}
	}

	public void Tick(){
		time += deltaTime;
	}
}
