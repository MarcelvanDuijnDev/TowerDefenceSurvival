﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Hex : MonoBehaviour
{
    public HexGridNav hexgridScript;

    public int xas;
    public int zas;
    
    public bool walkeble;
    public int steps;

    void OnMouseUp()
    {
        Debug.Log("Clicked Tile: " + xas.ToString() + " " + zas.ToString());
        //hexgridScript.MoveUnit(xas, zas);
        hexgridScript.GetPath(xas, zas);
    }
}
