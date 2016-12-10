using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public int indexToBeChecked;
	private Vector3 direction;
    int radiusOfView;

    public ChecklistManager checkList;
	// Use this for initialization
	void Start () {
		direction.Normalize ();
        checkList = FindObjectOfType<ChecklistManager>();
        radiusOfView = FindObjectOfType<CharacterController>().radiusOfView;
	}

    Collider2D overlapped;

    // Update is called once per frame
    void Update () {
        
        overlapped = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), radiusOfView, 1 << LayerMask.NameToLayer("Player"));
        if(overlapped != null)
        {
            print("Mostrar o botão do xbox aqui.");
        }
        else
        {
            print("Não mostrar mais o botão aqui.");
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
