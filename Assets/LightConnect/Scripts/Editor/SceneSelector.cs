using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class SceneSelector
{
    private const string MAIN_SCENE_PATH = "Assets/LightConnect/Scenes/Main.unity";
    private const string CONSTRUCTOR_SCENE_PATH = "Assets/LightConnect/Scenes/Constructor.unity";

    static SceneSelector()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            if (!Application.isPlaying)
            {
                string scenePath = GameModeToggle.IsConstructorModeEnabled() ?
                    CONSTRUCTOR_SCENE_PATH :
                    MAIN_SCENE_PATH;

                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    return;

                EditorSceneManager.OpenScene(scenePath);
            }
        }
    }
}