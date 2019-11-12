using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI : MonoBehaviour
{
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    //singleton implementation
    private static UI instance;
    public static UI Instance
    {
        get
        {
            if (instance == null)
                instance = new UI();

            return instance;
        }
    }

    protected UI()
    {
    }

    private float score = 0;

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public void SetScore(float value)
    {
        score = value;
        UpdateScoreText();
    }

    public void IncreaseScore(float value)
    {
        score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = score.ToString();
    }

    public void SetStatus(string text)
    {
        StatusText.text = text;
    }

    public Text ScoreText, StatusText;

}
