using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class EditorScript_Template : EditorWindow
{

    [MenuItem("Tools/Template")]
    static void Init()
    {
        EditorScript_Template window = (EditorScript_Template)EditorWindow.GetWindow(typeof(EditorScript_Template));
        window.Show();
    }

    void OnGUI()
    {
        
    }
}


