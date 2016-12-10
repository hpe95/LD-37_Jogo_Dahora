using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public int indexToBeChecked;
	private Vector3 direction;
    int radiusOfView;
	private SpriteRenderer sr;
    public ChecklistManager checkList;
	public Sprite buttonSprite;
	public GameObject[] button;
	// Use this for initialization
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
        if(overlapped != null)
        {
			Debug.DrawLine (transform.position, overlapped.gameObject.transform.position);
			if (button[0] != null) {
				direction = transform.position + new Vector3(0,1f,0);
				button [0].transform.position = direction;
				button[0].GetComponent<SpriteRenderer> ().sprite = buttonSprite;
			}

        }
        else
        {
			//print("Não mostrar mais o botão aqui.");
        }
    }

    public void checkTask()
    {
        checkList.checkTask(indexToBeChecked);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        
    }
}
