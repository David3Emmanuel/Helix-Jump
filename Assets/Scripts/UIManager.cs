using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    void Update()
    {
        scoreText.text = GameManager.instance.score.ToString();
        bestScoreText.text = "Best: " + GameManager.instance.highScore;
    }
}
