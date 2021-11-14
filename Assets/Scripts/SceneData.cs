using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    private string m_name;
    private string m_path;
    private bool m_isActive;
    private bool m_activeAtTheNextFrame;

    //constructor(not used)
    public SceneData()
    {
        m_name = "";
        m_path = "";
        m_isActive = false;
        m_activeAtTheNextFrame = false;
    }

    //parametric constructor
    public SceneData(string name, string path, bool isActive, bool activeAtTheNextFrame)
    {
        m_name = name;
        m_path = path;
        m_isActive = isActive;
        m_activeAtTheNextFrame = activeAtTheNextFrame;
    }

    //getter and setter
    public string GetName()
    {
        return m_name;
    }

    public string GetPath()
    {
        return m_path;
    }

    public bool GetStatut()
    {
        return m_isActive;
    }

    public void SetStatut(bool isActive)
    {
        m_isActive = isActive;
    }

    public bool GetStatutAtTheNextFrame()
    {
        return m_activeAtTheNextFrame;
    }

    public void SetStatutAtTheNextFrame(bool activeAtTheNextFrame)
    {
        m_activeAtTheNextFrame = activeAtTheNextFrame;
    }
}
