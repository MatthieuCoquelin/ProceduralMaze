using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotator : MonoBehaviour
{
    private float m_rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        m_rotationSpeed = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
    }
}
