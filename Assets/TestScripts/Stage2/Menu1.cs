using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu1 : MonoBehaviour
{
    public void LoadScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(SceneName).buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
