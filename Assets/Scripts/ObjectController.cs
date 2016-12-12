using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
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

	public AudioSource audio;


	private string[] lines;
	private bool textEnable = false;
	private CharacterController charController;
	void Awake(){
	}
	void Start () {
		audio = GetComponent<AudioSource> ();
		charController = FindObjectOfType<CharacterController> ();
		lines = new string[9];
        checkList = FindObjectOfType<ChecklistManager>();
        radiusOfView = FindObjectOfType<CharacterController>().radiusOfView;
		sr = GetComponent<SpriteRenderer> ();
		button = GameObject.FindGameObjectsWithTag("Button_B");
		direction = button [0].transform.position;
		lines = File.ReadAllLines(Application.dataPath + "/Texts/Popup.txt");
	}

    Collider2D overlapped;

    // Update is called once per frame
    void Update () {
		//text.enabled = false;
		if(lines.Length == 0){
			lines = File.ReadAllLines(Application.dataPath + "/Texts/Popup.txt");
		}

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

	private bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			lines = File.ReadAllLines(Application.dataPath + "/" + fileName);
		}

		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (System.Exception e)
		{
			return false;
		}
		return true;
	}
}
