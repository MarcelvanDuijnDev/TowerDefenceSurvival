using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    ObjectPool objectPoolScript;
    public GameObject objectPool;

	void Start () 
    {
        objectPoolScript = (ObjectPool)objectPool.GetComponent(typeof(ObjectPool));
	}

    void Fire()
    {
        for (int i = 0; i < objectPoolScript.objects.Count; i++)
        {
            if (!objectPoolScript.objects[i].activeInHierarchy)
            {
                objectPoolScript.objects[i].transform.position = transform.position;
                objectPoolScript.objects[i].transform.rotation = transform.rotation;
                objectPoolScript.objects[i].SetActive(true);
                break;
            }
        }
    }
}
