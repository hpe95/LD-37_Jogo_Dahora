using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TimeController : MonoBehaviour {

    private int time = 0;
    private float hourAngle = 0;
    private float minuteAngle = 0;

    // Use this for initialization
    void Start () {
        StartCoroutine(Wait());
    }

    private TextAsset textAsset;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10);
        FileStream fs = File.OpenWrite(Application.dataPath + "score.txt");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
