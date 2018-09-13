using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CustomTerrainEditor : EditorWindow 
{
    public bool activate;
    public Vector3 mousePostition;
    Vector3 worldPosition;


    [MenuItem("Tools/customTerrain")]
    static void Init()
    {
        CustomTerrainEditor customTerrain = (CustomTerrainEditor)EditorWindow.GetWindow(typeof(CustomTerrainEditor));
        customTerrain.autoRepaintOnSceneChange = true;
        customTerrain.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("setActive"))
        {
            activate = !activate;
        }
    }

	void Start () 
    {
		
	}
	
	void Update () 
    {
        if (activate)
        {
            worldPosition = Camera.current.ScreenToWorldPoint(mousePostition);
        }

	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(worldPosition,-1);
    }
}
