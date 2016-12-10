using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
	public float moveSpeed = 0f;
	private float moveHorizontal = 0f;
	private float moveVertical = 0f;
    private bool alreadyUsed = false;

    public ScoreManager score;
    public int radiusOfView;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
        score = FindObjectOfType<ScoreManager>();
	}


    private Collider2D[] overlapped = null;
    // Update is called once per frame
    void Update () {
		Move();
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && !alreadyUsed)
        {
            UseRemedy();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            overlapped = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radiusOfView, 1 << LayerMask.NameToLayer("UsableObjects"));
            foreach (Collider2D coll in overlapped)
            {
                print("oi");
                ObjectController oc = coll.gameObject.GetComponent<ObjectController>();
                oc.checkTask();
                SpriteRenderer sr = oc.GetComponent<SpriteRenderer>();
                sr.material.mainTexture.mipMapBias = 0;
            }
        }
    }

    private const float EPS = 1e-9f;

	public void Move(){
		moveHorizontal = Input.GetAxis ("Vertical");
		moveVertical = Input.GetAxis("Horizontal");

        moveHorizontal = Mathf.Abs(moveHorizontal) < EPS ? 0 : moveHorizontal*moveSpeed;
        moveVertical = Mathf.Abs(moveVertical) < EPS ? 0 : moveVertical*moveSpeed;

        Vector2 movement = new Vector2 (moveVertical, moveHorizontal);
		rb.velocity = movement;
	}

    private void UseRemedy()
    {
        alreadyUsed = true;
        score.DecreaseScore(300);
    }
}
