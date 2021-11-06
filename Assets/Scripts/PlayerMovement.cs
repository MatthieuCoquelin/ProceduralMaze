using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEditor;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    private float positionSpeed;
    private float rotationSpeed;
    private NavMeshAgent agent;

    [SerializeField]
    private ParticleSystem leftPropultion;
    
    [SerializeField]
    private ParticleSystem rightPropultion;

    // Start is called before the first frame update
    void Start()
    {
        leftPropultion.gameObject.SetActive(false);
        rightPropultion.gameObject.SetActive(false);
        positionSpeed = 2.5f;
        rotationSpeed = 50.0f;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey == false)
        {
            agent.updateRotation = false;
            agent.updatePosition = false;
            //transform.Rotate(Vector3.zero);
            //agent.velocity = Vector3.zero;
            
        }
        else
        {
            if (Input.GetKey("up"))
            {
                Vector3 moveDiretion = Vector3.forward * Time.deltaTime * positionSpeed;
                moveDiretion = transform.TransformDirection(moveDiretion);
                agent.velocity = moveDiretion;
                transform.position += moveDiretion;

                leftPropultion.gameObject.SetActive(true);
                rightPropultion.gameObject.SetActive(true);
            }
            if (Input.GetKey("down"))
            {
                Vector3 moveDiretion = Vector3.forward * Time.deltaTime * positionSpeed;
                moveDiretion = transform.TransformDirection(moveDiretion);
                agent.velocity = moveDiretion;
                transform.position -= moveDiretion;

                leftPropultion.gameObject.SetActive(true);
                rightPropultion.gameObject.SetActive(true);
            }
            if (Input.GetKey("left"))
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);

                leftPropultion.gameObject.SetActive(true);
                rightPropultion.gameObject.SetActive(true);
            }
                
            if (Input.GetKey("right"))
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

                leftPropultion.gameObject.SetActive(true);
                rightPropultion.gameObject.SetActive(true);
            }
                
        }
    }
}
