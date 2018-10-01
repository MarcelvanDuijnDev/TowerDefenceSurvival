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
    private float collomLength = 4;

    private int placementOption = 0;
    private int createOptions = 0;

    private Texture2D[] prefabImg = new Texture2D[0];
    private GameObject parentObject;

    private GameObject exampleObj;
    private int checkSelectedID = 999999999;

    Vector2 scrollPos;

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

        collomLength = position.width / 100;

        int x = 0;
        int y = 0;
        scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width - 20), GUILayout.Height(position.height - 150));
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
                if (x >= collomLength - 1)
                {
                    y++;
                    x = 0;
                }
                GUI.backgroundColor = defaultColor;
            }
        }
        GUILayout.Space(y * 100);

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.BeginVertical("Box");
        placementOption = GUILayout.Toolbar(placementOption, new string[] { "Click", "Paint"});
        createOptions = GUILayout.Toolbar(createOptions, new string[] { "Free", "Parent" });
        if(createOptions == 1)
        {
            parentObject = (GameObject)EditorGUILayout.ObjectField("Parent Object: ", parentObject, typeof(GameObject), true);
        }
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
        DestroyImmediate(exampleObj);
    }

    void OnSceneGUI(SceneView sceneView)
    {
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(worldRay, out hitInfo))
        {
            if (selectedID <= prefabs.Length)
            {
                if (checkSelectedID != selectedID)
                {
                    DestroyImmediate(exampleObj);
                    exampleObj = Instantiate(prefabs[selectedID], hitInfo.point, Quaternion.identity);
                    exampleObj.layer = LayerMask.NameToLayer("Ignore Raycast");
                    for (int i = 0; i < exampleObj.transform.childCount; i++)
                    {
                        exampleObj.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                        for (int o = 0; o < exampleObj.transform.GetChild(i).childCount; o++)
                        {
                            exampleObj.transform.GetChild(i).GetChild(o).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                        }
                    }
                    exampleObj.name = "Example Object";
                    checkSelectedID = selectedID;
                }
            }
            if (exampleObj != null)
            {
                exampleObj.transform.position = hitInfo.point;
            }

            if (!Event.current.alt)
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
    }

    void LoadPrefabs()
    {
        search_results = System.IO.Directory.GetFiles("Assets/", "*.prefab", System.IO.SearchOption.AllDirectories);
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
        search_results = System.IO.Directory.GetFiles("Assets/", "*.prefab", System.IO.SearchOption.AllDirectories);

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
        GameObject createdObj = Instantiate(prefabs[selectedID], createPos, Quaternion.identity);
        if (createOptions == 1)
        {
            createdObj.transform.parent = parentObject.transform;
        }
    }
}
 