using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScoreManager : MonoBehaviour {

    public int score = 0;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        try
        {
            string s = File.ReadAllText(Application.dataPath + "/score.txt");
            score = System.Int32.Parse(s);
        }
        catch(System.Exception e)
        {
            print("foi n");
            score = 0;
        }
        scoreText.text = score.ToString();
	}

    public void IncrementScore()
    {
        int x = System.Int32.Parse(scoreText.text);
        x += 1;
        scoreText.text = x.ToString();
    }

    public void AddToScore(int amount)
    {
        int x = System.Int32.Parse(scoreText.text);
        x += amount;
        print(x);
        scoreText.text = x.ToString();
        score = x;
    }

    // Só pra ficar com o nome bonito, mas na real que faz a msm coisa
    public void DecreaseScore(int amount)
    {
        AddToScore(-amount);
    }
}
