using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Image = UnityEngine.UI.Image;

public class TimeController : MonoBehaviour {

    private int time = 0;
    private float hourAngle = 0;
    private float minuteAngle = 0;

    public CharacterController player;
    public UnityEngine.UI.Text doDrugs;
    public Image overlay;
    public GameObject hour;
    public GameObject minute;
    public ScoreManager score;
    public UnityEngine.UI.Text days;
    private BlindnessController blindness;

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<CharacterController>();
        doDrugs.text = player.maxDrugs.ToString();
        overlay.color = new Color(0, 0, 0, 0);
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
        days.enabled = false;
    }

    void Update()
    {
        doDrugs.text = player.actualDrugs.ToString();
    }

    IEnumerator Wait()
    {

        Vector2 actual = Vector2.up;
        for (int i = 0; i < 500; i++)
        {
            //actual = new Vector2(Mathf.Cos(0.5f) * actual.x - Mathf.Sin(0.5f) * actual.y, Mathf.Sin(0.5f) * actual.x + Mathf.Cos(0.5f) * actual.y);
            hour.transform.Rotate(Vector3.forward, -0.24f, Space.World);
            yield return new WaitForSeconds(0.01f);
        }
        string s = "";


        float intensity = blindness.vignette.intensity + 0.33f;
        if (intensity > 1f)
        {
            intensity = 1f;
        }
        s = string.Concat(s, score.score.ToString() + "," + intensity.ToString());
        try
        {
            string s2 = File.ReadAllText(Application.dataPath + "/score.txt");
            string[] s1 = s2.Split(',');
            s = string.Concat(s, "," + (System.Int32.Parse(s1[2]) + 1).ToString());
        }
        catch (System.Exception e)
        {
            s = string.Concat(s, ",2");
        }
        s = string.Concat(s, "," + player.actualDrugs.ToString());
        print(s);
        File.WriteAllText(Application.dataPath + "/score.txt", s);

        while (overlay.color.a < 1f)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, overlay.color.a + 0.01f);
            yield return new WaitForSeconds(1f / 60f);       
        }

        days.enabled = true;
        yield return new WaitForSeconds(2f);


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
