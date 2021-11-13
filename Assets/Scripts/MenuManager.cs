using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject optionPanel;

    [SerializeField]
    private TextMeshProUGUI mode;

    private string difficulty;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        mode.text = difficulty;
    }

    //*****************MainMenu*****************

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Option()
    {
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        print("The Application.Quit call is ignored in the Editor.");
    }

    //*****************Option*****************

    public void Easy()
    {
        PlayerPrefs.SetString("Difficulty", "Easy");
        mode.text = "Easy";
    }

    public void Normal()
    {
        PlayerPrefs.SetString("Difficulty", "Normal");
        mode.text = "Normal";
    }

    public void Difficult()
    {
        PlayerPrefs.SetString("Difficulty", "Difficult");
        mode.text = "Difficult";
    }

    public void Random()
    {
        PlayerPrefs.SetString("Difficulty", "Random");
        mode.text = "Random";
    }

    public void Return()
    {
        optionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
}

