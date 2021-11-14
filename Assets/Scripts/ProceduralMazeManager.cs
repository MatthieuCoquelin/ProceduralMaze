using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProceduralMazeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_pausedMenuCanvas;

    
    //Be sure that the game is not stoped
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if we press "Escape" button and we are not at the end of the game  
        if (Input.GetKeyDown(KeyCode.Escape) && EndGameTargetInteractor.GetEndGame() == false)
        {
            Resume();
        }
    }

    //methods called by clicking on the different buttons:


    //*****************Paused*****************

    public void Resume()
    {
        m_pausedMenuCanvas.SetActive(!m_pausedMenuCanvas.activeSelf);
        if (m_pausedMenuCanvas.activeSelf)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
