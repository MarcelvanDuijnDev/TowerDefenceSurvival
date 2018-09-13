using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class Tool_Scripts : EditorWindow 
{
    private string searchScript;
    private string textField;
    #region Scripts
    public string[] scriptName = new string[]
    {
        "TestScript",
        "Test2"
    };
    public string[] scriptCode = new string[]
    {
        "Testing",
        "Test2"
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
        GUILayout.Label("Scripts", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.BeginVertical("Box");

        GUILayout.EndVertical();


        for (int i = 0; i < scriptName.Length; i++)
        {
            GUILayout.BeginHorizontal("Box");
            GUILayout.Label(scriptName[i], EditorStyles.boldLabel);
            if (GUILayout.Button("Add"))
            {
                //Add script
            }
            if (GUILayout.Button("Copy"))
            {
                EditorGUIUtility.systemCopyBuffer = scriptName[i];
            }
            if (GUILayout.Button("Show"))
            {
                textField = scriptCode[i];
            }


            GUILayout.EndHorizontal();
        }



        GUILayout.EndVertical();
    }
}