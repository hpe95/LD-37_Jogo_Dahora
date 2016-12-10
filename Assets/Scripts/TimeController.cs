using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TimeController : MonoBehaviour {

    private int time = 0;
    private float hourAngle = 0;
    private float minuteAngle = 0;

    public ScoreManager score;

    // Use this for initialization
    void Start () {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(30);
        print(FindObjectOfType<ScoreManager>().score);
        File.WriteAllText(Application.dataPath + "/score.txt", score.score.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
