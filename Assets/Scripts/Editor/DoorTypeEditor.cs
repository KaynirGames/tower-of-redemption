using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DoorType))]
public class DoorTypeEditor : Editor
{
    private DoorType doorType;

    private void OnEnable()
    {
        doorType = (DoorType)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Тип двери:");

        EditorGUILayout.BeginVertical("box");

        doorType.Name = EditorGUILayout.TextField("Наименование типа двери:", doorType.Name);
        doorType.PlacingPriority = EditorGUILayout.IntField("Приоритет размещения:", doorType.PlacingPriority);
        doorType.IsRequireKey = EditorGUILayout.Toggle("Требуется ключ?", doorType.IsRequireKey);

        if (doorType.IsRequireKey)
        {
            doorType.RequiredKey = (GameObject)EditorGUILayout.ObjectField("Требуемый ключ:", doorType.RequiredKey, typeof(GameObject), true);
        }

        EditorGUILayout.EndVertical();
    }
}
