using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingBake : MonoBehaviour
{
    [SerializeField] private Material m_NotWalkeble;
    [SerializeField] private GameObject m_PrefabObject;
    [SerializeField] private Vector3 m_GridSize;
    [SerializeField] private bool[ , , ] m_Walkeble;

    List<Vector3> newVertices = new List<Vector3>();
    Vector2[] newUV;
    int[] newTriangles;

    void Start ()
    {
        ScanMap();

        /*
        for (int i = 0; i < m_GridSize.x; i++)
        {
            for (int o = 0; o < m_GridSize.y; o++)
            {
                for (int p = 0; p < m_GridSize.z; p++)
                {
                    Instantiate(m_PrefabObject, new Vector3(i, o, p), Quaternion.identity);
                }
            }
        }
        */
    }

    void ScanMap()
    {
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        RaycastHit hit;

        for (int i = 0; i < m_GridSize.x; i++)
        {
            for (int o = 0; o < m_GridSize.z; o++)
            {
                if (Physics.Raycast(new Vector3(i,m_GridSize.y,o), transform.TransformDirection(Vector3.down), out hit, m_GridSize.y, layerMask) && hit.transform.gameObject.isStatic)
                {
                    //Instantiate(m_PrefabObject, hit.point, Quaternion.identity);
                    newVertices.Add(hit.point);
                }
            }
        }

        CreateMesh();
    }
	
	void Update ()
    {

    }

    void CreateMesh()
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(obj.GetComponent<BoxCollider>());

        Mesh mesh = obj.GetComponent<MeshFilter>().mesh;

        mesh.Clear();

        Vector3[] newVerticesCalc = new Vector3[newVertices.Count];
        newTriangles = new int[newVertices.Count];
        for (int i = 0; i < newVertices.Count; i++)
        {
            newVerticesCalc[i] = newVertices[i];
        }
        Vector2[] uv = new Vector2[7];
        for (int i = 0; i < 7; i++)
        {

            newUV[i] = new Vector2(
                (newVerticesCalc[i].x + 1),
                (newVerticesCalc[i].z + 1));

        }

        // Do some calculations...
        mesh.vertices = newVerticesCalc;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;

        obj.GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
