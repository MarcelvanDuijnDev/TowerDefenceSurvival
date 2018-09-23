using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridNav : MonoBehaviour
{
    public GameObject unit;

    public GameObject hexParent;
    private GameObject[] hex;
    public int hexLenght;

    private int[,] tiles;
    public Hex[] hexType;

	void Start ()
    {
        hex = new GameObject[hexParent.transform.childCount];
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

	void Update ()
    {
		
	}
}
