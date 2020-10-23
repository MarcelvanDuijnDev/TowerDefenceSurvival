using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private GameObject particleImpact;
    float timer = 10;
	
	void Update () 
    {
        timer -= 1 * Time.deltaTime;
        if (timer <= 0)
        {
            this.gameObject.SetActive(false);
        }
	}

    void OnEnable()
    {
        timer = 10;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Untagged")
        {
            this.gameObject.SetActive(false);
            GameObject obj = (GameObject)Instantiate(particleImpact);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }
    }
}
