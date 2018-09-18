using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseObjectPool : MonoBehaviour
{
    [SerializeField]private ObjectPool1 objectPoolScript;

    void Use()
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
