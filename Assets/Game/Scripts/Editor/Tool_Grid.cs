using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class Tool_Grid : EditorWindow
{
    #region Variables
    private GameObject prefabObj;
    private GameObject obj;
    private List<GameObject> prefabObjSave = new List<GameObject>();

    private int mode;
    private int totalObjects = 125;

    //Cube
    private int xAsLenght = 5, yAsLenght = 5, zAsLenght = 5;
    //Circle
    private float radius = 5;
    private int objectAmount = 10;
    private float distant = 1.5f;
    //Hex
    private int hexLengthX = 10, hexLengthZ = 10;
    private float hexSize = 1;

    //Advanced All
    private int adv_Menu;
    private bool adv_Center = true;
    private bool adv_Invert;
    private Vector3 adv_Rotation;
    private Vector3 adv_RotationObj;
    #endregion

    [MenuItem("Tools/Grid")]
    static void Init()
    {
        Tool_Grid window = (Tool_Grid)EditorWindow.GetWindow(typeof(Tool_Grid));
        window.Show();
    }

    void OnGUI()
    {
        mode = GUILayout.Toolbar(mode, new string[] { "Cube", "Circle", "Hexagon" });

        GUILayout.BeginVertical("Box");
        prefabObj = (GameObject)EditorGUILayout.ObjectField("Prefab Object: ", prefabObj, typeof(GameObject), true);
        obj = (GameObject)EditorGUILayout.ObjectField("Center Object", obj, typeof(GameObject), true);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        if (mode == 0)
        {
            distant = EditorGUILayout.FloatField("Distance: ", distant);
            xAsLenght = EditorGUILayout.IntField("xAsLenght: ", xAsLenght);
            yAsLenght = EditorGUILayout.IntField("yAsLenght: ", yAsLenght);
            zAsLenght = EditorGUILayout.IntField("zAsLenght: ", zAsLenght);
        }
        if (mode == 1)
        {
            radius = EditorGUILayout.FloatField("Radius: ", radius);
            objectAmount = EditorGUILayout.IntField("Object Amount: ", objectAmount);
        }
        if (mode == 2)
        {
            hexSize = EditorGUILayout.FloatField("Size: ", hexSize);
            hexLengthX = EditorGUILayout.IntField("Collom: ", hexLengthX);
            hexLengthZ = EditorGUILayout.IntField("Row: ", hexLengthZ);
        }
        if (GUILayout.Button("Calculate Total Objects"))
        {
            if (mode == 0) { totalObjects = xAsLenght * zAsLenght * yAsLenght; }
            if (mode == 1) { totalObjects = objectAmount; }
            if (mode == 2) { totalObjects = hexLengthX * hexLengthZ; }
        }
        EditorGUILayout.LabelField("Total: " + totalObjects.ToString());
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        adv_Menu = GUILayout.Toolbar(adv_Menu, new string[] { "Options","Rotation" });
        if (mode == 0 && adv_Menu == 0 || mode == 2 && adv_Menu == 0)
        {
            adv_Center = EditorGUILayout.Toggle("Center", adv_Center);
        }
        if (mode == 2)
        {
            adv_Invert = EditorGUILayout.Toggle("Invert: ", adv_Invert);
        }
        if (adv_Menu == 1)
        {
            adv_Rotation = EditorGUILayout.Vector3Field("Rotation: ", adv_Rotation);
            adv_RotationObj = EditorGUILayout.Vector3Field("Rotation Obj: ", adv_RotationObj);
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        if (GUILayout.Button("Create"))
        {
            if(prefabObjSave.Count > 0)
            {
                for (int i = 0; i < prefabObjSave.Count; i++)
                {
                    DestroyImmediate(prefabObjSave[i]);
                }
                prefabObjSave.Clear();
            }

            Vector3 objPos = obj.transform.position;

            if (mode == 0) { CreateCube(new Vector3(xAsLenght, yAsLenght, zAsLenght)); }
            if (mode == 1) { CreateCircle(); }
            if (mode == 2) { CreateHexagon(new Vector3(hexLengthX,0,hexLengthZ)); }

            SetParent();
            SetRotation();
            SetRotationObj();
        }

        if (GUILayout.Button("Destroy"))
        {
            for (int i = 0; i < prefabObjSave.Count; i++)
            {
                DestroyImmediate(prefabObjSave[i]);
            }
            prefabObjSave.Clear();
        }

        if (GUILayout.Button("Confirm"))
        {
            prefabObjSave.Clear();
        }
        GUILayout.EndVertical();
    }

    void CreateCube(Vector3 dimentsions)
    {
        Vector3 objPos = obj.transform.position;
        if(adv_Center)
        {
            objPos.x -= dimentsions.x * 0.5f;
            objPos.y -= dimentsions.y * 0.5f;
            objPos.z -= dimentsions.z * 0.5f;
        }

        for (int xas = 0; xas < dimentsions.x; xas++)
        {
            for (int zas = 0; zas < dimentsions.z; zas++)
            {
                GameObject gridObjZas = Instantiate(prefabObj, new Vector3(objPos.x + distant * xas, objPos.y, objPos.z + distant * zas), Quaternion.identity);
                prefabObjSave.Add(gridObjZas);
                for (int yas = 1; yas < dimentsions.y; yas++)
                {
                    GameObject gridObjYas = Instantiate(prefabObj, new Vector3(objPos.x + distant * xas, objPos.y + distant * yas, objPos.z + distant * zas), Quaternion.identity);
                    prefabObjSave.Add(gridObjYas);
                }
            }
        }
    }

    void CreateHexagon(Vector3 dimentsions)
    {
        Vector3 objPos = obj.transform.position;
        if (adv_Center && !adv_Invert)
        {
            objPos.x -= dimentsions.x * 0.5f * 1.7321f * hexSize;
            objPos.z -= dimentsions.z * 0.5f * -1.5f * hexSize;
        }
        if (adv_Center && adv_Invert)
        {
            objPos.x -= dimentsions.x * 0.5f * 1.7321f * hexSize;
            objPos.z += dimentsions.z * 0.5f * -1.5f * hexSize;
        }

        for (int xas = 0; xas < dimentsions.x; xas++)
        {
            CreateHax(new Vector3(objPos.x + 1.7321f * hexSize * xas, objPos.y, objPos.z));
            for (int zas = 1; zas < dimentsions.z; zas++)
            {
                float offset = 0;
                if (zas %2 == 1)
                {
                    offset = 0.86605f * hexSize;
                }
                else
                {
                    offset = 0;
                }
                if (!adv_Invert)
                {
                    CreateHax(new Vector3(objPos.x + 1.7321f * hexSize * xas - offset, objPos.y, objPos.z + -1.5f * hexSize * zas));
                }
                else
                {
                    CreateHax(new Vector3(objPos.x + 1.7321f * hexSize * xas - offset, objPos.y, objPos.z + +1.5f * hexSize * zas));
                }
            }
        }
    }

    void CreateHax(Vector3 positions)
    {
        Vector3 objPos = obj.transform.position;

        GameObject gridObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gridObj.transform.position = new Vector3(positions.x, positions.y, positions.z);

        DestroyImmediate(gridObj.GetComponent<BoxCollider>());

        float size = hexSize;
        float width = Mathf.Sqrt(3) * size;
        float height = size * 2f;
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[7];

        for (int i = 0; i < 6; i++)
        {
            float angle_deg = 60 * i - 30;
            float angle_rad = Mathf.Deg2Rad * angle_deg;

            vertices[i + 1] = new Vector3(size * Mathf.Cos(angle_rad), 0f, size * Mathf.Sin(angle_rad));
        }
        mesh.vertices = vertices;

        mesh.triangles = new int[]
        {
            2,1,0,
            3,2,0,
            4,3,0,
            5,4,0,
            6,5,0,
            1,6,0
        };

        Vector2[] uv = new Vector2[7];
        for (int i = 0; i < 7; i++)
        {
            uv[i] = new Vector2(
                (vertices[i].x + -width * .5f) * .5f / size,
                (vertices[i].z + -height * .5f) * .5f / size);
        }

        mesh.uv = uv;
        gridObj.GetComponent<MeshFilter>().sharedMesh = mesh;

        prefabObjSave.Add(gridObj);
    }

    void CreateCircle()
    {
        Vector3 objPos = obj.transform.position;
        for (int i = 0; i < objectAmount; i++)
        {
            float ang = 36000 / objectAmount * i * 0.01f;
            Vector3 pos;
            pos.x = obj.transform.position.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = obj.transform.position.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.z = obj.transform.position.z;
            //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, objPos - pos);
            GameObject gridObjCircle = Instantiate(prefabObj, pos, Quaternion.identity); //rot);
            prefabObjSave.Add(gridObjCircle);
        }
    }

    void SetParent()
    {
        for (int i = 0; i < prefabObjSave.Count; i++)
        {
            prefabObjSave[i].transform.parent = obj.transform;
        }
    }

    void SetRotation()
    {
        obj.transform.eulerAngles = adv_Rotation;
    }

    void SetRotationObj()
    {
        for (int i = 0; i < prefabObjSave.Count; i++)
        {
            prefabObjSave[i].transform.eulerAngles = adv_RotationObj;
        }
    }
}