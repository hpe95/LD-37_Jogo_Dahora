using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private int score = 0;
    public Text scoreText;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
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
    }

    // Só pra ficar com o nome bonito, mas na real que faz a msm coisa
    public void DecreaseScore(int amount)
    {
        AddToScore(-amount);
    }
}
