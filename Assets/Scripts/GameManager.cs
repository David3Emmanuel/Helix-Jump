using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int highScore;
    public int score;
    public int currentStage = 0;
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("high-score", 0);
        AdsManager.instance.LoadInterstitial();
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void RestartLevel()
    {
        AdsManager.instance.ShowInterstitial();
        score = 0;
        FindObjectOfType<PlayerController>().Reset();
        FindObjectOfType<HelixController>().Reset();
        foreach (PassCheck passCheck in FindObjectsOfType<PassCheck>())
        {
            passCheck.Reset();
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("high-score", highScore);
        }
    }
}
