using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;



    void Awake()
    {
        #region Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion


    }

    private void Start()
    {
        AddScore(0);
    }


    public void AddScore(int amount)
    {
        score += amount;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        Debug.Log(score);

        //show the text
    }

    public void ResetScore()
    {
        score = 0;
    }
}
