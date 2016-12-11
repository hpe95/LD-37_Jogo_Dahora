using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterController : MonoBehaviour {
	public float moveSpeed = 0f;
	private float moveHorizontal = 0f;
	private float moveVertical = 0f;
    private bool alreadyUsed = false;
	public GameObject button; 
    private BlindnessController blindness;

    public int maxDrugs;
    public int actualDrugs;
    public ScoreManager score;
    public int radiusOfView;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        try
        {
            string s = File.ReadAllText(Application.dataPath + "/score.txt");
            string[] s1 = s.Split(',');
            actualDrugs = System.Int32.Parse(s1[3]);
        }
        catch (System.Exception e)
        {
            print("Na moral que não foi");
            actualDrugs = maxDrugs;
        }

		rb = GetComponent<Rigidbody2D> ();
        score = FindObjectOfType<ScoreManager>();
        blindness = FindObjectOfType<BlindnessController>();
	}


    private Collider2D[] overlapped = null;
    // Update is called once per frame
    void Update () {
		Move();
		if ((Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Z)) && !alreadyUsed)
        {
            UseRemedy();
        }
		overlapped = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radiusOfView, 1 << LayerMask.NameToLayer("UsableObjects"));
		float mindistance = 1000f;
		ObjectController object1 = null;
		foreach (Collider2D coll in overlapped)
		{
			
			ObjectController oc = coll.gameObject.GetComponent<ObjectController>();
			if (!oc.used) {
				Vector3 distance = oc.transform.position - transform.position;
				float actualDistance = distance.magnitude;
				if (actualDistance < mindistance) {
					mindistance = actualDistance;
					object1 = oc;
				}
			}

		}

		if (object1 != null) {
			object1.enableButton ();
		} else {
			button.GetComponent<SpriteRenderer> ().enabled = false;
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.X))
        {
			if (object1 != null) {
				object1.use ();
				object1.checkTask ();
			}
        }
    }

    private const float EPS = 1e-9f;

	public void Move(){
		moveHorizontal = Input.GetAxis ("Vertical");
		moveVertical = Input.GetAxis("Horizontal");

        moveHorizontal = Mathf.Abs(moveHorizontal) < EPS ? 0 : moveHorizontal*(moveSpeed-blindness.percentage*2);
        moveVertical = Mathf.Abs(moveVertical) < EPS ? 0 : moveVertical*(moveSpeed- blindness.percentage*2);

        Vector2 movement = new Vector2 (moveVertical, moveHorizontal);
		rb.velocity = movement;
	}

    private void UseRemedy()
    {
        if (!blindness.doingStuff && actualDrugs > 0)
        {
            actualDrugs -= 1;
            alreadyUsed = true;
            blindness.DoHeal();
            score.DecreaseScore(300);
        }
    }
}
