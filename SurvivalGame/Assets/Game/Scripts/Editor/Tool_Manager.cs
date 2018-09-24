using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tool_Manager : EditorWindow
{
    [MenuItem("Tools/Tool Manager/Tool Manager")]
    static void Init()
    {
        Tool_Manager window = (Tool_Manager)EditorWindow.GetWindow(typeof(Tool_Manager));
        window.Show();
    }
    [MenuItem("Tools/Tool Manager/Tools/Scripts")]
    static void Init_Scripts()
    {
        Tool_Scripts window = (Tool_Scripts)EditorWindow.GetWindow(typeof(Tool_Scripts));
        window.Show();
    }
    [MenuItem("Tools/Tool Manager/Tools/Grid")]
    static void Init_Grid()
    {
        Tool_Grid window = (Tool_Grid)EditorWindow.GetWindow(typeof(Tool_Grid));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical("Box");

        GUILayout.Label("Tools Manager");


        GUILayout.EndVertical();
    }
}
