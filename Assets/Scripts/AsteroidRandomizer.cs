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
        m_randomSpeed = 0.0f;
        m_randomDirection = Vector3.zero;

        ScaleRandomizer();
        SpeedRandomizer();
        DirectionRandomizer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_randomDirection * m_randomSpeed * Time.deltaTime);
    }

    //Randomize the scale of the asteroid with a value between 0.7 and 1.30
    void ScaleRandomizer()
    {
        m_randomScale = Random.Range(0.7f, 1.31f);
        transform.localScale = new Vector3(transform.localScale.x * m_randomScale, transform.localScale.y * m_randomScale, transform.localScale.z * m_randomScale);
    }

    //Randomize the rotation speed of the asteroid with a value between 0.1 and 0.5 
    void SpeedRandomizer()
    {
        m_randomSpeed = Random.Range(0.1f, 0.6f);
    }

    //Randomize the direction of the asteroid's rotation with a value between -180 and 179
    void DirectionRandomizer()
    {
        m_randomDirection = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
    }
}
