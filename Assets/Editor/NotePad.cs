using UnityEditor;
using Newtonsoft.Json;
using UnityEngine;

public class NotePad : EditorWindow
{
    [SerializeField] string textAreaText;
    Vector2 scroll;

    [MenuItem("Window/Tools/Notepad")]
    static void Init()
    {
        var notePadWindow = EditorWindow.CreateInstance<NotePad>();
        notePadWindow.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Notes");
        scroll = EditorGUILayout.BeginScrollView(scroll);
        EditorStyles.textArea.wordWrap = true;
        textAreaText = EditorGUILayout.TextArea(textAreaText, GUILayout.Height(position.height - 30));
        EditorGUILayout.EndScrollView();
    }
}
