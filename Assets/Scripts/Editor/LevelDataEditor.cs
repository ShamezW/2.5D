using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    private ReorderableList list;


    void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("myLevel"), true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Level Layout");
        };

        list.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("blockType"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + 80, rect.y, rect.width - 80, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Location"), GUIContent.none);
        };
    }

    public override void OnInspectorGUI()
    {
        LevelData data = target as LevelData;
        data.PlayerBlockLoc = EditorGUILayout.Vector3Field("Player Block", data.PlayerBlockLoc);

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("test"))
        {
            data.CreateLevel();
        }
    }
}
