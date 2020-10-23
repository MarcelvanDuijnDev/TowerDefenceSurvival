using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour 
{
    public string m_Type;
    public int m_Amount;
	
	void Update () 
    {
        if (m_Amount <= 0)
        {
            gameObject.SetActive(false);
        }
	}
}
