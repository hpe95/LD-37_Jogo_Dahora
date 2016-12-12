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
    private bool seilavei = true;
    private TimeController tc;

    private Animator anim;

    public int maxDrugs;
    public int actualDrugs;
    public ScoreManager score;
    public int radiusOfView;

    private bool canPlaySound = false;
    private MyUnity music;
    private Vector3[] possibleRespawns = new Vector3[12];
    private Rigidbody2D rb;
    // Use this for initialization
    void Start() {
        tc = FindObjectOfType<TimeController>();
        music = FindObjectOfType<MyUnity>();
        if (music != null) {
            music.GetComponent<AudioHighPassFilter>().enabled = false;
        }
        anim = GetComponent<Animator>();
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


        possibleRespawns[0] = new Vector3(1f, -1f, 0f);
        possibleRespawns[1] = new Vector3(4f, -6f, 0f);
        possibleRespawns[2] = new Vector3(0f, -4f, 0f);
        possibleRespawns[3] = new Vector3(0f, -2f, 0f);
        possibleRespawns[4] = new Vector3(2f, -2f, 0f);
        possibleRespawns[5] = new Vector3(4f, -2f, 0f);
        possibleRespawns[6] = new Vector3(4f, -4f, 0f);
        possibleRespawns[7] = new Vector3(6f, -6f, 0f);
        possibleRespawns[8] = new Vector3(6f, -4f, 0f);
        possibleRespawns[9] = new Vector3(10f, -6f, 0f);

        rb = GetComponent<Rigidbody2D>();
        score = FindObjectOfType<ScoreManager>();
        blindness = FindObjectOfType<BlindnessController>();
        Respawn();
    }


    private Collider2D[] overlapped = null;
    // Update is called once per frame
    void Update() {
        if (tc.paused && Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            ModalDialogManager.Instance.CloseDialog();
        }
        if (seilavei && !tc.paused)
        {
            Move();
            if ((Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Z)) && !alreadyUsed)
            {
                UseRemedy();
            }
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
            object1.enableButton();
        } else {
            button.GetComponent<SpriteRenderer>().enabled = false;
        }
        if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.X)) && seilavei && !tc.paused)
        {
            print(tc.paused);
            if (object1 != null) {
                seilavei = false;
                rb.velocity = Vector2.zero;
                StartCoroutine(seilamano());
                object1.use();
                object1.checkTask();
				object1.gameObject.GetComponent<AudioSource> ().Play ();
            }
        }
    }

    IEnumerator seilamano()
    {
        anim.SetBool("walking", false);
        yield return new WaitForSeconds(3f);
        seilavei = true;
    }

    private const float EPS = 1e-9f;

	public void Move(){

		moveHorizontal = Input.GetAxis ("Vertical");
		moveVertical = Input.GetAxis("Horizontal");

        anim.SetFloat("speedX", moveVertical);
        anim.SetFloat("speedY", moveHorizontal);

        moveHorizontal = Mathf.Abs(moveHorizontal) < EPS ? 0 : moveHorizontal*(moveSpeed-blindness.percentage*2);
        moveVertical = Mathf.Abs(moveVertical) < EPS ? 0 : moveVertical*(moveSpeed- blindness.percentage*2);

        Vector2 movement = new Vector2 (moveVertical, moveHorizontal);
		rb.velocity = movement;
	}

    void FixedUpdate()
    {
        float test = Input.GetAxis("Vertical");
        float test2 = Input.GetAxis("Horizontal");


        test = Mathf.Abs(test) < EPS ? 0 : test;
        test2 = Mathf.Abs(test2) < EPS ? 0 : test2;

        if (seilavei && !tc.paused)
        {
            if (test != 0 || test2 != 0)
            {
                anim.SetBool("walking", true);
                if (test > 0)
                    anim.SetFloat("lastMoveY", 1f);
                else if (test < 0)
                    anim.SetFloat("lastMoveY", -1f);
                else
                    anim.SetFloat("lastMoveY", 0);

                if (test2 > 0)
                    anim.SetFloat("lastMoveX", 1f);
                else if (test2 < 0)
                    anim.SetFloat("lastMoveX", -1f);
                else
                    anim.SetFloat("lastMoveX", 0);
            }
            else
            {
                anim.SetBool("walking", false);
            }
        }
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

	private void Respawn(){
		Vector3 randomRespawn = possibleRespawns [Random.Range (0, 9)];
		transform.position = randomRespawn;
	}
}
