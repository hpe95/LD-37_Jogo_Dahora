using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatController : MonoBehaviour {

    Animator anim;
    bool done = true;
    float velocity = 0.5f;
    Rigidbody2D rb;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Walk();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Sit()
    {
        anim.SetTrigger("Sit");
        float deltaTime = Random.Range(2f, 5f);
        done = false;
        rb.velocity = Vector2.zero;
        StartCoroutine(Wait(deltaTime, false));
       
    }

    void Walk()
    {
        
        float deltaTime = Random.Range(1.5f, 3f);
        int dir = Random.Range(0, 2);
        if(dir == 1)
        {
            sr.flipX = true;
            rb.velocity = Vector2.right * velocity;
        }
        else
        {
            sr.flipX = false;
            rb.velocity = Vector2.left * velocity;
        }
        anim.SetTrigger("Walk");

        done = false;
        StartCoroutine(Wait(deltaTime, true));
    }

    IEnumerator Wait(float delta, bool seila)
    {
        yield return new WaitForSeconds(delta);
        if (seila)
        {
            Sit();
        } else
        {
            Walk();
        }
    }
}
