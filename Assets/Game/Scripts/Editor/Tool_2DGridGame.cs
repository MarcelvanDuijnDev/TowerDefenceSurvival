using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class Tool_2DGridGame : EditorWindow
{
    private Vector2 m_Size;
    private GameObject m_ParentObj;
    private Vector4[] m_GridObjects;


    [MenuItem("Tools/2DGridGame")]
    static void Init()
    {
        Tool_2DGridGame window = (Tool_2DGridGame)EditorWindow.GetWindow(typeof(Tool_2DGridGame));
        window.Show();
    }

    void OnGUI()
    {
        
    }
}

