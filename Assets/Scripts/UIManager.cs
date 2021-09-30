using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI hightScoreText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateHighScore(int highScore)
    {
        hightScoreText.text = "High Score Text: " + highScore.ToString();
    }
}
