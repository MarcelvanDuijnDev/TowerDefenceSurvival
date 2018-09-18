using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool1 : MonoBehaviour
{
    [Header("Amount")]
    public int pooledAmount;

    [Header("Object")]
    public GameObject prefabObj;

    [HideInInspector]public List<GameObject> objects;

    void Start()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefabObj);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            objects.Add(obj);
        }
    }
}
