using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
	public float moveSpeed = 0f;
	private float moveHorizontal = 0f;
	private float moveVertical = 0f;
	public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	public void Move(){
		moveHorizontal += Input.GetAxis ("Vertical") * moveSpeed;
		moveVertical += Input.GetAxis ("Horizontal") * moveSpeed;
		Vector3 movement = new Vector3 (moveVertical, moveHorizontal, transform.position.z);
		rb.MovePosition (movement);
	}
}
