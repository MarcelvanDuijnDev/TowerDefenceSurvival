using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = new Vector3(0,24,0);
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}
