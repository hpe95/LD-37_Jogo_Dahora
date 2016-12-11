using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Text[] texts = new Text[2];
    public SpriteRenderer background;
    public Color chosenColor;

    private Color color;
    private int index;
    private bool canGo = true;

    // Use this for initialization
    void Start() {
        color = texts[0].color;
        ActivateText(texts[0]);
        System.IO.File.Delete(Application.dataPath + "/score.txt");
        scale();
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
                    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                }
                else
                {
                    Application.Quit();
                }
            }
        }
        
    }

    void scale()
    {
        transform.localScale = new Vector3(1, 1, 1);

        float width = background.sprite.bounds.size.x;
        float height = background.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float x = worldScreenWidth / width;
        float y = worldScreenHeight / height;

        background.gameObject.transform.localScale = new Vector3(x, y, 1);
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
        textToActivate.color = chosenColor;
    }

    void DeactivateText(Text textToDeactivate)
    {
        textToDeactivate.color = color;
    }
}
