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
    public UnityEngine.UI.Text days;
    private BlindnessController blindness;

    // Use this for initialization
    void Start () {
        StartCoroutine(Wait());
        blindness = FindObjectOfType<BlindnessController>();
        string s = "";
        try
        {
            string s2 = File.ReadAllText(Application.dataPath + "/score.txt");
            string[] s1 = s2.Split(',');
            s = string.Concat(s, (System.Int32.Parse(s1[2]) + 1).ToString());
        }
        catch (System.Exception e)
        {
            s = string.Concat(s, "1");
        }
        days.text = string.Concat("Day ", s);
    }

    IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(10);
        string s = "";
       
        
        float intensity = blindness.vignette.intensity + 0.33f;
        if(intensity > 1f)
        {
            intensity = 1f;
        }
        s = string.Concat(s, score.score.ToString() + "," + intensity.ToString());
        try
        {
            string s2 = File.ReadAllText(Application.dataPath + "/score.txt");
            string[] s1 = s2.Split(',');
            s = string.Concat(s, ","+(System.Int32.Parse(s1[2])+1).ToString());
        }
        catch (System.Exception e)
        {
            s = string.Concat(s, ",2");
        }
        print(s);
        File.WriteAllText(Application.dataPath + "/score.txt", s);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
