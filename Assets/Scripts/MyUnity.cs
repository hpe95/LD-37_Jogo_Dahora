using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnity : MonoBehaviour {
	private static MyUnity instance = null;

	public static MyUnity Instance{
		get { return instance; }
	}

	void Awake(){
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		} else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
