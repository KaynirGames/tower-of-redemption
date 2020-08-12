using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnTable))]
public class SpawnTableEditor : Editor
{
    private SpawnTable _spawnTable;

    private void OnEnable()
    {
        _spawnTable = (SpawnTable)target;
    }

    public override void OnInspectorGUI()
    {
        if (_spawnTable.SpawnableObjects.Length > 0)
        {
            for (int i = 0; i < _spawnTable.SpawnableObjects.Length; i++)
            {
                EditorGUILayout.BeginVertical("box");

                if (GUILayout.Button("X", GUILayout.Height(20), GUILayout.Width(20)))
                {
                    _spawnTable.Remove(_spawnTable.SpawnableObjects[i]);
                    break;
                }

                SerializedObject serializedObject = new SerializedObject(target);
                serializedObject.Update();

                SerializedProperty serializedProperty = serializedObject
                    .FindProperty("_spawnableObjects")
                    .GetArrayElementAtIndex(i);

                SerializedProperty spawnObject = serializedProperty.FindPropertyRelative("_object");
                SerializedProperty weight = serializedProperty.FindPropertyRelative("_weight");

                spawnObject.objectReferenceValue = EditorGUILayout.ObjectField("Объект:",
                                                                               spawnObject.objectReferenceValue,
                                                                               typeof(Object), true);
                weight.intValue = EditorGUILayout.IntField("Вероятность появления:", weight.intValue);

                serializedObject.ApplyModifiedProperties();

                EditorGUILayout.EndVertical();
            }
        }
        else
        {
            GUILayout.Label("Таблица вероятностей появления объектов пуста.");
        }

        if (GUILayout.Button("Добавить новый объект", GUILayout.Height(30)))
        {
            _spawnTable.Add(null, 0);
        }
    }
}
