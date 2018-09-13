using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;

public class Unity_Assistent : EditorWindow
{
    Unity_Assistent_JsonSave JsonFile = new Unity_Assistent_JsonSave();
    Unity_SaveTerrain JsonFileTerrain = new Unity_SaveTerrain();

    GameObject selectedObject;
    GameObject checkSelectedGameObject;
    bool settings, objectSettings, addScripts, terrainEditor; //Top Horizontal Buttons
    bool change_Color; //Button Options
    bool objectInfo, objectComponents, objectQuikChanges; //ObjectSettings Options

    bool loadFilter = false;

    string searchComponent = "", objectName;
    string errorMessage = "Not Working";
    string[] searchComponents = new string[91];
    string[] searchComponentsTag = new string[91];

    Vector3 transformPosition, transformRotation, transformSize;
    Vector2 scrollPos;

    Color matColor = Color.white;

    //Terrain Editor
    bool terrainSettings,terrainFlora;
    bool terrainActive;
    Terrain terrainObject;
    GameObject[] terrainObjects;
    Vector3 vector3Variable;
    float m_TreeDistance;

    [MenuItem("Tools/Unity_Assistent")]
    static void Init()
    {
        Unity_Assistent assist = (Unity_Assistent)EditorWindow.GetWindow(typeof(Unity_Assistent));
        assist.autoRepaintOnSceneChange = true;
        assist.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Obj Settings"))
        {
            objectSettings = !objectSettings;
            settings = false;
            terrainEditor = false;
        }
        if (GUILayout.Button("Settings"))
        {
            settings = !settings;
            objectSettings = false;
            terrainEditor = false;
        }
        if (GUILayout.Button("Terrain Editor"))
        {
            terrainEditor = !terrainEditor;
            objectSettings = false;
            settings = false;
        }
        GUILayout.EndHorizontal();

        //Object Settings
        if (objectSettings)
        {
            //Object Settings
            GUILayout.BeginHorizontal("box");
            GUILayout.BeginVertical("box");
            GUILayout.Label("Object Settings", EditorStyles.boldLabel);
            //enableBool = EditorGUILayout.Toggle("Test Bool", enableBool);
            objectName = EditorGUILayout.TextField("Name", objectName);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height - 100));
            //Object Info
            GUILayout.BeginVertical("Box");
            if (GUILayout.Button("Object Info"))
            {
                objectInfo = !objectInfo;
            }
            if (objectInfo)
            {
                GUILayout.Label("Object Transform", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                transformPosition = EditorGUILayout.Vector3Field("Position", transformPosition);
                GUILayout.EndHorizontal();
                transformRotation = EditorGUILayout.Vector3Field("Rotation", transformRotation);
                transformSize = EditorGUILayout.Vector3Field("Scale", transformSize);
            }
            GUILayout.EndVertical();
            //Object Quik Change
            GUILayout.BeginVertical("Box");
            if (GUILayout.Button("Object Change"))
            {
                objectQuikChanges = !objectQuikChanges;
            }
            if (objectQuikChanges)
            {
                GUILayout.Label("Object Changes", EditorStyles.boldLabel);
                matColor = EditorGUILayout.ColorField("New Color", matColor);
            }
            GUILayout.EndVertical();

            //Object Components
            GUILayout.BeginVertical("Box");
            if (GUILayout.Button("Object Components"))
            {
                objectComponents = !objectComponents;
            }
            if (objectComponents)
            {

                    //Search Component
                    searchComponent = EditorGUILayout.TextField("Search: ", searchComponent);
                    LayoutButton("Mesh");
                    LayoutButton("Effects");
                    LayoutButton("Physics");
                    LayoutButton("Physics2D");
                    LayoutButton("Navigation");
                    //LayoutButton("Audio");
                    LayoutButton("Video");
                    LayoutButton("Rendering");
                    LayoutButton("Tilemap");
                    LayoutButton("Layout");

            }
            GUILayout.EndVertical();

            GUILayout.EndScrollView();
        }

