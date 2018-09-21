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

    private string searchScript = "";
    private string searchCode = "";
    private string searchAlgorithms = "";
    private string searchScriptTag = "";

    private int selectedTab = 3;
    private int selectedTabQuikStart;
    private int selectedTabType3D;
    private int selectedTabType2D;
    private int selectedTabOptions;

    private bool addScene = true, addScripts = true, addObject = true;

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
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class Movement_Fps : MonoBehaviour {\n\n    [Header(\"Player\")]\n    [SerializeField] private float m_NormalSpeed;\n    [SerializeField] private float m_SprintSpeed;\n    [SerializeField] private float m_JumpSpeed;\n    [SerializeField] private float m_Gravity;\n\n    [Header(\"Camera\")]\n    [SerializeField] private Transform head;\n    [SerializeField] private float cameraSensitivity;\n\n    private CharacterController m_CC;\n    private Vector3 m_MoveDirection;\n    private float m_Speed;\n\n    //Rotation\n    private float m_RotationX = 0.0f;\n    private float m_RotationY = 0.0f;\n\n    void Start()\n    {\n        Cursor.lockState = CursorLockMode.Locked;\n        m_CC = GetComponent<CharacterController>();\n    }\n\n    void Update() \n    {\n        //Rotation\n        m_RotationX += Input.GetAxis(\"Mouse X\") * cameraSensitivity * Time.deltaTime;\n        m_RotationY += Input.GetAxis(\"Mouse Y\") * cameraSensitivity * Time.deltaTime;\n        m_RotationY = Mathf.Clamp (m_RotationY, -90, 90);\n\n        transform.localRotation = Quaternion.AngleAxis(m_RotationX, Vector3.up);\n        head.transform.localRotation = Quaternion.AngleAxis(m_RotationY, Vector3.left);\n\n        //Movement\n        if (m_CC.isGrounded)\n        {\n            m_MoveDirection = new Vector3(Input.GetAxis(\"Horizontal\"), 0, Input.GetAxis(\"Vertical\"));\n            m_MoveDirection = transform.TransformDirection(m_MoveDirection);\n            m_MoveDirection *= m_Speed;\n            if (Input.GetButton(\"Jump\"))\n                m_MoveDirection.y = m_JumpSpeed;\n        }\n        m_MoveDirection.y -= m_Gravity * Time.deltaTime;\n        m_CC.Move(m_MoveDirection * Time.deltaTime);\n\n        //Sprint\n        if (Input.GetKey(KeyCode.LeftShift))\n        {\n            m_Speed = m_SprintSpeed;\n        }\n        else\n        {\n            m_Speed = m_NormalSpeed;\n        }\n    }\n}\n",
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class Movement_ThirdPerson : MonoBehaviour\n{\n    [Header(\"Player\")]\n    [SerializeField] private float m_NormalSpeed;\n    [SerializeField] private float m_SprintSpeed;\n    [SerializeField] private float m_MouseSensitivity;\n    [SerializeField] private float m_JumpSpeed;\n    [SerializeField] private float m_Gravity;\n\n    [Header(\"Camera\")]\n    [SerializeField] private GameObject m_RotationPoint;\n\n    private CharacterController m_CC;\n    private GameObject m_Player;\n    private float m_Speed;\n    private Vector3 m_MoveDirection;\n\n    //Rotation\n    private float m_RotationX = 0.0f;\n    private float m_RotationY = 0.0f;\n\n    void Start()\n    {\n        Cursor.lockState = CursorLockMode.Locked;\n        m_CC = GetComponent<CharacterController>();\n        m_Player = this.gameObject;\n    }\n\n    void Update()\n    {\n        //Movement\n        if (m_CC.isGrounded)\n        {\n            m_MoveDirection = new Vector3(Input.GetAxis(\"Horizontal\"), 0, Input.GetAxis(\"Vertical\"));\n            m_MoveDirection = transform.TransformDirection(m_MoveDirection);\n            m_MoveDirection *= m_Speed;\n            if (Input.GetButton(\"Jump\"))\n                m_MoveDirection.y = m_JumpSpeed;\n        }\n        m_MoveDirection.y -= m_Gravity * Time.deltaTime;\n        m_CC.Move(m_MoveDirection * Time.deltaTime);\n\n        //Sprint\n        if (Input.GetKey(KeyCode.LeftShift))\n        {\n            m_Speed = m_SprintSpeed;\n        }\n        else\n        {\n            m_Speed = m_NormalSpeed;\n        }\n\n        //Player Rotation\n        m_RotationX += Input.GetAxis(\"Mouse X\") * m_MouseSensitivity * Time.deltaTime;\n        m_RotationY += Input.GetAxis(\"Mouse Y\") * m_MouseSensitivity * Time.deltaTime;\n        m_RotationY = Mathf.Clamp(m_RotationY, -90, 90);\n\n        transform.localRotation = Quaternion.AngleAxis(m_RotationX, Vector3.up);\n        m_RotationPoint.transform.localRotation = Quaternion.AngleAxis(m_RotationY, Vector3.left);\n    }\n}\n",
        "",
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class Movement_Platformer3D : MonoBehaviour\n{\n    [Header(\"player\")]\n    [SerializeField] private float m_NormalSpeed;\n    [SerializeField] private float m_SprintSpeed;\n    [SerializeField] private float m_JumpSpeed;\n    [SerializeField] private float m_Gravity;\n    private Vector3 m_MoveDirection;\n\n    [Header(\"Camera\")]\n    [SerializeField] private GameObject m_Camera;\n    [SerializeField] private Vector3 m_OffSet;\n    [SerializeField] private bool m_LookTowardsPlayer;\n\n    private float m_Speed;\n    private CharacterController m_CC;\n    private GameObject m_Player;\n\n    void Start ()\n    {\n        Cursor.lockState = CursorLockMode.Locked;\n        m_CC = GetComponent<CharacterController>();\n        m_Player = this.gameObject;\n    }\n\t\n\tvoid Update ()\n    {\n        //Movement\n        if (m_CC.isGrounded)\n        {\n            m_MoveDirection = new Vector3(Input.GetAxis(\"Horizontal\"), 0, 0);\n            m_MoveDirection = transform.TransformDirection(m_MoveDirection);\n            m_MoveDirection *= m_Speed;\n            if (Input.GetButton(\"Jump\"))\n                m_MoveDirection.y = m_JumpSpeed;\n        }\n        m_MoveDirection.y -= m_Gravity * Time.deltaTime;\n        m_CC.Move(m_MoveDirection * Time.deltaTime);\n\n        //Sprint\n        if (Input.GetKey(KeyCode.LeftShift))\n        {\n            m_Speed = m_SprintSpeed;\n        }\n        else\n        {\n            m_Speed = m_NormalSpeed;\n        }\n\n        //Camera\n        m_Camera.transform.position = new Vector3(m_Player.transform.position.x + m_OffSet.x, m_Player.transform.position.y + m_OffSet.y, m_Player.transform.position.z + m_OffSet.z);\n        if (m_LookTowardsPlayer)\n        {\n            m_Camera.transform.LookAt(m_Player.transform);\n        }\n    }\n}\n",
        "",
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class Movement_TopDown3D : MonoBehaviour\n{\n    [Header(\"Camera\")]\n    [SerializeField] private Camera m_Camera;\n    [SerializeField] private Vector3 m_OffSet;\n    [SerializeField] private bool m_LookTowardsPlayer;\n    [Header(\"Player\")]\n    [SerializeField] private float m_NormalSpeed;\n    [SerializeField] private float m_SprintSpeed;\n\n    private Rigidbody m_rb;\n    private GameObject m_Player;\n    private float m_Speed;\n\n    void Start () \n    {\n        m_rb = GetComponent<Rigidbody>();\n        m_Player = this.gameObject;\n    }\n\t\n\tvoid Update ()\n    {\n        //Movement\n        Vector3 moveInput = new Vector3(Input.GetAxisRaw(\"Horizontal\"), 0f, Input.GetAxisRaw(\"Vertical\"));\n        if (Input.GetKey(KeyCode.LeftShift)) { m_Speed = m_SprintSpeed; } else { m_Speed = m_NormalSpeed; }\n        m_rb.velocity = moveInput * m_Speed;\n\n        //Camera\n        m_Camera.transform.position = new Vector3(m_Player.transform.position.x + m_OffSet.x, m_Player.transform.position.y + m_OffSet.y, m_Player.transform.position.z + m_OffSet.z);\n        if(m_LookTowardsPlayer)\n        {\n            m_Camera.transform.LookAt(m_Player.transform);\n        }\n\n        //Player Rotation\n        Ray cameraRay = m_Camera.ScreenPointToRay(Input.mousePosition);\n        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);\n        float rayLength;\n        if (groundPlane.Raycast(cameraRay, out rayLength))\n        {\n            Vector3 pointToLook = cameraRay.GetPoint(rayLength);\n            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));\n        }\n    }\n}\n",
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class FreeCam : MonoBehaviour\n{\n    [Header(\"Settings\")]\n    [SerializeField] private float m_MouseSensitivity;\n    [SerializeField] private float m_CameraSpeed;\n    [SerializeField] private float m_ShiftSpeed;\n\n    private float rotationX, rotationY, m_Speed;\n\t\n\tvoid Update ()\n    {\n        Vector3 moveInput = new Vector3(Input.GetAxisRaw(\"Horizontal\"), 0f, Input.GetAxisRaw(\"Vertical\"));\n        if (Input.GetKey(KeyCode.LeftShift)) { m_Speed = m_ShiftSpeed; } else { m_Speed = m_CameraSpeed; }\n        transform.Translate(moveInput * m_Speed * Time.deltaTime);\n\n        rotationX += Input.GetAxis(\"Mouse X\") * m_MouseSensitivity * Time.deltaTime;\n        rotationY += Input.GetAxis(\"Mouse Y\") * m_MouseSensitivity * Time.deltaTime;\n\n        transform.eulerAngles = new Vector3(-rotationY, rotationX, 0);\n    }\n}\n",
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class ObjectPool : MonoBehaviour\n{\n    [Header(\"Amount\")]\n    public int pooledAmount;\n\n    [Header(\"Object\")]\n    public GameObject prefabObj;\n\n    [HideInInspector]public List<GameObject> objects;\n\n    void Start()\n    {\n        for (int i = 0; i < pooledAmount; i++)\n        {\n            GameObject obj = (GameObject)Instantiate(prefabObj);\n            obj.transform.parent = gameObject.transform;\n            obj.SetActive(false);\n            objects.Add(obj);\n        }\n    }\n}\n",
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine;\n\npublic class Rotation : MonoBehaviour\n{\n    [SerializeField] private Vector3 m_Rotation;\n\n\tvoid Update ()\n    {\n        this.transform.Rotate(m_Rotation.x * Time.deltaTime, m_Rotation.y * Time.deltaTime, m_Rotation.z * Time.deltaTime);\n    }\n}\n",
        "using System.Collections;\nusing System.Collections.Generic;\nusing UnityEngine.SceneManagement;\nusing UnityEngine;\n\npublic class Menu : MonoBehaviour\n{\n    public void LoadScene(int SceneID)\n    {\n        SceneManager.LoadScene(SceneID);\n    }\n\n    public void LoadScene(string SceneName)\n    {\n        SceneManager.LoadScene(SceneManager.GetSceneByName(SceneName).buildIndex);\n    }\n\n    public void Quit()\n    {\n        Application.Quit();\n    }\n}\n"
    };
    public string[] scriptTags = new string[]
    {
        "3D",
        "3D",
        "2D",
        "3D",
        "2D",
        "3D",
        "3D",
        "Other",
        "Other",
        "Other"
    };
    #endregion
    #region Code Examples
    public string[] codeExampleName = new string[]
    {
        "for",
        "switch",
        "ontriggerenter",
        "Instantiate"
    };
    public string[] codeExampleCode = new string[]
    {
        "for (int i = 0; i < length; i++)\n            {\n\n            }",
        "switch (exampleIntValue)\n{\n    case 1:\n        //Do Something\n        break;\n    case 2:\n        //Do Something Else\n        break;\n    default:\n        //If Nothing Do This\n        break;\n}",
        "    void OnTriggerEnter(Collider Other)\n    {\n\n    }",
        "Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);"
    };
    #endregion
    #region Algorithms
    public string[] algorithmsName = new string[]
    {
        "New Algorithm"
    };
    public string[] algorithmsCode = new string[]
    {
        ""
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
        selectedTab = GUILayout.Toolbar(selectedTab, new string[] { "Scripts", "Code", "Algorithms", "QuikStart" });

        if (selectedTab != 3)
        {
            //Search
            GUILayout.BeginVertical("Box");
            if (selectedTab == 0)
            {
                searchScript = EditorGUILayout.TextField("Search: ", searchScript);
                searchScriptTag = EditorGUILayout.TextField("SearchTag: ", searchScriptTag);
            }
            if (selectedTab == 1)
            {
                searchCode = EditorGUILayout.TextField("Search: ", searchCode);
            }
            if (selectedTab == 2)
            {
                searchAlgorithms = EditorGUILayout.TextField("Search: ", searchAlgorithms);
            }
            GUILayout.EndVertical();

            //Results
            GUILayout.BeginVertical("Box");
            if (selectedTab == 0)
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
                            GUILayout.EndHorizontal();
                        }
                    }
                }
            }
            if (selectedTab == 1)
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
                        GUILayout.EndHorizontal();
                    }
                }
            }
            if (selectedTab == 2)
            {
                for (int i = 0; i < algorithmsName.Length; i++)
                {
                    if (searchCode == "" || algorithmsName[i].ToLower().Contains(searchCode.ToLower()))
                    {
                        GUILayout.BeginHorizontal("Box");
                        GUILayout.Label(algorithmsName[i], EditorStyles.boldLabel);
                        if (GUILayout.Button("Add"))
                        {
                            //AddScripts(scriptName[i], scriptCode[i]);
                        }
                        if (GUILayout.Button("Copy"))
                        {
                            EditorGUIUtility.systemCopyBuffer = algorithmsCode[i];
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.EndVertical();
        }

        //Quick Start
        if (selectedTab == 3)
        {
            GUILayout.BeginVertical("Box");
            selectedTabQuikStart = GUILayout.Toolbar(selectedTabQuikStart, new string[] { "2D", "3D" });
            GUILayout.EndVertical();

            GUILayout.BeginVertical("Box");
            if(selectedTabQuikStart == 0)
            {
                selectedTabType2D = GUILayout.Toolbar(selectedTabType2D, new string[] { "TopDown", "Platformer" });
            }
            else
            {
                selectedTabType3D = GUILayout.Toolbar(selectedTabType3D, new string[] { "FPS", "ThirdPerson", "TopDown", "Platformer" });
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            Color defaultColor = GUI.backgroundColor;
            if (addScene) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = new Color(1, 0, 0); }
            addScene = GUILayout.Toggle(addScene, "Scene", EditorStyles.toolbarButton);
            if (addObject) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = new Color(1, 0, 0); }
            addObject = GUILayout.Toggle(addObject, "Objects", EditorStyles.toolbarButton);
            if (addScripts) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = new Color(1, 0, 0); }
            addScripts = GUILayout.Toggle(addScripts, "Scripts", EditorStyles.toolbarButton);
            GUI.backgroundColor = defaultColor;
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            if (GUILayout.Button("Create"))
            {
                if (addScene)
                {
                    Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                }
                if (addObject)
                {
                    CreateObjects();
                }
                if (addScripts)
                {
                    CreateScripts();
                }
            }
        }
    }

    void AddScripts(string newScriptName, string contents)
    {
        using (StreamWriter sw = new StreamWriter(string.Format(Application.dataPath + "/" + newScriptName + ".cs",
                                                   new object[] { newScriptName.Replace(" ", "") })))
        {
            sw.Write(contents);
        }
    }

    public void CreateObjects()
    {
        //3D
        if (selectedTabQuikStart == 1)
        {
            GameObject groundCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            groundCube.name = "Ground";
            groundCube.transform.position = new Vector3(0, 0, 0);

            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player";
            player.transform.position = new Vector3(0, 2, 0);

            GameObject cameraObj = GameObject.Find("Main Camera");
            //FPS
            if (selectedTabType3D == 0)
            {
                groundCube.transform.localScale = new Vector3(25, 1, 25);
                cameraObj.transform.parent = player.transform;
                cameraObj.transform.localPosition = new Vector3(0, 0.65f, 0);
            }
            //ThirdPerson
            if (selectedTabType3D == 1)
            {
                groundCube.transform.localScale = new Vector3(25, 1, 25);
                GameObject rotationPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rotationPoint.name = "rotationPoint";
                rotationPoint.transform.position = new Vector3(0, 2, 0);
                cameraObj.transform.parent = rotationPoint.transform;
                cameraObj.transform.localPosition = new Vector3(1, 0.65f, -1.5f);
                rotationPoint.transform.parent = player.transform;
            }
            //TopDown
            if (selectedTabType3D == 2)
            {
                groundCube.transform.localScale = new Vector3(25, 1, 25);
                cameraObj.transform.position = new Vector3(0,10,-1.5f);
                cameraObj.transform.eulerAngles = new Vector3(80, 0, 0);
            }
            //Platformer
            if (selectedTabType3D == 3)
            {
                groundCube.transform.localScale = new Vector3(25, 1, 1);
            }
        }

        //2D
        if (selectedTabQuikStart == 0)
        {
            GameObject groundCube = GameObject.CreatePrimitive(PrimitiveType.Quad);
            groundCube.name = "Ground";
            groundCube.transform.position = new Vector3(0, 0, 0);

            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Quad);
            player.name = "Player";
            player.transform.position = new Vector3(0, 2, 0);

            GameObject cameraObj = GameObject.Find("Main Camera");
            Camera cam = cameraObj.GetComponent<Camera>();
            cam.orthographic = true;

            //TopDown
            if (selectedTabType2D == 0)
            {
                groundCube.transform.localScale = new Vector3(100,100,1);
                groundCube.transform.position = new Vector3(0, 0, 1);
            }
            //Platformer
            if(selectedTabType2D == 1)
            {
                groundCube.transform.localScale = new Vector3(25,1,1);
            }
        }
    }
    void CreateScripts()
    {
        //3D
        if (selectedTabType3D == 0)
        {
            AddScripts(scriptName[0], scriptCode[0]);
        }
        if (selectedTabType3D == 1)
        {
            AddScripts(scriptName[1], scriptCode[1]);
        }
        if (selectedTabType3D == 2)
        {
            AddScripts(scriptName[4], scriptCode[4]);
        }
        if (selectedTabType3D == 3)
        {
            AddScripts(scriptName[3], scriptCode[3]);
        }
        if (selectedTabType2D == 0)
        {

        }
        if (selectedTabType2D == 1)
        {

        }
    }
}


