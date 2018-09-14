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
    private bool quickStart = true;
    private string searchScript = "";
    private string searchScriptTag = "";
    private string textField = "";

    //3D Options
    private int options3DStep;

    //2D Options
    private int options2DStep;

    //Both
    private string gameType = "";
    private string settings = "";

    private string sceneOptions = "";

    #region Scripts
    public string[] scriptName = new string[]
    {
        "Movement_Fps",
        "Movement_ThirdPerson",
        "Movement_Platformer",
        "Movement_TopDown3D",
        "Movement_TopDown2D",
        "OtherScript"
    };
    public string[] scriptCode = new string[]
    {
        "",
        "",
        "",
        "",
        "",
        "",

    };
    public string[] scriptTags = new string[]
    {
        "3D",
        "3D",
        "3D",
        "3D",
        "2D",
        "Other"
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
            GUILayout.Label("Scripts", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal("Box");
            searchScript = EditorGUILayout.TextField("Search: ", searchScript);
            searchScriptTag = EditorGUILayout.TextField("SearchTag: ", searchScriptTag);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("Box");

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
                            string sn = scriptName[i];
                            /*
                            AssetDatabase.CreateAsset(sn, "Assets/test.cs");
                            Add script
                            */
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

                        /*
                        GUILayout.BeginScrollView(new Vector2(3,150), GUILayout.Width(position.width), GUILayout.Height(position.height - 100));
                        GUILayout.BeginHorizontal("Box");
                        //textField = EditorGUI.TextArea(new Vector2(0,0), textField);
                        GUILayout.EndHorizontal();
                        GUILayout.EndScrollView();
                        */
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
                if (GUILayout.Button("Advanced", GUILayout.Height(position.height * 0.29f)))
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
            if(options3DStep == 1)
            {
                if (GUILayout.Button("First Person Shooter", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType = "FPS";
                    settings += "Type: First Person Shooter \n";
                    options3DStep = 2;
                }
                if (GUILayout.Button("Third Person", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType = "ThirdPerson";
                    settings += "Type: ThirdPerson \n";
                    options3DStep = 2;
                }
                if (GUILayout.Button("Top Down", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType = "TopDown";
                    settings += "Type: Top Down \n";
                    options3DStep = 2;
                }
            }
            if (options3DStep == 2)
            {
                if (GUILayout.Button("Create: scene, objects and scripts", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType += " NewScene";
                    settings += "Options: Create: scene, objects and scripts \n";
                    options3DStep = 3;
                }
                if (GUILayout.Button("Create: objects with scripts", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType += " AddObjWithScript";
                    settings += "Type: Create: objects with scripts \n";
                    options3DStep = 3;
                }
                if (GUILayout.Button("Create: scripts", GUILayout.Height(position.height * 0.29f)))
                {
                    gameType += " AddScript";
                    settings += "Type: Create: scripts \n";
                    options3DStep = 3;
                }
            }
            if (options3DStep == 3)
            {
                GUILayout.Label(settings, EditorStyles.boldLabel);
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
        if(gameType.Contains("NewScene"))
        {
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            AddScripts();
            CreateBasic3DScene();
            //add script
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

    void AddScripts()
    {
        string newScriptName = "TestScript";
        string contents = "GameObject cameraObject";
        using (StreamWriter sw = new StreamWriter(string.Format(Application.dataPath + "/testScript.cs",
                                                   new object[] { newScriptName.Replace(" ", "") })))
        {
            sw.Write(contents);
        }
    }
    void CreateBasic3DScene()
    {
        GameObject groundCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        groundCube.name = "Ground";
        groundCube.transform.position = new Vector3(0,0,0);
        groundCube.transform.localScale = new Vector3(25,1,25);

        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Player";
        player.transform.position = new Vector3(0,2,0);
        player.AddComponent<CharacterController>();
        player.AddComponent<Rigidbody>();

        GameObject cameraObj = GameObject.Find("Main Camera");
        cameraObj.transform.parent = player.transform;
        cameraObj.transform.localPosition = new Vector3(0, 0.65f, 0);

        player.AddComponent<TestScript>().cameraObject = cameraObj;
    }
}


