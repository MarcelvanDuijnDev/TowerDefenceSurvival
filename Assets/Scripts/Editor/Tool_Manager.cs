using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Tool_Manager : EditorWindow
{
    private JsonSaveTools toolsSave = new JsonSaveTools();

    [MenuItem("Tools/Tool Manager")]
    static void Init()
    {
        Tool_Manager window = (Tool_Manager)EditorWindow.GetWindow(typeof(Tool_Manager));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical("Box");

        GUILayout.Label("Tools Manager");

        if (GUILayout.Button("Save"))
        {
            Save();
        }
        if (GUILayout.Button("Load"))
        {
            Load();
        }

        GUILayout.EndVertical();
    }

    private void Save()
    {
        toolsSave.saveInfo = "Last Updated: " + System.DateTime.Now.ToString();
        string json = JsonUtility.ToJson(toolsSave);
        File.WriteAllText(Application.persistentDataPath + "/SaveTool.json", json.ToString());
    }
    private void Load()
    {
        string dataPath = Application.persistentDataPath + "/SaveTool.json";
        string dataAsJson = File.ReadAllText(dataPath);
        toolsSave = JsonUtility.FromJson<JsonSaveTools>(dataAsJson);
    }
}


public class JsonSaveTools
{
    public string saveInfo = "";

}
