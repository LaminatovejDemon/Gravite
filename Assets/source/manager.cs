using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager<T> : MonoBehaviour where T:Component{

	private static T _instance;
	public static T get {
		get {
			if (_instance == null) {
				T[] managers_ = GameObject.FindObjectsOfType<T> ();
				if (managers_.Length > 0) {
					_instance = managers_ [0];
				} else if (_instance == null) {
					_instance = new GameObject().AddComponent<T>();
					_instance.gameObject.name = typeof(T).ToString();	
				}	
			}
			return _instance;
		}
	}
}
