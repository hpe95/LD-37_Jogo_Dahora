using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Text[] texts = new Text[2];

    private Color color;
    private int index;
    private bool canGo = true;

    // Use this for initialization
    void Start() {
        color = texts[0].color;
        ActivateText(texts[0]);
        System.IO.File.Delete(Application.dataPath + "/score.txt");
	}
	
	// Update is called once per frame
	void Update () {
        if (canGo)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                doStuff();
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                doStuff();
            }
            else if(Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                if (index == 0)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("First_Scene");
                }
                else
                {
                    Application.Quit();
                }
            }
        }
        
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        canGo = true;

    }

    void doStuff()
    {
        DeactivateText(texts[index]);
        index = (index - 1) % 2;
        index = Mathf.Abs(index);
        ActivateText(texts[index]);
        canGo = false;
        StartCoroutine(wait());
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
