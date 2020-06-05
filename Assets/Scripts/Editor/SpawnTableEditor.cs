using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnTable))]
public class SpawnTableEditor : Editor
{
    private SpawnTable spawnTable;

    private void OnEnable()
    {
        spawnTable = (SpawnTable)target;
    }

    public override void OnInspectorGUI()
    {
        if (spawnTable.SpawnTableObjects.Count > 0)
        {
            foreach (SpawnTableObject tableObject in spawnTable.SpawnTableObjects)
            {
                EditorGUILayout.BeginVertical("box");

                if (GUILayout.Button("X", GUILayout.Height(20), GUILayout.Width(20)))
                {
                    spawnTable.SpawnTableObjects.Remove(tableObject);
                    break;
                }
                tableObject.ObjectToSpawn = EditorGUILayout.ObjectField("Объект:", tableObject.ObjectToSpawn, typeof(Object), true);
                tableObject.Weight = EditorGUILayout.IntField("Вероятность появления:", tableObject.Weight);

                EditorGUILayout.EndVertical();
            }
        }
        else
        {
            GUILayout.Label("Таблица вероятностей появления объектов пуста.");
        }

        if (GUILayout.Button("Добавить новый объект", GUILayout.Height(30)))
        {
            spawnTable.SpawnTableObjects.Add(new SpawnTableObject());
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(spawnTable);
        }
    }
}
