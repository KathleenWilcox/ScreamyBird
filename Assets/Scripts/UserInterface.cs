using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{

    private int score;
    public Text scoreText;
    public Text endScore;
    public Text endText;
    public MicBird bird;

    void Start() {
        score = 0;
    }

    void Update()
    {
        if (bird.checkIfDead()) {
            endScore.text = "Score: " + score;
            endScore.gameObject.SetActive(true);
            scoreText.enabled = false;
            endText.gameObject.SetActive(true);
        }
    }

    public void increaseScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
