using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

	public Vector3 direction;
	// Use this for initialization
	void Start () {
		direction.Normalize ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collisionObject){
		Vector2 normal = collisionObject.contacts [0].normal;
		direction = Vector2.
	}
}
