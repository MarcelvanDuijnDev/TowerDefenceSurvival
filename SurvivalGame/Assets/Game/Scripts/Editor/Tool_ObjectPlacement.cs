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
    private float imageSize = 1;

    //Array Selection
    private float collomLength = 4;
    private int selectedID = 99999999;
    private int checkSelectedID = 999999999;

    //Other
    private Vector2 scrollPos1;
    private Texture2D[] prefabImg = new Texture2D[0];

    //Options
    private bool windowHideOptions = true;
    private int showOption = 0;
    private int placementOption = 0;

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
    private Vector3 mousePos;
    private Vector3 snapPos;
    private Vector3 objPosition;

    //Onscene Options
    private bool showOptionsOnScreen;
    private int OnscreenSelectedID;

    [MenuItem("Tools/Object Placement")]
    static void Init()
    {
        Tool_ObjectPlacement window = CreateInstance<Tool_ObjectPlacement>();
        window.title = "ObjectPlacement";
        window.Show();
    }

    void OnGUI()
    {
        Color defaultColor = GUI.backgroundColor;

        #region List Prefabs
        //Prefabs
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal();
        showOption = GUILayout.Toolbar(showOption, new string[] { "Icon", "Text" });
        imageSize = EditorGUILayout.Slider(imageSize, 0.25f, 2);
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
        float calcWidth = 100 * imageSize;
        collomLength = position.width / calcWidth;
        int x = 0;
        int y = 0;
        if (!windowHideOptions)
        {
            scrollPos1 = GUILayout.BeginScrollView(scrollPos1, GUILayout.Width(position.width - 20), GUILayout.Height(position.height - 300));
        }
        else
        {
            scrollPos1 = GUILayout.BeginScrollView(scrollPos1, GUILayout.Width(position.width - 20), GUILayout.Height(position.height - 90));
        }
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
                    if (GUI.Button(new Rect(x * 100 * imageSize, y * 100 * imageSize, 100 * imageSize, 100 * imageSize), content))
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
            GUILayout.Space(y * 100 * imageSize + 100);
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.EndVertical();
        #endregion

        #region Options
        GUILayout.BeginVertical("Box");
        if (!windowHideOptions)
        {
            GUILayout.BeginVertical("Box");
            placementOption = GUILayout.Toolbar(placementOption, new string[] { "Click", "Paint" });
            if (placementOption == 1)
            {
                paintSpeed = EditorGUILayout.FloatField("Paint Speed: ", paintSpeed);
            }
            parentObject = (GameObject)EditorGUILayout.ObjectField("Parent Object: ", parentObject, typeof(GameObject), true);
            if (parentObject != null)
            {
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
            rotation = EditorGUILayout.Vector3Field("Rotation ", rotation);
            snapRot = EditorGUILayout.FloatField("Snap Rotation: ", snapRot);
            randomRot = EditorGUILayout.Toggle("Random Rotation: ", randomRot);
            GUILayout.EndVertical();
        }
        if (windowHideOptions)
        {
            if (GUILayout.Button("Show Options"))
            {
                windowHideOptions = false;
            }
        }
        else
        {
            if (GUILayout.Button("Hide Options"))
            {
                windowHideOptions = true;
            }
        }
        GUILayout.EndVertical();
    #endregion
    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
        SceneView.onSceneGUIDelegate += this.OnScene;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
        SceneView.onSceneGUIDelegate -= this.OnScene;
        DestroyImmediate(exampleObj);
    }

    void OnSceneGUI(SceneView sceneView)
    {
        Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(worldRay, out hitInfo))
        {
            mousePos = hitInfo.point;

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
                exampleObj.transform.rotation = Quaternion.EulerRotation(rotation.x, rotation.y, rotation.z);
            }

            if (!Event.current.alt)
            {
                if (selectedID != 99999999)
                {
                    if (Event.current.type == EventType.Layout)
                    {
                        HandleUtility.AddDefaultControl(0);
                    }

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

            // Draw grid
            /*
            Handles.color = new Color(0, 1, 0);
            for (int hor = 0; hor < 3; hor++)
            {
                Handles.DrawLine(new Vector3(hitInfo.point.x - 10, hitInfo.point.y, hitInfo.point.z - 1 + 1 * hor), new Vector3(hitInfo.point.x + 10, hitInfo.point.y, hitInfo.point.z - 1 + 1 * hor));
            }
            for (int ver = 0; ver < 3; ver++)
            {
                Handles.DrawLine(new Vector3(hitInfo.point.x - 1 + 1 * ver, hitInfo.point.y, hitInfo.point.z - 10), new Vector3(hitInfo.point.x - 1 + 1 * ver, hitInfo.point.y, hitInfo.point.z + 10));
            }
            */

            // Draw obj location

            if (selectedID != 99999999)
            {
                Handles.color = new Color(1, 0, 0);
                Handles.DrawLine(new Vector3(hitInfo.point.x - 0.1f, hitInfo.point.y, hitInfo.point.z), new Vector3(hitInfo.point.x + 0.1f, hitInfo.point.y, hitInfo.point.z));
                Handles.DrawLine(new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z - 0.1f), new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z + 0.1f));
                Handles.DrawSphere(1,hitInfo.point, Quaternion.identity,0.05f);
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

    void OnScene(SceneView sceneView)
    {
        Handles.BeginGUI();
        if (showOptionsOnScreen)
        {
            GUI.Box(new Rect(0, 0, Screen.width, 22), GUIContent.none);
            OnscreenSelectedID = GUI.Toolbar(new Rect(22, 1, Screen.width / 3 - 30, 20), OnscreenSelectedID, new string[] { "Settings", "Placement", "Transform", "Snap" });
            if(OnscreenSelectedID == 0)
            {
                //parentObject = (GameObject)EditorGUILayout.ObjectField("Parent Object: ", parentObject, typeof(GameObject), true);
                GUI.Label(new Rect(Screen.width / 3, 1, 200, 50), "Test");
            }
        }

        GUI.color = new Color(1f, 1f, 1f, 1f);
        if (!showOptionsOnScreen)
        {
            if (GUI.Button(new Rect(1, 1, 20, 20), "+"))
            {
                showOptionsOnScreen = true;
            }
        }
        else
        {
            if (GUI.Button(new Rect(1, 1, 20, 20), "-"))
            {
                showOptionsOnScreen = false;
            }
        }
        Handles.EndGUI();
    }

    void CreatePrefab(Vector3 createPos)
    {
        GameObject createdObj = Instantiate(prefabs[selectedID], createPos, Quaternion.identity);
        if (parentObject != null)
        {
            createdObj.transform.parent = parentObject.transform;
        }
        if(randomRot)
        {
            createdObj.transform.rotation = Quaternion.EulerAngles(0,Random.Range(0,360),0);
        }
    }
}
 