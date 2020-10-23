using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class Tool_WaveGenerator : EditorWindow 
{
    public Tool_WaveOptions[] waves = new Tool_WaveOptions[0];

    [MenuItem("Tools/WaveGenerator")]
    static void Init()
    {
        Tool_WaveGenerator window = (Tool_WaveGenerator)EditorWindow.GetWindow(typeof(Tool_WaveGenerator));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Wave", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");

        if (GUILayout.Button("A"))
        {
        }
    }
}
    
public struct Tool_WaveOptions
{

}
