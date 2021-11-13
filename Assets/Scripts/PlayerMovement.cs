using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEditor;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    private float m_positionSpeed;
    private float m_rotationSpeed;
    private NavMeshAgent m_agent;
    private float m_projectileSpeed;
    private float m_positionSpeedBoost;

    [SerializeField]
    private ParticleSystem m_leftPropultion;
    
    [SerializeField]
    private ParticleSystem m_rightPropultion;

    [SerializeField]
    private GameObject m_projectile;

    [SerializeField]
    private Transform m_origin;

    [SerializeField]
    private GameObject m_map;

    // Start is called before the first frame update
    void Start()
    {
        m_leftPropultion.gameObject.SetActive(false);
        m_rightPropultion.gameObject.SetActive(false);
        m_positionSpeed = 2.5f;
        m_rotationSpeed = 50.0f;
        m_agent = GetComponent<NavMeshAgent>();
        m_projectileSpeed = 1000.0f;
        m_positionSpeedBoost = 1.0f;
        m_map.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey == false)
        {
            m_agent.updateRotation = false;
            m_agent.updatePosition = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                m_map.SetActive(!m_map.activeSelf);

            if(Input.GetMouseButtonDown(0))
            {
                GameObject instance = Instantiate(m_projectile, m_origin.position, Quaternion.identity);
                instance.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * m_projectileSpeed * Time.deltaTime);    
            }

            if (Input.GetKey(KeyCode.Z))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    m_positionSpeedBoost = 1.5f;
                else
                    m_positionSpeedBoost = 1.0f;

                Vector3 moveDiretion = Vector3.forward * Time.deltaTime * m_positionSpeed * m_positionSpeedBoost;
                moveDiretion = transform.TransformDirection(moveDiretion);
                m_agent.velocity = moveDiretion;
                transform.position += moveDiretion;

                m_leftPropultion.gameObject.SetActive(true);
                m_rightPropultion.gameObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Vector3 moveDiretion = Vector3.forward * Time.deltaTime * m_positionSpeed;
                moveDiretion = transform.TransformDirection(moveDiretion);
                m_agent.velocity = moveDiretion;
                transform.position -= moveDiretion;

                m_leftPropultion.gameObject.SetActive(true);
                m_rightPropultion.gameObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(-Vector3.up * m_rotationSpeed * Time.deltaTime);

                m_leftPropultion.gameObject.SetActive(true);
                m_rightPropultion.gameObject.SetActive(true);
            }
                
            if (Input.GetKey(KeyCode.D))
            {
                
                transform.Rotate(Vector3.up * m_rotationSpeed * Time.deltaTime);

                m_leftPropultion.gameObject.SetActive(true);
                m_rightPropultion.gameObject.SetActive(true);
            }
                
        }
    }
}
