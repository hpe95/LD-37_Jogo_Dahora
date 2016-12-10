using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Text[] texts = new Text[2];

    private Color color;
    private int index;

    // Use this for initialization
    void Start() {
        color = texts[0].color;
        ActivateText(texts[0]);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DeactivateText(texts[index]);
            index = (index - 1) % 2;
            index = Mathf.Abs(index);
            ActivateText(texts[index]);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DeactivateText(texts[index]);
            index = (index + 1) % 2;
            index = Mathf.Abs(index);
            ActivateText(texts[index]);
        }
    }

    void ActivateText(Text textToActivate)
    {
        textToActivate.color = Color.red;
    }

    void DeactivateText(Text textToDeactivate)
    {
        textToDeactivate.color = color;
    }
}
