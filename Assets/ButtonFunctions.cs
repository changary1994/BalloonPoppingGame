using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Instructions()
    {
        SceneManager.LoadScene("instructions");
    }

    public void PlayGame()
    {
        
        string s = playerNameInput.text;
        PersistentData.Instance.SetName(s);
        PersistentData.Instance.SetScore(0);
        SceneManager.LoadScene("level1");
        Time.timeScale = 1.0f;

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }
    public void HiScores()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            string s = playerNameInput.text;
            PersistentData.Instance.SetName(s);
            PersistentData.Instance.SetScore(0);
        }
 
        SceneManager.LoadScene("highScores");
    }

    public void Ending()
    {
        SceneManager.LoadScene("ending");
    }
}
