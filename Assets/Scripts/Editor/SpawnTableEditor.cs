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
        if (_spawnTable.TableSize > 0)
        {
            SerializedObject serializedObject = new SerializedObject(target);
            serializedObject.Update();

            for (int i = 0; i < _spawnTable.TableSize; i++)
            {
                EditorGUILayout.BeginVertical("box");

                SerializedProperty serializedProperty = serializedObject
                    .FindProperty("_spawnableObjects")
                    .GetArrayElementAtIndex(i);

                if (GUILayout.Button("X", GUILayout.Height(20), GUILayout.Width(20)))
                {
                    _spawnTable.RemoveSpawnableObject(serializedProperty.FindPropertyRelative("_index").intValue);
                    break;
                }

                SerializedProperty spawnObject = serializedProperty.FindPropertyRelative("_objectToSpawn");
                SerializedProperty weight = serializedProperty.FindPropertyRelative("_weight");

                spawnObject.objectReferenceValue = EditorGUILayout.ObjectField("Объект:",
                                                                               spawnObject.objectReferenceValue,
                                                                               typeof(Object), true);
                weight.intValue = EditorGUILayout.IntField("Вероятность появления:", weight.intValue);

                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();
            serializedObject.Dispose();
        }
        else
        {
            GUILayout.Label("Таблица вероятностей появления объектов пуста.");
        }

        if (GUILayout.Button("Добавить новый объект", GUILayout.Height(30)))
        {
            _spawnTable.AddSpawnableObject(null, 0);
        }
    }
}
