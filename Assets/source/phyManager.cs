using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phyManager : MonoBehaviour {
	List<phyObject> _objects;
	private static phyManager _instance;
	public static phyManager manager {
		get {
			if (_instance == null) {
				_instance = new GameObject().AddComponent<phyManager>();
				_instance.gameObject.name = "phyManager";
			}
			return _instance;
		}
	}

	public void SpreadGravity(phyObject source){
		if (_objects == null) {
			_objects = new List<phyObject> ();
		}
		if (!_objects.Contains (source)) {
			_objects.Add (source);
		}

		for (int i = 0; i < _objects.Count; ++i) {
			if (source == _objects [i]) {
				continue;
			}
			_objects [i].ApplyGravity (source);	
		}
	}

	void Update(){
	/*	string log_ = "Objects: ";
		for (int i = 0; i < _objects.Count; ++i) {
			log_ += _objects [i] + ", ";
		}
		Debug.Log (log_);*/
	}
}
