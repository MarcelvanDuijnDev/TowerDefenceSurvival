using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class Tool_Scripts : EditorWindow
{
    private string filePath = "Assets";
    private bool codeExamples = false;
    private bool quickStart = true;
    private string searchScript = "";
    private string searchCode = "";
    private string searchScriptTag = "";
    private string textField = "";

    //3D Options
    private int options3DStep;

    //2D Options
    private int options2DStep;

    //Both
    private string gameType = "";
    private string settings = "";
    private List<int> scriptsNeeded = new List<int>();
    private bool[] scriptArray;

    private string sceneOptions = "";

    #region Scripts
    public string[] scriptName = new string[]
    {
        "Movement_Fps",
        "Movement_ThirdPerson",
        "Movement_Platformer2D",
        "Movement_Platformer3D",
        "Movement_TopDown2D",
        "Movement_TopDown3D",
        "Free_Cam",
        "ObjectPool",
        "Rotation",
        "Menu"
    };
    public string[] scriptCode = new string[]
    {
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        ""
    };
    public string[] scriptTags = new string[]
    {
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        ""
    };
    #endregion
    #region Code Examples
    public string[] codeExampleName = new string[]
    {
        "for",
        "switch",
        "ontriggerenter",
        "",
        "",
    };
    public string[] codeExampleCode = new string[]
    {
        "for (int i = 0; i < length; i++)\n            {\n\n            }",
        "",
        "",
        "",
        "",
    };
    #endregion

    [MenuItem("Tools/Scripts")]
    static void Init()
    {
        Tool_Scripts window = (Tool_Scripts)EditorWindow.GetWindow(typeof(Tool_Scripts));
        window.Show();
    }

    void OnGUI()
    {
        if (!quickStart && options3DStep == 0 && options2DStep == 0)
        {
            GUILayout.BeginHorizontal("Box");
            if (GUILayout.Button("Scripts"))
            {
                codeExamples = false;
            }
            if (GUILayout.Button("Code Examples"))
            {
                codeExamples = true;
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("Scripts", EditorStyles.boldLabel);
            GUILayout.BeginVertical("Box");
            if (!codeExamples)
            {
                searchScript = EditorGUILayout.TextField("Search: ", searchScript);
                searchScriptTag = EditorGUILayout.TextField("SearchTag: ", searchScriptTag);
            }
            else
            {
                searchCode = EditorGUILayout.TextField("Search: ", searchCode);
            }
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box");

            if (!codeExamples)
            {
                for (int i = 0; i < scriptName.Length; i++)
                {
                    if (searchScript == "" || scriptName[i].ToLower().Contains(searchScript.ToLower()))
                    {
                        if (scriptTags[i].ToLower().Contains(searchScriptTag.ToLower()) || scriptTags[i] == "" || scriptTags[i] == null)
                        {
                            GUILayout.BeginHorizontal("Box");
                            GUILayout.Label(scriptName[i], EditorStyles.boldLabel);
                            if (GUILayout.Button("Add"))
                            {
                                AddScripts(scriptName[i], scriptCode[i]);
                                if (i == 7)
                                {
                                    AddScripts("UseObjectPool", "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class UseObjectPool : MonoBehaviour\n{\n    [SerializeField]private ObjectPool1 objectPoolScript;\n\n    void Use()\n    {\n        for (int i = 0; i < objectPoolScript.objects.Count; i++)\n        {\n            if (!objectPoolScript.objects[i].activeInHierarchy)\n            {\n                objectPoolScript.objects[i].transform.position = transform.position;\n                objectPoolScript.objects[i].transform.rotation = transform.rotation;\n                objectPoolScript.objects[i].SetActive(true);\n                break;\n            }\n        }\n    }\n}\n");
                                }
                            }
                            if (GUILayout.Button("Copy"))
                            {
                                EditorGUIUtility.systemCopyBuffer = scriptCode[i];
                            }
                            if (GUILayout.Button("Show"))
                            {
                                textField = scriptCode[i];
                            }
                            GUILayout.EndHorizontal();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < codeExampleName.Length; i++)
                {
                    if (searchCode == "" || codeExampleName[i].ToLower().Contains(searchCode.ToLower()))
                    {
                        GUILayout.BeginHorizontal("Box");
                        GUILayout.Label(codeExampleName[i], EditorStyles.boldLabel);
                        if (GUILayout.Button("Copy"))
                        {
                            EditorGUIUtility.systemCopyBuffer = codeExampleCode[i];
                        }
                        if (GUILayout.Button("Show"))
                        {
                            textField = codeExampleCode[i];
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndVertical();
        }
        else
        {
            GUILayout.Label("Quick Start", EditorStyles.boldLabel);
            GUILayout.BeginVertical("Box");
            if (options2DStep == 0 && options3DStep == 0)
            {
                if (GUILayout.Button("Scripts", GUILayout.Height(position.height * 0.29f)))
                {
                    quickStart = false;
                }
                if (GUILayout.Button("3D", GUILayout.Height(position.height * 0.29f)))
                {
                    options3DStep = 1;
                }
                if (GUILayout.Button("2D", GUILayout.Height(position.height * 0.29f)))
                {
                    options2DStep = 1;
                }
                settings = "";
            }

            //3D
            if (options3DStep == 1)
            {
                if (GUILayout.Button("First Person Shooter", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType = "FPS";
                    settings += "Type: First Person Shooter \n";
                    options3DStep = 2;
                    scriptArray = new bool[1];
                }
                if (GUILayout.Button("Third Person", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType = "ThirdPerson";
                    settings += "Type: ThirdPerson \n";
                    options3DStep = 2;
                    scriptArray = new bool[1];
                }
                if (GUILayout.Button("Top Down", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType = "TopDown";
                    settings += "Type: Top Down \n";
                    options3DStep = 2;
                    scriptArray = new bool[1];
                }
                for (int i = 0; i < scriptArray.Length; i++)
                {
                    scriptArray[i] = true;
                }
            }
            if (options3DStep == 2)
            {
                if (GUILayout.Button("Create: scene, objects and scripts", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType += " NewScene + Scripts";
                    settings += "Options: Create: scene, objects and scripts \n";
                    options3DStep = 3;
                }
                if (GUILayout.Button("Create: objects with scripts", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType += " AddObjWithScriptS";
                    settings += "Type: Create: objects with scripts \n";
                    options3DStep = 3;
                }
                if (GUILayout.Button("Create: scripts", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType += " AddScriptS";
                    settings += "Type: Create: scripts \n";
                    options3DStep = 3;
                }
            }
            if (options3DStep == 3)
            {
                GUILayout.Label(settings, EditorStyles.boldLabel);
                //Select Scripts
                if (GUILayout.Button("Confirm", GUILayout.Height(position.height * 0.3f)))
                {
                    Set3DSettings1();
                }
                if (GUILayout.Button("Back to Quik Start", GUILayout.Height(position.height * 0.3f)))
                {
                    gameType = "";
                    settings = "";
                    options3DStep = 0;
                }
            }

            //2D
            if (options2DStep == 1)
            {

            }

            GUILayout.EndVertical();
        }
    }

    void Set3DSettings1()
    {
        if (gameType.Contains("NewScene"))
        {
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            //AddScripts();
            CreateBasic3DScene(gameType);
            CreateScripts(gameType);
            //add objects

        }
        if (gameType.Contains("AddObjWithScript"))
        {
            //Create new scene
        }
        if (gameType.Contains("AddScript"))
        {
            //Create new scene
        }
        Set3DSettings2();
    }
    void Set3DSettings2()
    {
        if (gameType.Contains("FPS"))
        {
            //Create new scene
        }
        if (gameType.Contains("ThirdPerson"))
        {
            //Create new scene
        }
        if (gameType.Contains("TopDown"))
        {
            //Create new scene
        }
        quickStart = false;
    }

    void AddScripts(string newScriptName, string contents)
    {
        using (StreamWriter sw = new StreamWriter(string.Format(Application.dataPath + "/" + newScriptName + ".cs",
                                                   new object[] { newScriptName.Replace(" ", "") })))
        {
            sw.Write(contents);
        }
    }

    void CreateBasic3DScene(string type)
    {
        GameObject groundCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        groundCube.name = "Ground";
        groundCube.transform.position = new Vector3(0, 0, 0);

        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.transform.position = new Vector3(0, 2, 0);
        player.AddComponent<CharacterController>();

        GameObject cameraObj = GameObject.Find("Main Camera");

        if (type.Contains("TopDown") || type.Contains("FPS") || type.Contains("ThirdPerson"))
        {
            groundCube.transform.localScale = new Vector3(25, 1, 25);
            if (type.Contains("FPS"))
            {
                cameraObj.transform.parent = player.transform;
                cameraObj.transform.localPosition = new Vector3(0, 0.65f, 0);
            }
            if (type.Contains("ThirdPerson"))
            {
                GameObject rotationPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rotationPoint.name = "rotationPoint";
                rotationPoint.transform.position = new Vector3(0, 2, 0);
                cameraObj.transform.parent = rotationPoint.transform;
                cameraObj.transform.localPosition = new Vector3(1, 0.65f, -1.5f);
                rotationPoint.transform.parent = player.transform;
            }
        }
        else
        {
            groundCube.transform.localScale = new Vector3(25, 1, 1);
        }
    }
    void CreateScripts(string type)
    {
        if (type.Contains("FPS"))
        {
            AddScripts(scriptName[0], scriptCode[0]);
        }
        if (type.Contains("TopDown"))
        {
            AddScripts(scriptName[4], scriptCode[4]);
        }
        if (type.Contains("ThirdPerson"))
        {
            AddScripts(scriptName[1], scriptCode[1]);
        }
        if (type.Contains("PlatFormer3D"))
        {
            AddScripts(scriptName[3], scriptCode[3]);
        }
    }
}


