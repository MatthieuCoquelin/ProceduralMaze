using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private float positionSpeed;
    private float rotationSpeed;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
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
            }
            if (Input.GetKey("down"))
            {
                Vector3 moveDiretion = Vector3.forward * Time.deltaTime * positionSpeed;
                moveDiretion = transform.TransformDirection(moveDiretion);
                agent.velocity = moveDiretion;
                transform.position -= moveDiretion;
            }
            if (Input.GetKey("left"))
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            if (Input.GetKey("right"))
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
