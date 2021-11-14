using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class ScenesManager : EditorWindow
{
    private List<SceneData> m_sceneDatas = new List<SceneData>();
    private Vector2 m_scrollPosition;
    private bool m_toogleScene;  

    //create scenesManager window
    [MenuItem("Tools/Scenes Manager")]
    public static void ShowWindow()
    {
        GetWindow<ScenesManager>("Scenes Manager");
    }


    private void OnGUI()
    {
        //create content for scenesmanager window
        GUILayout.BeginVertical();
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Always check all your requiered scenes are in build");
                if (GUILayout.Button("Check build"))
                {
                    EditorWindow.GetWindow(Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
                }
            }GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Select scene to load:");
                if (GUILayout.Button("Refresh list"))
                {
                    RefreshRefreshContent();
                }
            }GUILayout.EndHorizontal();

            m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition, GUILayout.Width(100), GUILayout.Height(100));
            {
                for (int i = 0; i < m_sceneDatas.Count; i++)
                {
                    m_toogleScene = GUILayout.Toggle(m_sceneDatas[i].GetStatut(), m_sceneDatas[i].GetName());
                    
                    if(m_toogleScene != m_sceneDatas[i].GetStatut())
                    {
                        if(m_toogleScene)
                        {
                            EditorSceneManager.OpenScene(m_sceneDatas[i].GetPath(), OpenSceneMode.Additive);
                        }
                        else
                        {
                            if(EditorSceneManager.sceneCount == 1)
                            {
                                Debug.LogError("Impossible to have any scene actived");
                                m_toogleScene = GUILayout.Toggle(true, m_sceneDatas[i].GetName());
                            }
                            EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByName(m_sceneDatas[i].GetName()), true);
                        }
                        m_sceneDatas[i].SetStatut(m_toogleScene);
                    }
                }
            }GUILayout.EndScrollView();
        }
        GUILayout.EndVertical();
    }

    //get scen in the editor build setting
    private void RefreshRefreshContent()
    {
        EditorBuildSettingsScene[] listOfScene;

        m_sceneDatas.Clear();

        listOfScene = EditorBuildSettings.scenes;
        for (int i = 0; i < listOfScene.Length; i++)
            m_sceneDatas.Add(new SceneData(Path.GetFileNameWithoutExtension(listOfScene[i].path), listOfScene[i].path, false, false));

        for (int i = 0; i < m_sceneDatas.Count; i++)
        { 
            EditorSceneManager.OpenScene(m_sceneDatas[i].GetPath(), OpenSceneMode.Additive);
        }
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            m_sceneDatas[EditorSceneManager.GetSceneAt(i).buildIndex].SetStatut(true);
            m_sceneDatas[EditorSceneManager.GetSceneAt(i).buildIndex].SetStatutAtTheNextFrame(true);
        }
    }

//******************************************************************************************************

    //add Scene to the build
    [MenuItem("Assets/Scene/Add to build", false)]
    public static void AddToBuild()
    {
        EditorBuildSettingsScene[] listOfScene;
        listOfScene = EditorBuildSettings.scenes;
        SceneAsset selectedObject;

        if (AddToBuild(Selection.activeObject))
        {
            selectedObject = (SceneAsset)Selection.activeObject;
            if (listOfScene.Any(obj => Path.GetFileNameWithoutExtension(obj.path) == selectedObject.name))
                Debug.LogWarning(selectedObject.name + " already present in the list");
            else
            {
                Debug.Log(selectedObject.name + " added to the list");
                listOfScene.ToList().Add(new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(selectedObject), false));
                EditorBuildSettings.scenes = listOfScene.ToArray();
            }
        }
        else
            Debug.LogError("Wrong type");
    }

    //check if the asset is a scene or not 
    [MenuItem("Assets/Scene/Add to build", true)]
    public static bool AddToBuild(object obj)
    {
        if (obj is SceneAsset)
            return true;
        else
            return false;
    }
}
