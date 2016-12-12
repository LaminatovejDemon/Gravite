using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phyManager : manager<phyManager> {
	
	List<phyObject> _objects;
	public int Speed = 10;

	public void SpreadGravity(phyObject source){
		
		for (int i = 0; i < _objects.Count; ++i) {
			if (source == _objects [i]) {
				continue;
			}
			_objects [i].ApplyGravity (source);	
		}
	}

	public void Initialise(phyObject source){
		if (_objects == null) {
			_objects = new List<phyObject> ();
		}
		if (!_objects.Contains (source)) {
			_objects.Add (source);
		}
	}

	void Update(){
		for (int i = 0; i < Speed; ++i) {
			timeManager.get.Tick();
			for ( int j = 0; j < _objects.Count; ++j){
				SpreadGravity (_objects[j]);		
				_objects [j].Cycle ();
			}
		}
	}
}
