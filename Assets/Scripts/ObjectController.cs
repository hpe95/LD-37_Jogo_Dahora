using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public int[] indexToBeChecked;
	private Vector3 direction;
    int radiusOfView;
	private SpriteRenderer sr;
    public ChecklistManager checkList;
	public Sprite buttonSprite;
	public GameObject[] button;
	public bool used = false;
	public AudioClip anySoundYouWantBro;
	private float volumeAudio = .2f;

	void Awake(){

	}
	void Start () {
		
        checkList = FindObjectOfType<ChecklistManager>();
        radiusOfView = FindObjectOfType<CharacterController>().radiusOfView;
		sr = GetComponent<SpriteRenderer> ();
		button = GameObject.FindGameObjectsWithTag("Button_B");
		direction = button [0].transform.position;
	}

    Collider2D overlapped;

    // Update is called once per frame
    void Update () {
        
        overlapped = Physics2D.OverlapCircle(new Vector2(transform.position.x + 2f, transform.position.y), radiusOfView, 1 << LayerMask.NameToLayer("Player"));
       

     
        
    }

	public void enableButton(){
		if (!used) {
			direction = transform.position + new Vector3 (0, 1.33f, 0);
			button [0].transform.position = direction;
			SpriteRenderer sr = button [0].GetComponent<SpriteRenderer> ();
			sr.enabled = true;
			sr.sprite = buttonSprite;
		}
	}

	public void use(){
		used = true;
	}
	public void disableButton(){
		button [0].GetComponent<SpriteRenderer> ().enabled = false;
	}


    public void checkTask()
    {
        foreach (int index in indexToBeChecked)
        {
            if (checkList.checkTask(index))
            {
                break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
		GetComponent<AudioSource> ().Play ();
    }
}
