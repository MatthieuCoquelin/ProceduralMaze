using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[AddComponentMenu("test/EndGameTargetInteractor")]
public class EndGameTargetInteractor : MonoBehaviour
{
    [SerializeField]
    private GameObject m_canvas;

    [SerializeField]
    private GameObject m_endGamePanel;

    [SerializeField]
    private GameObject m_creditsPanel;

    [SerializeField]
    private GameObject m_timeText;

    private static bool m_endGame;

    // Start is called before the first frame update
    private void Start()
    {
        m_endGame = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Time.timeScale = 0.0f;

            m_endGame = true;

            m_canvas.SetActive(true);
            m_endGamePanel.SetActive(true);
            m_creditsPanel.SetActive(false);

            m_timeText.GetComponent<TextMeshProUGUI>().text = "Time: " + string.Format("{0:00}:{1:00}", Mathf.FloorToInt(Timer.GetTimer() / 60), Mathf.FloorToInt(Timer.GetTimer() % 60));
        }
    }

    public static bool GetEndGame()
    {
        return m_endGame;
    }

    //*****************EndGame*****************

    public void NewGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Credits()
    {
        m_endGamePanel.SetActive(false);
        m_creditsPanel.SetActive(true);
    }

    //*****************Credits*****************

    public void Return()
    {
        m_creditsPanel.SetActive(false);
        m_endGamePanel.SetActive(true);
    }

}
