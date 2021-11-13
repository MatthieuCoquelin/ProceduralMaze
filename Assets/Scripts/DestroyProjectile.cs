using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Timer.StopTimer();
            Destroy(collision.gameObject);
        }
            
        Destroy(gameObject);
    }
}
