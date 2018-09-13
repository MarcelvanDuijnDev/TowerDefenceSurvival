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
        "Tool_Movement",
        "Test2"
    };
    public string[] scriptCode = new string[]
    {
        "Testing",
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
        GUILayout.Label("Scripts", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal("Box");
		searchScript = EditorGUILayout.TextField("Search: ", searchScript);
        GUILayout.EndHorizontal();


        for (int i = 0; i < scriptName.Length; i++)
        {
			if (searchScript == "" || searchScript == null || scriptName[i].ToLower().Contains(searchScript.ToLower()))
			{
				GUILayout.BeginHorizontal("Box");
				GUILayout.Label(scriptName[i], EditorStyles.boldLabel);
				if (GUILayout.Button("Add"))
				{
					//Add script
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

				/*
				GUILayout.BeginScrollView(new Vector2(3,150), GUILayout.Width(position.width), GUILayout.Height(position.height - 100));
				GUILayout.BeginHorizontal("Box");
				//textField = EditorGUI.TextArea(new Vector2(0,0), textField);
				GUILayout.EndHorizontal();
				GUILayout.EndScrollView();
				*/
			}
        }



        GUILayout.EndVertical();
    }
}
