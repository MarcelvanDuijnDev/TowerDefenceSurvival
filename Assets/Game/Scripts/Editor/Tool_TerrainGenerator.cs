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

    //Settings
    private string terrainName;
    private Vector3 terrainSize;

    //Generate
    private float gen_Height;
    private float gen_Mountains;
    private float gen_Detail;

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


                terrainSize.x = EditorGUILayout.FloatField("Size X: ", terrainSize.x);
                terrainSize.z = EditorGUILayout.FloatField("Size Z: ", terrainSize.z);

                gen_Height = EditorGUILayout.FloatField("Height: ", gen_Height);

                gen_Mountains = EditorGUILayout.Slider("Mountains", gen_Mountains, 0, 100);
                gen_Detail = EditorGUILayout.Slider("Detail",gen_Detail, 0, 100);

                if (GUILayout.Button("Generate"))
                {
                    Generator();
                }
            }
            if(terrainTab == 2)
            {
                GUILayout.Label("Draw");
            }
        }
        GUILayout.EndVertical();
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

    void Generator()
    {
        TerrainData terrainData = new TerrainData();

        terrainData.size = new Vector3(terrainSize.x * 0.1f, gen_Height, terrainSize.z * 0.1f);
        terrainData.heightmapResolution = 512;
        terrainData.baseMapResolution = 1024;
        terrainData.SetDetailResolution(1024, 10);

        int _heightmapWidth = terrainData.heightmapResolution;
        int _heightmapHeight = terrainData.heightmapResolution;

        terrain.terrainData = terrainData;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(selectionObj.transform.position, 1);
    }
}
