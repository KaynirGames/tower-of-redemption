using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomRuntimeSet))]
public class RoomRuntimeSetEditor : Editor
{
    private RoomRuntimeSet roomRuntimeSet;
    private bool displayRooms = true;

    public void OnEnable()
    {
        roomRuntimeSet = (RoomRuntimeSet)target;
    }

    public override void OnInspectorGUI()
    {
        if (roomRuntimeSet.GetAmount() == 0)
        {
            GUILayout.Label("Список комнат в наборе пуст.");
            return;
        }

        displayRooms = EditorGUILayout.Foldout(displayRooms, "Список комнат в наборе:");

        if (displayRooms)
        {
            DisplayRoomSetInfo();
        }
    }

    /// <summary>
    /// Отобразить информацию о комнатах в наборе.
    /// </summary>
    private void DisplayRoomSetInfo()
    {
        for (int i = 0; i < roomRuntimeSet.GetAmount(); i++)
        {
            Room room = roomRuntimeSet.GetByIndex(i);

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
            EditorGUILayout.LabelField($"[{room.GetWorldPosition().x},{room.GetWorldPosition().y}]");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
    }
}
