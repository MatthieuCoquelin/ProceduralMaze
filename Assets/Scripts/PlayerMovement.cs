using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        speed = 50.0f;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDiretion = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
        moveDiretion = transform.TransformDirection(moveDiretion);
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f) * speed * Time.deltaTime);
        controller.Move(moveDiretion * 2 * Time.deltaTime);

        //if (Input.GetKey("up"))
        //{

        //    transform.position += Vector3.forward * Time.deltaTime;
        //}
        //if(Input.GetKey("down"))
        //{
        //    transform.position -= Vector3.forward * Time.deltaTime;
        //}
        //if(Input.GetKey("left"))
        //{
        //    transform.Rotate(-Vector3.up * speed * Time.deltaTime);
        //    //transform.
        //}
        //if(Input.GetKey("right"))
        //{
        //    transform.Rotate(Vector3.up * speed * Time.deltaTime);
        //}
    }
}
