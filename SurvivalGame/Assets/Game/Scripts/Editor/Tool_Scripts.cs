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

    private string sceneOptions = "";

    #region Scripts
    public string[] scriptName = new string[]
    {
        "Movement_Fps",
        "Movement_ThirdPerson",
        "Movement_Platformer",
        "Movement_TopDown3D",
        "Movement_TopDown2D",
        "Free_Cam",
        "ObjectPool"
    };
    public string[] scriptCode = new string[]
    {
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class Movement_Fps : MonoBehaviour {\n\n    //Movement\n    [SerializeField]private float normalSpeed, sprintSpeed;\n    [SerializeField]private float jumpSpeed;\n    [SerializeField]private float gravity;\n    private Vector3 moveDirection = Vector3.zero;\n    //Look around\n    public float cameraSensitivity;\n    [SerializeField]private Transform head;\n    private float rotationX = 0.0f;\n    private float rotationY = 0.0f;\n    private float speed;\n    private float dpadHorizontal, dpadVertical;\n\n    void Start()\n    {\n        Cursor.lockState = CursorLockMode.Locked;\n    }\n\n    void Update() \n    {\n\n        //Look around\n        rotationX += Input.GetAxis(\"Mouse X\") * cameraSensitivity * Time.deltaTime;\n        rotationY += Input.GetAxis(\"Mouse Y\") * cameraSensitivity * Time.deltaTime;\n        rotationY = Mathf.Clamp (rotationY, -90, 90);\n\n        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);\n        head.transform.localRotation = Quaternion.AngleAxis(rotationY, Vector3.left);\n\n        //Movement\n        CharacterController controller = GetComponent<CharacterController>();\n        if (controller.isGrounded) {\n            moveDirection = new Vector3(Input.GetAxis(\"Horizontal\"), 0, Input.GetAxis(\"Vertical\"));\n            moveDirection = transform.TransformDirection(moveDirection);\n            moveDirection *= speed;\n            if (Input.GetButton(\"Jump\"))\n                moveDirection.y = jumpSpeed;\n        }\n        moveDirection.y -= gravity * Time.deltaTime;\n        controller.Move(moveDirection * Time.deltaTime);\n\n\n        //Sprint\n        if(Input.GetKey(KeyCode.LeftShift))\n        {\n            speed = sprintSpeed;\n        }\n        else\n        {\n            speed = normalSpeed;\n        }\n    }\n}\n",
		"",
        "",
        "",
        "",
        "",
        ""
    };
    public string[] scriptTags = new string[]
    {
        "3D",
        "3D",
        "3D",
        "3D",
        "2D",
        "Other",
        "3D"
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
            //AddScripts();
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

    void AddScripts(string newScriptName, string contents)
    {
		using (StreamWriter sw = new StreamWriter(string.Format(Application.dataPath + "/" + newScriptName + ".cs",
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

        GameObject cameraObj = GameObject.Find("Main Camera");
        cameraObj.transform.parent = player.transform;
        cameraObj.transform.localPosition = new Vector3(0, 0.65f, 0);

        //player.AddComponent<TestScript>().cameraObject = cameraObj;
    }
}


