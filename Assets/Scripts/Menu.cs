using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
    public void OnButtonClick_Start()
    {
        SceneManager.LoadScene(1);
    }
    public void OnButtonClick_Exit()
    {
        Application.Quit();
    }
}
