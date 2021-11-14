using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;

    private static float m_timer;
    private static float m_timerCopy;


    // Start is called before the first frame update
    void Start()
    {
        m_timer = 0.0f;
        m_timerCopy = m_timer;
    }

    // Update is called once per frame
    void Update()
    {
        //if an asteroid is destroyed we stop the timer(we delay the timer and we use the timer copy for the display)
        if((int)m_timer == (int)m_timerCopy)
        {
            m_timer += Time.deltaTime;
            m_timerCopy = m_timer;

            m_text.color = Color.white;
            m_text.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(m_timer / 60), Mathf.FloorToInt(m_timer % 60));
        }
        else
        {
            m_timer += Time.deltaTime;

            m_text.color = Color.red;
            m_text.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(m_timerCopy / 60), Mathf.FloorToInt(m_timerCopy % 60));
        }
    }

    public static void StopTimer()
    {
        m_timer -= 10.0f;
    }

    public static float GetTimer()
    {
        return m_timerCopy;
    }
}
