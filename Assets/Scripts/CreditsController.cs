using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {
	// Use this for initialization
	void Start () {
		StartCoroutine (WaitForEndGame ());

	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator WaitForEndGame(){
		yield return new WaitForSeconds (10);
		Application.Quit ();
	}
}
