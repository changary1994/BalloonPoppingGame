using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] float score = 0;
    int pops = 0;
    const float DEFAULT_POINTS = 14;
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] TMP_Text levelTxt;
    [SerializeField] TMP_Text nameTxt;
    [SerializeField] int level;
    float penalty = 0f;
    const int POP_THRESHOLD_PER_LEVEL = 5;
    //[SerializeField] int scoreThresholdForThisLevel;

    // Start is called before the first frame update
    void Start()
    {
        score = PersistentData.Instance.GetScore();
        level = SceneManager.GetActiveScene().buildIndex;
        //scoreThresholdForThisLevel = SCORE_THRESHOLD_PER_LEVEL;


        DisplayScore();
        DisplayLevel();
        DisplayName();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();
    }

    public void AddPoints(float points)
    {
        penalty = Time.timeSinceLevelLoad;
        points = System.MathF.Floor(points - penalty);
        score += points;
        pops = pops + 1;
        Debug.Log("score " + score);
        Debug.Log("Earned " + points + " points");
        DisplayScore();
        PersistentData.Instance.SetScore(score);

        if (pops >= POP_THRESHOLD_PER_LEVEL)
        {
            //move on to next level
            SceneManager.LoadScene(level + 1);
        }

    }
    public void AddPoints()
    {
        AddPoints(DEFAULT_POINTS);
    }
    public void DisplayScore()
    {
        scoreTxt.text = "Score: " + score;
    }

    public void DisplayLevel()
    {
        int levelToDisplay = level;
        levelTxt.text = "Level " + levelToDisplay;
    }

    public void DisplayName()
    {
        nameTxt.text = "Player: " + PersistentData.Instance.GetName();
    }
}