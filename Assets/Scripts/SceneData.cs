using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    public string m_name;
    public string m_path;
    public bool m_isActive;
    public bool m_activeAtTheNextFrame;

    public SceneData()
    {
        m_name = "";
        m_path = "";
        m_isActive = false;
        m_activeAtTheNextFrame = false;
    }

    public SceneData(string name, string path, bool isActive, bool activeAtTheNextFrame)
    {
        m_name = name;
        m_path = path;
        m_isActive = isActive;
        m_activeAtTheNextFrame = activeAtTheNextFrame;
    }
}
