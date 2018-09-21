using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;

public class Tool_SceneSelector : EditorWindow
{
    private Tool_SceneTabs[] sceneTabs = new Tool_SceneTabs[SceneManager.sceneCount];

    [MenuItem("Scenes/")]
    static void Init()
    {
        //Tool_Scripts window = (Tool_Scripts)EditorWindow.GetWindow(typeof(Tool_Scripts));
        //window.Show();
    }

    void OnGUI()
    {
    }


    [MenuItem("Scenes/scene1test")]
    void loadScene()
    {
        
    }
}

public class Tool_SceneTabs : Tool_SceneSelector
{
    //string name = "Scenes/" + SceneManager.GetSceneByBuildIndex(0).name.ToString()
    //[MenuItem(name.ToString())]

}

