using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phyManager : MonoBehaviour {
	List<phyObject> _objects;
	private static phyManager _instance;
	public static phyManager manager {
		get {
			if (_instance == null) {
				phyManager[] managers_ = GameObject.FindObjectsOfType<phyManager> ();
				if (managers_.Length > 0) {
					_instance = managers_ [0];
				} else if (_instance == null) {
					_instance = new GameObject().AddComponent<phyManager>();
					_instance.gameObject.name = "phyManager";	
				}	
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

}
