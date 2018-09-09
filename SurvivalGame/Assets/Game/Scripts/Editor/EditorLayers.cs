using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class EditorLayers : EditorWindow 
{
    public Layer[] layers = new Layer[0];
    private Layer[] backupLayers;
    private GameObject[] selectedGameObjects;

	void Update () 
    {
        selectedGameObjects = Selection.gameObjects;
	}

    public void LayerVisable(int index)
    {
        layers[index].active = !layers[index].active;
        for (int i = 0; i <  layers[index].objects.Length; i++)
        {
            if (layers[index].active)
            {
                if (layers[index].objectActive[i])
                {
                    layers[index].objects[i].SetActive(false);
                }
            }
            else
            {
                if (layers[index].objectActive[i])
                {
                    layers[index].objects[i].SetActive(true);
                }
            }
        }
    }

    public void AddObjects(int index)
    {

        layers[index].objects = new GameObject[selectedGameObjects.Length];
        layers[index].objectActive = new bool[selectedGameObjects.Length];

        for (int i = 0; i < selectedGameObjects.Length; i++)
        {
            layers[index].objects[i] = selectedGameObjects[i];
            layers[index].objectActive[i] = selectedGameObjects[i].activeSelf;
        }
    }

    public void SelectObjects(int index)
    {
        Selection.objects = layers[index].objects;
    }

    public void RemoveLayer(int index)
    {
        backupLayers = layers;
        layers = new Layer[backupLayers.Length - 1];
        int o = 0;
        for (int i = 0; i < backupLayers.Length - 1; i++)
        {
            o++;
            if (i != index)
            {
                layers[i] = backupLayers[o];
            }
            else
            {
                o++;
            }
        }
    }

    [MenuItem("Window/Layers")]
    static void Init()
    {
        EditorLayers window = (EditorLayers)EditorWindow.GetWindow(typeof(EditorLayers));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Layers", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");

        if (GUILayout.Button("Add Layer"))
        {
            backupLayers = layers;
            layers = new Layer[backupLayers.Length + 1];
            for (int i = 0; i < layers.Length; i++)
            {
                if (i != layers.Length - 1)
                {
                    layers[i] = backupLayers[i];
                }
                else
                {
                    AddObjects(i);
                    layers[i].layerName = "New Layer";
                }
            }
        }
        if (GUILayout.Button("Remove Layer"))
        {
            backupLayers = layers;
            layers = new Layer[layers.Length - 1];
            for (int i = 0; i < backupLayers.Length - 1; i++)
            {
                layers[i] = backupLayers[i];
            }
        }
        GUILayout.EndVertical();
        for (int i = 0; i < layers.Length; i++)
        {
            GUILayout.BeginHorizontal("Box");
            layers[i].layerName = EditorGUILayout.TextField("",layers[i].layerName);

            if (layers[i].active)
            {
                GUI.contentColor = Color.grey;
            }
            if (GUILayout.Button("Visable"))
            {
                LayerVisable(i);
            }
            GUI.contentColor = Color.white;


            if (GUILayout.Button("Select"))
            {
                SelectObjects(i);
            }
            if (GUILayout.Button("Change"))
            {
                AddObjects(i);
            }
            if (GUILayout.Button("Delete"))
            {
                RemoveLayer(i);
            }
            GUILayout.Label("Obj: " + layers[i].objects.Length.ToString(), EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
        }
    }
}
    
public struct Layer
{
    public string layerName;
    public bool active;
    public GameObject[] objects;
    public bool[] objectActive;
}
