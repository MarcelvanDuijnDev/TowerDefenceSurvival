using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour 
{
    public string Type;
    public int Amount;
	
	void Update () 
    {
        if (Amount <= 0)
        {
            gameObject.SetActive(false);
        }
	}
}