        //Settings
        if (settings)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.BeginVertical("box");
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box");
            if (GUILayout.Button("Save Settings"))
            {
                Save();
                Debug.Log("Saved");
            }
            if (GUILayout.Button("Load Settings"))
            {
                Load();
            }
            GUILayout.EndVertical();
        }

        //Terrain Editor
        if (terrainEditor)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Terrain Editor", EditorStyles.boldLabel);
            objectName = EditorGUILayout.TextField("Name", objectName);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            if (terrainActive)
            {
                GUILayout.BeginVertical("Box");
                if (GUILayout.Button("Terrain Settings"))
                {
                    terrainSettings = !terrainSettings;
                }
                if (terrainSettings)
                {
                    GUILayout.BeginVertical("Box");
                    GUILayout.Label("Settings", EditorStyles.boldLabel);
                    vector3Variable = terrainObject.terrainData.size;
                    vector3Variable = EditorGUILayout.Vector3Field("TerrainSize", vector3Variable);
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical("Box");
                    GUILayout.Label("Graphics", EditorStyles.boldLabel);
                    m_TreeDistance = EditorGUILayout.Slider("Tree Distance",m_TreeDistance, 0,2000);
                    GUILayout.EndVertical();
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical("Box");
                if (GUILayout.Button("Terrain Flora"))
                {
                    terrainFlora = !terrainFlora;
                }
                if (terrainFlora)
                {
                    if (GUILayout.Button("Generate Trees"))
                    {
                        
                    }
                }
                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.Label("Select Gameobject with a Terrain Component", EditorStyles.boldLabel);
            }
        }
    }

    void LayoutButton(string tagName)
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label(tagName, EditorStyles.boldLabel);
        for (int i = 0; i < searchComponents.Length; i++)
        {
            if (string.IsNullOrEmpty(searchComponent) || searchComponents[i].ToLower().Contains(searchComponent.ToLower()))
            {
                if (searchComponentsTag[i] == tagName && selectedObject != null)
                {
                    GetComponents(searchComponents[i]);
                }
            }
        }
        GUILayout.EndVertical();
    }

    void Update()
    {
        //Set Filter
        if (!loadFilter)
        {
            SetFilter();
        }
        selectedObject = Selection.activeGameObject;
        Debug.Log(selectedObject.GetInstanceID());

        if (selectedObject != checkSelectedGameObject && selectedObject != null)
        {
            objectName = selectedObject.transform.name;
            transformPosition = selectedObject.transform.position;
            transformRotation = selectedObject.transform.eulerAngles;
            transformSize = selectedObject.transform.localScale;
            checkSelectedGameObject = selectedObject;
        }

        if (selectedObject != null)
        {
            transformPosition = selectedObject.transform.position;
            transformRotation = selectedObject.transform.eulerAngles;
            transformSize = selectedObject.transform.localScale;
            selectedObject.transform.name = objectName;
            selectedObject.transform.position = new Vector3(transformPosition.x, transformPosition.y, transformPosition.z);
            selectedObject.transform.eulerAngles = new Vector3(transformRotation.x, transformRotation.y, transformRotation.z);
            selectedObject.transform.localScale = new Vector3(transformSize.x, transformSize.y, transformSize.z);
        }

        //Terrain Editor
        if (selectedObject != null && selectedObject.GetComponent<Terrain>() != null)
        {
            if(!terrainActive)
            {
                GetTerrainInfo();
            }
            terrainActive = true;
        }
        else
        {
            terrainActive = false;
        }
        //
        if (terrainActive)
        {
            terrainObject = selectedObject.GetComponent<Terrain>();
            terrainObject.terrainData.size = vector3Variable;

            //Graphics
            terrainObject.treeDistance = m_TreeDistance;


        }

        object getObjTest = EditorUtility.InstanceIDToObject(-1238);
        Debug.Log(getObjTest);
    }

    void GetTerrainInfo()
    {
        terrainObject = selectedObject.GetComponent<Terrain>();
        vector3Variable = terrainObject.terrainData.size;
        m_TreeDistance = terrainObject.treeDistance;
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(JsonFile);
        File.WriteAllText(Application.persistentDataPath + "/Unity_AssistentSave.json", json.ToString());
    }
    private void Load()
    {
        string dataPath = Application.persistentDataPath + "/Unity_AssistentSave.json";
        string dataAsJson = File.ReadAllText(dataPath);
        JsonFile = JsonUtility.FromJson<Unity_Assistent_JsonSave>(dataAsJson);
    }

    void GetComponents(string name)
    {
        if (selectedObject.GetComponent(name) != null)
        { GUI.backgroundColor = new Color(0, 1, 0, 0.85f); }
        else
        { GUI.backgroundColor = new Color(1f, 0, 0, 0.5f); }
        if (GUILayout.Button(name.ToString()))
        {
            if (selectedObject.GetComponent(name) != null)
            { DestroyImmediate(selectedObject.GetComponent(name)); }
            else
            {
                getComponent(name);
            }
        }
        GUI.backgroundColor = new Color(1, 1, 1, 1);
    }

    void CreateScriptebleObject()
    {
    }
        

    void getComponent(string componentName)
    {
        //Mesh
        if (componentName == "MeshFilter") { selectedObject.AddComponent<MeshFilter>();}
        if (componentName == "TextMesh") { selectedObject.AddComponent<TextMesh>();}
        if (componentName == "MeshRenderer") { selectedObject.AddComponent<MeshRenderer>();}
        if (componentName == "SkinnedMeshRenderer") { selectedObject.AddComponent<SkinnedMeshRenderer>();}
        //Effects
        if (componentName == "ParticleSystem") { selectedObject.AddComponent<ParticleSystem>();}
        if (componentName == "TrailRenderer") { selectedObject.AddComponent<TrailRenderer>();}
        if (componentName == "LineRenderer") { selectedObject.AddComponent<LineRenderer>();}
        if (componentName == "LensFlare") { selectedObject.AddComponent<LensFlare>();}
        //if (componentName == "Halo"){Debug.Log(errorMessage);}
        if (componentName == "Projector") { selectedObject.AddComponent<Projector>();}
        //if (componentName == "LegacyParticles"){Debug.Log(errorMessage);}
        //Physics
        if (componentName == "Rigidbody") { selectedObject.AddComponent<Rigidbody>();}
        if (componentName == "CharacterController") { selectedObject.AddComponent<CharacterController>();}
        if (componentName == "BoxCollider") { selectedObject.AddComponent<BoxCollider>();}
        if (componentName == "SphereCollider") { selectedObject.AddComponent<SphereCollider>();}
        if (componentName == "CapsuleCollider") { selectedObject.AddComponent<CapsuleCollider>();}
        if (componentName == "MeshCollider") { selectedObject.AddComponent<MeshCollider>();}
        if (componentName == "WheelCollider") { selectedObject.AddComponent<WheelCollider>();}
        if (componentName == "TerrainCollider") { selectedObject.AddComponent<TerrainCollider>();}
        if (componentName == "Cloth") { selectedObject.AddComponent<Cloth>();}
        if (componentName == "HingeJoint") { selectedObject.AddComponent<HingeJoint>();}
        if (componentName == "FixedJoint") { selectedObject.AddComponent<FixedJoint>();}
        if (componentName == "SpringJoint") { selectedObject.AddComponent<SpringJoint>();}
        if (componentName == "CharacterJoint") { selectedObject.AddComponent<CharacterJoint>();}
        if (componentName == "ConfigurableJoint") { selectedObject.AddComponent<ConfigurableJoint>();}
        if (componentName == "ConstantForce") { selectedObject.AddComponent<ConstantForce>();}
        //Physics2D
        if (componentName == "Rigidbody2D") { selectedObject.AddComponent<Rigidbody2D>();}
        if (componentName == "BoxCollider2D") { selectedObject.AddComponent<BoxCollider2D>();}
        if (componentName == "CircleCollider2D") { selectedObject.AddComponent<CircleCollider2D>();}
        if (componentName == "EdgeCollider2D") { selectedObject.AddComponent<EdgeCollider2D>();}
        if (componentName == "PolygonCollider2D") { selectedObject.AddComponent<PolygonCollider2D>();}
        if (componentName == "CapsuleCollider2D") { selectedObject.AddComponent<CapsuleCollider2D>();}
        if (componentName == "CompositeCollider2D") { selectedObject.AddComponent<CompositeCollider2D>();}
        if (componentName == "DistanceJoint2D") { selectedObject.AddComponent<DistanceJoint2D>();}
        if (componentName == "FixedJoint2D") { selectedObject.AddComponent<FixedJoint2D>();}
        if (componentName == "FrictionJoint2D") { selectedObject.AddComponent<FrictionJoint2D>();}
        if (componentName == "HingeJoint2D") { selectedObject.AddComponent<HingeJoint2D>();}
        if (componentName == "RelativeJoint2D") { selectedObject.AddComponent<RelativeJoint2D>();}
        if (componentName == "SliderJoint2D") { selectedObject.AddComponent<SliderJoint2D>();}
        if (componentName == "SpringJoint2D") { selectedObject.AddComponent<SpringJoint2D>();}
        if (componentName == "TargetJoint2D") { selectedObject.AddComponent<TargetJoint2D>();}
        if (componentName == "WheelJoint2D") { selectedObject.AddComponent<WheelJoint2D>();}
        if (componentName == "AreaEffector2D") { selectedObject.AddComponent<AreaEffector2D>();}
        if (componentName == "BuoyancyEffector2D") { selectedObject.AddComponent<BuoyancyEffector2D>();}
        if (componentName == "PointEffector2D") { selectedObject.AddComponent<PointEffector2D>();}
        if (componentName == "PlatformEffector2D") { selectedObject.AddComponent<PlatformEffector2D>();}
        if (componentName == "SurfaceEffector2D") { selectedObject.AddComponent<SurfaceEffector2D>();}
        if (componentName == "ConstantForce2D") { selectedObject.AddComponent<ConstantForce2D>();}
        //Navigation
        //if (componentName == "NavMeshAgent") {Debug.Log(errorMessage); }
        //if (componentName == "OffMeshLink") {Debug.Log(errorMessage); }
        //if (componentName == "NavMeshObstacle") {Debug.Log(errorMessage); }
        //Audio
        if (componentName == "AudioListener") { selectedObject.AddComponent<AudioListener>();}
        if (componentName == "AudioSource") { selectedObject.AddComponent<AudioSource>();}
        if (componentName == "AudioReverbZone") { selectedObject.AddComponent<AudioReverbZone>();}
        if (componentName == "AudioLowPassFilter") { selectedObject.AddComponent<AudioLowPassFilter>();}
        if (componentName == "AudioLowPassFilter") { selectedObject.AddComponent<AudioLowPassFilter>();}
        if (componentName == "AudioHighPassFilter") { selectedObject.AddComponent<AudioHighPassFilter>();}
        if (componentName == "AudioDistortionFilter") { selectedObject.AddComponent<AudioDistortionFilter>();}
        if (componentName == "AudioReverbFilter") { selectedObject.AddComponent<AudioReverbFilter>();}
        if (componentName == "AudioChorusFilter") { selectedObject.AddComponent<AudioChorusFilter>();}
        //if (componentName == "AudioSpatializer") {Debug.Log(errorMessage); }
        //Video
        if (componentName == "ConstantForce2D") { selectedObject.AddComponent<ConstantForce2D>();}
        //Rendering
        if (componentName == "Camera") { selectedObject.AddComponent<Camera>();}
        if (componentName == "Skybox") { selectedObject.AddComponent<Skybox>();}
        if (componentName == "FlareLayer") { selectedObject.AddComponent<FlareLayer>();}
        if (componentName == "GUILayer") { selectedObject.AddComponent<GUILayer>();}
        if (componentName == "Light") { selectedObject.AddComponent<Light>();}
        if (componentName == "LightProbeGroup") { selectedObject.AddComponent<LightProbeGroup>();}
        if (componentName == "LightProbeProxyVolume") { selectedObject.AddComponent<LightProbeProxyVolume>();}
        if (componentName == "ReflectionProbe") { selectedObject.AddComponent<ReflectionProbe>();}
        if (componentName == "OcclusionArea") { selectedObject.AddComponent<OcclusionArea>();}
        if (componentName == "OcclusionPortal") { selectedObject.AddComponent<OcclusionPortal>();}
        if (componentName == "LODGroup") { selectedObject.AddComponent<LODGroup>();}
        if (componentName == "SpriteRenderer") { selectedObject.AddComponent<SpriteRenderer>();}
        //if (componentName == "SortingGroup") { selectedObject.AddComponent<>();}
        if (componentName == "CanvasRenderer") { selectedObject.AddComponent<CanvasRenderer>();}
        if (componentName == "GUITexture") { selectedObject.AddComponent<GUITexture>();}
        if (componentName == "GUIText") { selectedObject.AddComponent<GUIText>();}
        //TileMap
        //if (componentName == "Tilemap") { selectedObject.AddComponent<Tilemap>();}
        //if (componentName == "TilemapRendering") { selectedObject.AddComponent<TilemapRendering>();}
        //if (componentName == "TilemapCollider2D") { selectedObject.AddComponent<TilemapCollider2D>();}
        //Layout
        if (componentName == "RectTransform") { selectedObject.AddComponent<RectTransform>();}
        if (componentName == "Canvas") { selectedObject.AddComponent<Canvas>();}
        if (componentName == "CanvasGroup") { selectedObject.AddComponent<CanvasGroup>();}
        if (componentName == "CanvasScaler") { selectedObject.AddComponent<CanvasScaler>();}
        if (componentName == "LayoutElement") { selectedObject.AddComponent<LayoutElement>();}
        if (componentName == "ContentSizeFitter") { selectedObject.AddComponent<ContentSizeFitter>();}
        if (componentName == "AspectRatioFitter") { selectedObject.AddComponent<AspectRatioFitter>();}
        if (componentName == "HorizontalLayoutGroup") { selectedObject.AddComponent<HorizontalLayoutGroup>();}
        if (componentName == "VerticalLayoutGroup") { selectedObject.AddComponent<VerticalLayoutGroup>();}
        if (componentName == "GridLayoutGroup") { selectedObject.AddComponent<GridLayoutGroup>();}
    }

    void SetFilter()
    {
        loadFilter = true;
        SetFilterComponent();
        SetFilterTag();
    }

    void SetFilterComponent()
    {
        //Mesh
        searchComponents[0] = "MeshFilter";
        searchComponents[1] = "TextMesh";
        searchComponents[2] = "MeshRenderer";
        searchComponents[3] = "SkinnedMeshRenderer";
        //Effect
        searchComponents[4] = "ParticleSystem";
        searchComponents[5] = "TrailRenderer";
        searchComponents[6] = "LineRenderer";
        searchComponents[7] = "LensFlare";
        searchComponents[8] = "Halo";
        searchComponents[9] = "Projector";
        searchComponents[10] = "LegacyParticles";
        //Physics
        searchComponents[11] = "Rigidbody";
        searchComponents[12] = "CharacterController";
        searchComponents[13] = "BoxCollider";
        searchComponents[14] = "SphereCollider";
        searchComponents[15] = "CapsuleCollider";
        searchComponents[16] = "MeshCollider";
        searchComponents[17] = "WheelCollider";
        searchComponents[18] = "TerrainCollider";
        searchComponents[19] = "Cloth";
        searchComponents[20] = "HingeJoint";
        searchComponents[21] = "FixedJoint";
        searchComponents[22] = "SpringJoint";
        searchComponents[23] = "CharacterJoint";
        searchComponents[24] = "ConfigurableJoint";
        searchComponents[25] = "ConstantForce";
        //Physics2D
        searchComponents[26] = "Rigidbody2D";
        searchComponents[27] = "BoxCollider2D";
        searchComponents[28] = "CircleCollider2D";
        searchComponents[29] = "EdgeCollider2D";
        searchComponents[30] = "PolygonCollider2D";
        searchComponents[31] = "CapsuleCollider2D";
        searchComponents[32] = "CompositeCollider2D";
        searchComponents[33] = "DistanceJoint2D";
        searchComponents[34] = "FixedJoint2D";
        searchComponents[35] = "FrictionJoint2D";
        searchComponents[36] = "HingeJoint2D";
        searchComponents[37] = "RelativeJoint2D";
        searchComponents[38] = "SliderJoint2D";
        searchComponents[39] = "SpringJoint2D";
        searchComponents[40] = "TargetJoint2D";
        searchComponents[41] = "WheelJoint2D";
        searchComponents[42] = "AreaEffector2D";
        searchComponents[43] = "BuoyancyEffector2D";
        searchComponents[44] = "PointEffector2D";
        searchComponents[45] = "PlatformEffector2D";
        searchComponents[46] = "SurfaceEffector2D";
        searchComponents[47] = "ConstantForce2D";
        //Navigation
        searchComponents[48] = "NavMeshAgent";
        searchComponents[49] = "OffMeshLink";
        searchComponents[50] = "NavMeshObstacle";
        //Audio
        searchComponents[51] = "AudioListener";
        searchComponents[52] = "AudioSource";
        searchComponents[53] = "AudioReverbZone";
        searchComponents[54] = "AudioLowPassFilter";
        searchComponents[55] = "AudioHighPassFilter";
        searchComponents[56] = "AudioEchoFilter";
        searchComponents[57] = "AudioDistortionFilter";
        searchComponents[58] = "AudioReverbFilter";
        searchComponents[59] = "AudioChorusFilter";
        searchComponents[60] = "AudioSpatializer";
        //Video
        searchComponents[61] = "VideoPlayer";
        //Rendering
        searchComponents[62] = "Camera";
        searchComponents[63] = "Skybox";
        searchComponents[64] = "FlareLayer";
        searchComponents[65] = "GUILayer";
        searchComponents[66] = "Light";
        searchComponents[67] = "LightProbeGroup";
        searchComponents[68] = "LightProbeProxyVolume";
        searchComponents[69] = "ReflectionProbe";
        searchComponents[70] = "OcclusionArea";
        searchComponents[71] = "OcclusionPortal";
        searchComponents[72] = "LODGroup";
        searchComponents[73] = "SpriteRenderer";
        searchComponents[74] = "SortingGroup";
        searchComponents[75] = "CanvasRenderer";
        searchComponents[76] = "GUITexture";
        searchComponents[77] = "GUIText";
        //Tilemap
        searchComponents[78] = "Tilemap";
        searchComponents[79] = "TilemapRendering";
        searchComponents[80] = "TilemapCollider2D";
        //Layout
        searchComponents[81] = "RectTransform";
        searchComponents[82] = "Canvas";
        searchComponents[83] = "CanvasGroup";
        searchComponents[84] = "CanvasScaler";
        searchComponents[85] = "LayoutElement";
        searchComponents[86] = "ContentSizeFitter";
        searchComponents[87] = "AspectRatioFitter";
        searchComponents[88] = "HorizontalLayoutGroup";
        searchComponents[89] = "VerticalLayoutGroup";
        searchComponents[90] = "GridLayoutGroup";
    }

    void SetFilterTag()
    {
        for (int i = 0; i < searchComponentsTag.Length; i++)
        {
            if(i >= 0 && i <= 3)
            {
                searchComponentsTag[i] = "Mesh";
            }
            if(i >= 4 && i <= 10)
            {
                searchComponentsTag[i] = "Effects";
            }
            if(i >= 11 && i <= 25)
            {
                searchComponentsTag[i] = "Physics";
            }
            if(i >= 26 && i <= 47)
            {
                searchComponentsTag[i] = "Physics2D";
            }
            if(i >= 48 && i <= 50)
            {
                searchComponentsTag[i] = "Navigation";
            }
            if(i >= 61 && i <= 61)
            {
                searchComponentsTag[i] = "Video";
            }
            if(i >= 62 && i <= 77)
            {
                searchComponentsTag[i] = "Rendering";
            }
            if(i >= 78 && i <= 80)
            {
                searchComponentsTag[i] = "Tilemap";
            }
            if(i >= 81 && i <= 90)
            {
                searchComponentsTag[i] = "Layout";
            }
        }
    }
}

//GUILayout.Toolbar

class Unity_Assistent_JsonSave
{
}

class Unity_SaveTerrain
{
    public int[] Terrain_ObjectID;
}
    