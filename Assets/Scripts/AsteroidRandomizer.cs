using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRandomizer : MonoBehaviour
{
    private float m_randomScale;
    private Vector3 m_randomDirection;
    private float m_randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_randomScale = 0.0f;
        m_randomDirection = Vector3.zero;
        m_randomSpeed = 0.0f;

        ScaleRandomizer();
        SpeedRandomizer();
        DirectionRandomizer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_randomDirection * m_randomSpeed * Time.deltaTime);
    }

    void ScaleRandomizer()
    {
        m_randomScale = Random.Range(0.7f, 1.31f);
        transform.localScale = new Vector3(transform.localScale.x * m_randomScale, transform.localScale.y * m_randomScale, transform.localScale.z * m_randomScale);
    }

    void SpeedRandomizer()
    {
        m_randomSpeed = Random.Range(0.1f, 0.6f);
    }

    void DirectionRandomizer()
    {
        m_randomDirection = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
    }
}
