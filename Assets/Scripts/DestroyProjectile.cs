using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //if collion is an asteroid, we destroy the asteroid...
        if (collision.gameObject.tag == "Asteroid")
        {
            Timer.StopTimer();
            Destroy(collision.gameObject);
        }
        //...and we destroy the projectile    
        Destroy(gameObject);
    }
}
