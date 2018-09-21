using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;

public class Tool_SceneSelector : EditorWindow
{
    Tool_SceneGet[] sceneGet = new Tool_SceneGet[EditorSceneManager.sceneCount];
    public string[] sceneName = new string[EditorSceneManager.sceneCount];

    void Update()
    {
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            sceneName[i] = EditorSceneManager.GetSceneByBuildIndex(i).name;
        }
    }
}

public class Tool_SceneGet : Tool_SceneSelector
{
    Tool_SceneSelector sceneSelector = new Tool_SceneSelector();
    string[] scenename = new string[0];

    void Update()
    {
        scenename = sceneSelector.sceneName;
    }

    [MenuItem("Scenes/" + "test")]
    static void LoadScene()
    {
        //EditorSceneManager.OpenScene();
    }
}


