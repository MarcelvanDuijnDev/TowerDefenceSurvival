using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour 
{
	void Update () 
    {
        transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
	}
}
