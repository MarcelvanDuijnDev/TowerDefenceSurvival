﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HexGridNav : MonoBehaviour
{
    public GameObject unit;
    private Unit unitScript;

    public GameObject hexParent;
    private GameObject[] hex;
    public int hexLenght;

    private int[,] tiles;
    private TextMeshPro[] textMesh;
    public Hex[] hexType;

	void Start ()
    {
        unitScript = unit.GetComponent<Unit>();

        hex = new GameObject[hexParent.transform.childCount];
        textMesh = new TextMeshPro[hexParent.transform.childCount];
        tiles = new int[hexLenght, hexLenght];

        for (int i = 0; i < hexParent.transform.childCount; i++)
        {
            hex[i] = hexParent.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < hexLenght; i++)
        {
            for (int o = 0; o < hexLenght; o++)
            {
                int calc = i * hexLenght + o;
                hex[calc].name = i.ToString() + " " + o.ToString();
                tiles[i, o] = 0;

                Hex hexScript = hex[calc].GetComponent<Hex>();
                hexScript.xas = i;
                hexScript.zas = o;
                hexScript.hexgridScript = this;

                textMesh[calc] = hexParent.transform.GetChild(calc).transform.GetChild(0).GetComponent<TextMeshPro>();
                textMesh[calc].text = i.ToString() + " " + o.ToString();
            }
        }
	}

    public Vector3 TileCoordToWorldCoord(int x,int z)
    {

        return new Vector3(x * 1.7321f, 0, z * -1.5f);
    }
	
    public void MoveUnit(int x, int z)
    {
        unit.transform.position = TileCoordToWorldCoord(x,z);
        unit.GetComponent<Unit>().tileX = x;
        unit.GetComponent<Unit>().tileZ = z;
        //unit.transform.position = new Vector3(x * 1.7321f, 0.5f, z * -1.5f);
    }

    public void GetPath(int x, int z)
    {
        Debug.Log("GetPath");

        Vector2 currenTile = new Vector2(5,4);
        Vector2 currentTileCalc = new Vector2(5,4);
        Vector2 finalDestenation = new Vector2(0,0);

        int stepCount = 0;
        List<Vector2> steps = new List<Vector2>();
        Vector2 getNextStep = new Vector2(unitScript.tileX,unitScript.tileZ);
        List<Vector2> getTile = new List<Vector2>();
        bool final = false;

        while (!final)
        {
            //Right
            if (finalDestenation.x > currenTile.x)
            {
                getNextStep.x += 1;
            }
            if (finalDestenation.x <= currenTile.x && finalDestenation.y > currenTile.y)
            {
                getNextStep.y += 1;
            }
            if (finalDestenation.x <= currenTile.x && finalDestenation.y < currenTile.y)
            {
                getNextStep.y -= 1;
            }
            //Left
            if (finalDestenation.x < currenTile.x && finalDestenation.y > currenTile.y)
            {
                getNextStep.x -= 1;
                getNextStep.y += 1;
            }
            if (finalDestenation.x < currenTile.x && finalDestenation.y < currenTile.y)
            {
                getNextStep.x -= 1;
                getNextStep.y -= 1;
            }
            if (finalDestenation.x < currenTile.x)
            {
                getNextStep.x -= 1;
            }

            steps.Add(getNextStep);
            currenTile = getNextStep;
            currentTileCalc = getNextStep;
            getTile.Add(currentTileCalc);
            stepCount++;

            if (currenTile == finalDestenation)
            {
                for (int i = 0; i < steps.Count; i++)
                {
                    Debug.Log(getTile[i]);
                    for (int o = 0; o < hexType.Length; o++)
                    {
                        if (hexType[o].xas == getTile[i].x && hexType[o].zas == getTile[i].y)
                        {
                            Debug.Log("123");
                            //hex[o].GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                }
                final = true;
            }
        }

    }

	void Update ()
    {
		
	}
}