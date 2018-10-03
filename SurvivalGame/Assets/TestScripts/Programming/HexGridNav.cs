using System.Collections;
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

    private int currentStep;
    private List<Vector2> stepsFinal = new List<Vector2>();

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
        stepsFinal.Clear();
        Debug.Log("GetPath");

        Vector2 currenTile = new Vector2(unitScript.tileX, unitScript.tileZ);
        Vector2 finalDestenation = new Vector2(x, z);

        List<Vector2> steps = new List<Vector2>();
        Vector2 getNextStep = new Vector2(unitScript.tileX,unitScript.tileZ);
        List<Vector2> getTile = new List<Vector2>();

        for (int i = 0; i < 6; i++)
        {

        }


        /* Left Bottom
        -1 1
        -1 0
        -1 -1
        0 -1
        1 0
        0 1
        */
        stepsFinal = getTile;
    }

    public void SetStep()
    {
        if (stepsFinal.Count >= 0)
        {
            float offset = 0;
            if (stepsFinal[currentStep].y % 2 == 1)
            {
                offset = 0.86605f * 1;
            }
            else
            {
                offset = 0;
            }
            unit.transform.position = new Vector3(stepsFinal[currentStep].x * 1.7321f + offset, 0.5f, stepsFinal[currentStep].y * -1.5f);
        }
        currentStep += 1;
    }


    void Update ()
    {
		
	}
}
