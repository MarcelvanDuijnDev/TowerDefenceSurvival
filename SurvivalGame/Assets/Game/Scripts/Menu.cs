using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
    [SerializeField]private Button m_ButtonStart;
    [SerializeField]private Button m_ButtonExit;

	void Start () 
    {
		
	}
	
	void Update () 
    {
		
	}

    public void OnButtonClick_Start()
    {
        SceneManager.LoadScene(1);
    }
    public void OnButtonClick_Exit()
    {
        Application.Quit();
    }
}
