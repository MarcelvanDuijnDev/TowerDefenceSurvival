using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.UI;

public class Tool_Canvas : EditorWindow
{
    private string canvasName = "New Canvas";
    private GameObject canvas;
    private Button[] buttons;
    private int canvasOption1;
    private bool addMenu, addPauzeMenu;
    private int canvasOption3 = 1;
    private int canvasOption4 = 1;
    private Vector2 resolution = new Vector2(1920,1080);

    [MenuItem("Tools/Canvas")]
    static void Init()
    {
        Tool_Canvas window = (Tool_Canvas)EditorWindow.GetWindow(typeof(Tool_Canvas));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Options");
        canvasName = EditorGUILayout.TextField("Canvas Name: ",canvasName);
        canvasOption1 = GUILayout.Toolbar(canvasOption1, new string[] { "New Canvas" });

        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        Color defaultColor = GUI.backgroundColor;
        if (addMenu) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = new Color(1, 0, 0); }
        addMenu = GUILayout.Toggle(addMenu, "Menu", EditorStyles.toolbarButton);
        if (addPauzeMenu) { GUI.backgroundColor = new Color(0, 1, 0); } else { GUI.backgroundColor = new Color(1, 0, 0); }
        addPauzeMenu = GUILayout.Toggle(addPauzeMenu, "Pauze Menu", EditorStyles.toolbarButton);
        GUI.backgroundColor = defaultColor;
        GUILayout.EndHorizontal();

        canvasOption3 = GUILayout.Toolbar(canvasOption3, new string[] { "World Space", "Screen Space (OverLay)", "Sceen Space (Camera)" });
        if (canvasOption3 != 0)
        {
            canvasOption4 = GUILayout.Toolbar(canvasOption4, new string[] { "Constant Pixal", "Scale With Screen", "Constant Physical" });
            if (canvasOption4 == 1)
            {
                resolution = EditorGUILayout.Vector2Field("Resolution: ", resolution);
            }
        }

        if (GUILayout.Button("Create Canvas"))
        {
            CreateCanvas();
        }
        GUILayout.EndVertical();
        
    }


    void CreateCanvas()
    {
        if(canvasOption1 == 0)
        {
            GameObject newCanvas = new GameObject();
            newCanvas.name = canvasName;
            Canvas c = newCanvas.AddComponent<Canvas>();
            CanvasScaler cs = newCanvas.AddComponent<CanvasScaler>();
            newCanvas.AddComponent<GraphicRaycaster>();

            if (addMenu)
            {
                GameObject newObj = new GameObject();
                Image newImage = newObj.AddComponent<Image>();
                newObj.GetComponent<RectTransform>().SetParent(newCanvas.transform);
                newImage.rectTransform.sizeDelta = new Vector2(1920,1080);
                newObj.name = "Menu BackGround";

                GameObject newObj1 = new GameObject();
                Image newImage1 = newObj1.AddComponent<Image>();
                Button newButton1 = newObj1.AddComponent<Button>();
                newObj1.transform.SetParent(newObj.transform);

            }
            if (addPauzeMenu)
            {
                GameObject newObj = new GameObject();
                Image newImage = newObj.AddComponent<Image>();
                newObj.GetComponent<RectTransform>().SetParent(newCanvas.transform);
                newImage.rectTransform.sizeDelta = new Vector2(1000, 800);
                newImage.color = new Vector4(0.2f, 0.2f, 0.2f, 0.5f);
                newObj.name = "Pauze Menu BackGround";
            }
            if (canvasOption3 == 1)
            {
                c.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            if (canvasOption3 == 2)
            {
                c.renderMode = RenderMode.ScreenSpaceCamera;
            }
            if (canvasOption4 == 1)
            {
                cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                cs.referenceResolution = resolution;
                cs.matchWidthOrHeight = 0.5f;
            }
           
        }
    }
}


