using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatController : MonoBehaviour {

    Animator anim;
    bool done = true;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        Sit();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Sit()
    {
        anim.SetTrigger("sit");
        float deltaTime = Random.Range(0.5f, 2f);
        done = false;
        StartCoroutine(Wait(deltaTime));
        while (!done)
        {
            // Do nothing
        }
        Walk();
    }

    void Walk()
    {
        anim.SetTrigger("walk");
        print("oi");
        float deltaTime = Random.Range(0.5f, 2f);
        done = false;
        StartCoroutine(Wait(deltaTime));
        while (!done)
        {
            // Do nothing
        }
        Sit();
    }

    IEnumerator Wait(float delta)
    {
        yield return new WaitForSeconds(delta);
        done = true;
    }
}
