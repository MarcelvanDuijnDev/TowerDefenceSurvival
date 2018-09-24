using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Tool_TerrainGenerator : EditorWindow 
{
    private Terrain terrain;
    private GameObject selectionObj;

    private bool active;
    private Vector3 mousePostition;
    Vector3 worldPosition;

    private int terrainTab;

    private Vector3 terrainSize;

    [MenuItem("Tools/Terrain")]
    static void Init()
    {
        Tool_TerrainGenerator customTerrain = (Tool_TerrainGenerator)EditorWindow.GetWindow(typeof(Tool_TerrainGenerator));
        customTerrain.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical("box");
        if (terrain == null)
        {
            GUILayout.Label("Select Terrain");
        }
        else
        {
            terrainTab = GUILayout.Toolbar(terrainTab, new string[] { "Settings", "Generate", "Draw" });

            if (terrainTab == 0)
            {
                GUILayout.Label("Settings");

                terrainSize.x = EditorGUILayout.FloatField("Size X: ", terrainSize.x);
                terrainSize.z = EditorGUILayout.FloatField("Size Z: ", terrainSize.z);

                if (GUILayout.Button("Confirm"))
                {
                    terrain.terrainData.size = terrainSize;
                }
            }
            if(terrainTab == 1)
            {
                GUILayout.Label("Generate");
            }
            if(terrainTab == 2)
            {
                GUILayout.Label("Draw");
            }

        }
        GUILayout.EndVertical();
    }

	void Start () 
    {
		
	}

    void Update()
    {
        selectionObj = Selection.activeGameObject;
        try
        {
            if (terrain == null && selectionObj.GetComponent<Terrain>() != null)
            {
                terrain = selectionObj.GetComponent<Terrain>();
                active = true;
            }
        }
        catch
        {
            active = false;
            terrain = null;
        }

        if (active)
        {



            //OnDrawGizmos();
        }
    }

    void GetInfo()
    {
        terrainSize = terrain.terrainData.size;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(selectionObj.transform.position, 1);
    }
}
