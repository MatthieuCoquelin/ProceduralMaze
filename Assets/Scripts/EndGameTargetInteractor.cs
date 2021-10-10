using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTargetInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("Congratulation");
            Application.LoadLevel(0);
        }
    }
}
