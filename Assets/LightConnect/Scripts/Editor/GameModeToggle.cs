using UnityEditor;

public static class GameModeToggle
{
    private const string MENU_NAME = "Tools/Constructor mode";
    private const string PREF_KEY = "ConstructorMode";

    [MenuItem(MENU_NAME)]
    private static void Toggle()
    {
        bool enabled = EditorPrefs.GetBool(PREF_KEY, false);
        enabled = !enabled;
        EditorPrefs.SetBool(PREF_KEY, enabled);
        Menu.SetChecked(MENU_NAME, enabled);
    }

    [MenuItem(MENU_NAME, true)]
    private static bool ToggleValidate()
    {
        Menu.SetChecked(MENU_NAME, EditorPrefs.GetBool(PREF_KEY, false));
        return true;
    }

    public static bool IsConstructorModeEnabled()
    {
        return EditorPrefs.GetBool(PREF_KEY, false);
    }
}