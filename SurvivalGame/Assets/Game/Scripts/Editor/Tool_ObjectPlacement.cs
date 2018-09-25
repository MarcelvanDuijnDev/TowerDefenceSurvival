using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

[ExecuteInEditMode]
public class Tool_ObjectPlacement : EditorWindow
{
    private GameObject[] prefabs = new GameObject[10];
    private string[] search_results = new string[0];
    private int selectedID = 999999;

    [MenuItem("Tools/Object Placement")]
    static void Init()
    {
        Tool_ObjectPlacement window = (Tool_ObjectPlacement)EditorWindow.GetWindow(typeof(Tool_ObjectPlacement));
        window.Show();
    }

    void OnGUI()
    {
        Color defaultColor = GUI.backgroundColor;
        GUILayout.BeginVertical("Box");
        GUILayout.BeginVertical("Box");
        GUILayout.BeginScrollView(new Vector2(2,2));
        int x = 0;
        int y = 0;
        for (int i = 0; i < search_results.Length; i++)
        {
            if (prefabs[i] != null)
            {
                if (selectedID == i) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = new Color(1, 0, 0); }
                Texture2D img = AssetPreview.GetAssetPreview(prefabs[i]);
                GUIContent content = new GUIContent();
                content.image = img;
                //content.text = prefabs[i].name;
                if (GUI.Button(new Rect(x * 100, y * 100, 100, 100), content))
                {
                    if (selectedID == i) { selectedID = 9999; } else { selectedID = i; }
                }
                x++;
                if (x == 3)
                {
                    y++;
                    x = 0;
                }
                GUI.backgroundColor = defaultColor;
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        if (GUILayout.Button("Search for prefabs"))
        {
            LoadPrefabs();
        }
        GUILayout.EndVertical();

        if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            Debug.Log("Left-Mouse Up");
        }
    }

    void Update()
    {
        Debug.Log("Working");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Test");
        }
    }

    void OnSceneGUI()
    {
        if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
        {
            Debug.Log("tst");
            //anchorTool myTarget = (anchorTool)target;
        }
    }

    void LoadPrefabs()
    {
        search_results = System.IO.Directory.GetFiles("Assets/Game/Prefabs/", "*.prefab", System.IO.SearchOption.AllDirectories);

        for (int i = 0; i < search_results.Length; i++)
        {
            Object prefab = null;
            prefab = AssetDatabase.LoadAssetAtPath(search_results[i], typeof(GameObject));
            prefabs[i] = prefab as GameObject;
            Debug.Log(prefabs[i].name);
        }
    }

    void CreatePrefab(int prefabID)
    {

        Instantiate(prefabs[prefabID], new Vector3(0,0,0), Quaternion.identity);
    }
}


