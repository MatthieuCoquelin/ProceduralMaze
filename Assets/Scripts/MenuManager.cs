using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_proceduralMazePanel;

    [SerializeField]
    private GameObject m_optionPanel;

    [SerializeField]
    private TextMeshProUGUI m_mode;

    private string m_difficulty;

    // Start is called before the first frame update
    void Start()
    {
        //get the difficulty from the PlayerPrefs
        m_difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        m_mode.text = m_difficulty;
    }

    //methods called by clicking on the different buttons:

    //*****************MainMenu*****************

    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Option()
    {
        m_proceduralMazePanel.SetActive(false);
        m_optionPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        print("The Application.Quit call is ignored in the Editor.");
    }

    //*****************Option*****************

    //set dificulty in PlayerPrefs
    public void Easy()
    {
        PlayerPrefs.SetString("Difficulty", "Easy");
        m_mode.text = "Easy";
    }

    public void Normal()
    {
        PlayerPrefs.SetString("Difficulty", "Normal");
        m_mode.text = "Normal";
    }

    public void Difficult()
    {
        PlayerPrefs.SetString("Difficulty", "Difficult");
        m_mode.text = "Difficult";
    }

    public void Random()
    {
        PlayerPrefs.SetString("Difficulty", "Random");
        m_mode.text = "Random";
    }

    public void Return()
    {
        m_optionPanel.SetActive(false);
        m_proceduralMazePanel.SetActive(true);
    }
    
}

