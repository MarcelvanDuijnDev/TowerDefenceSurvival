using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class Tool_ObjectPlacement : EditorWindow
{
    //Prefab Array
    private GameObject[] prefabs = new GameObject[0];
    private string[] search_results = new string[0];

    //Array Options
    private string searchPrefab = "";
    private bool hideNames = true;

    //Array Selection
    private float collomLength = 4;
    private int selectedID = 99999999;
    private int checkSelectedID = 999999999;

    //Other
    private Vector2 scrollPos1;
    private Texture2D[] prefabImg = new Texture2D[0];

    //Options
    private int showOption = 0;
    private int placementOption = 0;
    private int createOptions = 0;

    //Placement
    private GameObject parentObject;
    private GameObject exampleObj;

    //Placement Option
    private float paintSpeed = 1;
    private float timer1 = 0;

    //Check Buttons Event
    private bool mouseDown;
    private bool mouseUp;

    //Rotation
    private float snapRot;
    private bool randomRot = true;
    private Vector3 rotation;

    //Position
    private Vector3 snapPos;
    private Vector3 objPosition;

    [MenuItem("Tools/Object Placement")]
    static void Init()
    {
        Tool_ObjectPlacement window = (Tool_ObjectPlacement)EditorWindow.GetWindow(typeof(Tool_ObjectPlacement));
        window.Show();
    }

    void OnGUI()
    {
        Color defaultColor = GUI.backgroundColor;

        //Prefabs
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal();
        showOption = GUILayout.Toolbar(showOption, new string[] { "Icon", "Text" });
        if (!hideNames)
        {
            if (GUILayout.Button("Hide Names", GUILayout.Width(100)))
            {
                hideNames = true;
            }
        }
        else
        {
            if (GUILayout.Button("Show Names", GUILayout.Width(100)))
            {
                hideNames = false;
            }
        }
        GUILayout.EndHorizontal();
        searchPrefab = EditorGUILayout.TextField("Search: ", searchPrefab);
        GUILayout.BeginVertical("Box");
        collomLength = position.width / 100;
        int x = 0;
        int y = 0;
        scrollPos1 = GUILayout.BeginScrollView(scrollPos1, GUILayout.Width(position.width - 20), GUILayout.Height(position.height - 300));
        for (int i = 0; i < search_results.Length; i++)
        {
            if (prefabs[i] != null && prefabs[i].name.ToLower().Contains(searchPrefab.ToLower()))
            {
                if (showOption == 0)
                {
                    if (selectedID == i) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = new Color(1, 0, 0); }
                    GUIContent content = new GUIContent();
                    content.image = prefabImg[i];
                    GUI.skin.button.imagePosition = ImagePosition.ImageAbove;
                    if (!hideNames)
                    {
                        content.text = prefabs[i].name;
                    }
                    if (GUI.Button(new Rect(x * 100, y * 100, 100, 100), content))
                    {
                        if (selectedID == i) { selectedID = 99999999; DestroyImmediate(exampleObj); } else { selectedID = i; }
                    }
                    x++;
                    if (x >= collomLength - 1)
                    {
                        y++;
                        x = 0;
                    }
                    GUI.backgroundColor = defaultColor;
                }
                else
                {
                    if (selectedID == i) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = defaultColor; }
                    if (GUILayout.Button(prefabs[i].name))
                    {
                        if (selectedID == i) { selectedID = 99999999; DestroyImmediate(exampleObj); } else { selectedID = i; }
                    }
                    GUI.backgroundColor = defaultColor;
                }
            }
        }
        if (showOption == 0)
        {
            GUILayout.Space(y * 100);
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.EndVertical();

        //Options
        GUILayout.BeginVertical("Box");
        GUILayout.BeginVertical("Box");
        placementOption = GUILayout.Toolbar(placementOption, new string[] { "Click", "Paint"});
        if(placementOption == 1)
        {
            paintSpeed = EditorGUILayout.FloatField("Paint Speed: ", paintSpeed);
        }
        createOptions = GUILayout.Toolbar(createOptions, new string[] { "Free", "Parent" });
        if(createOptions == 1)
        {
            parentObject = (GameObject)EditorGUILayout.ObjectField("Parent Object: ", parentObject, typeof(GameObject), true);
            if (GUILayout.Button("Clean Parent"))
            {
                int childAmount = parentObject.transform.childCount;
                int childCalc = childAmount - 1;
                for (int i = 0; i < childAmount; i++)
                {
                    DestroyImmediate(parentObject.transform.GetChild(childCalc).gameObject);
                    childCalc -= 1;
                }
            }
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
        GUILayout.BeginVertical("Box");
        snapPos = EditorGUILayout.Vector3Field("Snap Position: ", snapPos);
        snapRot = EditorGUILayout.FloatField("Snap Rotation: ", snapRot);
        randomRot = EditorGUILayout.Toggle("Random Rotation: ", randomRot);
        GUILayout.EndVertical();
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
                //objPosition = hitInfo.point;


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

                    //if (Event.current.shift)
                    //{
                    //    CreatePrefab(hitInfo.point);
                    //}

                    if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                    {
                        mouseDown = true;
                    }
                    if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
                    {
                        mouseDown = false;
                        mouseUp = true;
                    }


                    if (placementOption == 0)
                    {
                        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                        {
                            CreatePrefab(hitInfo.point);
                        }
                    }
                    else
                    {
                        float timer1Final = paintSpeed;
                        if (mouseDown)
                        {
                            timer1 += 1 * Time.deltaTime;
                            if (timer1 >= timer1Final)
                            {
                                CreatePrefab(hitInfo.point);
                                timer1 = 0;
                            }
                        }
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
        if(randomRot)
        {
            createdObj.transform.rotation = Quaternion.EulerAngles(0,Random.Range(0,360),0);
        }
    }
}
 