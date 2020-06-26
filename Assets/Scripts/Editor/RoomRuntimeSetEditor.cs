using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomRuntimeSet))]
public class RoomRuntimeSetEditor : Editor
{
    private RoomRuntimeSet _roomRuntimeSet; // Набор комнат.
    private bool _displayRooms; // Включить отображение комнат в наборе?

    public void OnEnable()
    {
        _roomRuntimeSet = (RoomRuntimeSet)target;
        _displayRooms = true;
    }

    public override void OnInspectorGUI()
    {
        if (_roomRuntimeSet.Count == 0)
        {
            GUILayout.Label("Список комнат в наборе пуст.");
            return;
        }

        _displayRooms = EditorGUILayout.Foldout(_displayRooms, "Список комнат в наборе:");

        if (_displayRooms)
        {
            DisplayRoomSetInfo();
        }
    }
    /// <summary>
    /// Отобразить информацию о комнатах в наборе.
    /// </summary>
    private void DisplayRoomSetInfo()
    {
        for (int i = 0; i < _roomRuntimeSet.Count; i++)
        {
            Room room = _roomRuntimeSet.GetObject(i);

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Тип комнаты:");
            EditorGUILayout.LabelField(room.RoomTypeData.Name);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Координаты в сетке подземелья:");
            EditorGUILayout.LabelField($"[{room.DungeonGridPosition.x},{room.DungeonGridPosition.y}]");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Координаты на сцене:");
            EditorGUILayout.LabelField($"[{room.transform.position.x},{room.transform.position.y}]");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
    }
}
