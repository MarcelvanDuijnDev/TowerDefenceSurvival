using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;


public class Tool_ObjectPlacement : EditorWindow
{
    private GameObject[] prefabs = new GameObject[0];
    private string[] search_results = new string[0];
    private int selectedID = 99999999;
    private int collomLength = 4;

    private Texture2D[] prefabImg = new Texture2D[0];

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
        collomLength = EditorGUILayout.IntField("Collom Length", collomLength);
        GUILayout.BeginVertical("Box");
        GUILayout.BeginScrollView(new Vector2(2,2));
        int x = 0;
        int y = 0;
        for (int i = 0; i < search_results.Length; i++)
        {
            if (prefabs[i] != null)
            {
                if (selectedID == i) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = new Color(1, 0, 0); }
                GUIContent content = new GUIContent();
                content.image = prefabImg[i];
                if (GUI.Button(new Rect(x * 100, y * 100, 100, 100), content))
                {
                    if (selectedID == i) { selectedID = 99999999; } else { selectedID = i; }
                }
                x++;
                if (x == collomLength)
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
        if (GUILayout.Button("Fix prefabs"))
        {
            FixPreview();
        }
        GUILayout.EndVertical();
    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        Debug.Log("OnSceneGUI");
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(worldRay, out hitInfo))
        {
            if (selectedID != 99999999)
            {
                if (Event.current.type == EventType.Layout)
                {
                    HandleUtility.AddDefaultControl(0);
                }

                if (Event.current.shift)
                {
                    CreatePrefab(hitInfo.point);
                }

                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                    CreatePrefab(hitInfo.point);
                }
            }
        }
    }

    void LoadPrefabs()
    {
        search_results = System.IO.Directory.GetFiles("Assets/Game/Prefabs/", "*.pref?b", System.IO.SearchOption.AllDirectories);
        prefabs = new GameObject[search_results.Length];
        prefabImg = new Texture2D[search_results.Length];

        for (int i = 0; i < search_results.Length; i++)
        {
            Object prefab = null;
            prefab = AssetDatabase.LoadAssetAtPath(search_results[i], typeof(GameObject));
            prefabs[i] = prefab as GameObject;

            prefabImg[i] = AssetPreview.GetAssetPreview(prefabs[i]);
        }
    }

    void FixPreview()
    {
        search_results = System.IO.Directory.GetFiles("Assets/Game/Prefabs/", "*.pref?b", System.IO.SearchOption.AllDirectories);

        for (int i = 0; i < search_results.Length; i++)
        {
            if (prefabImg[i] == null)
            {
                AssetDatabase.ImportAsset(search_results[i]);
                Debug.Log("Scan");
            }
        }
    }

    void CreatePrefab(Vector3 createPos)
    {

        Instantiate(prefabs[selectedID], createPos, Quaternion.identity);
    }
}
 