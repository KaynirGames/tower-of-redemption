using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int width = 20; // Ширина комнаты.
    [SerializeField] private int height = 12; // Высота комнаты.

    /// <summary>
    /// Локальная позиция комнаты в сетке координат подземелья.
    /// </summary>
    public Vector2Int LocalGridPosition { get; set; }

    /// <summary>
    /// Устанавливает глобальную позицию комнаты на сцене.
    /// </summary>
    public void SetWorldPosition()
    {
        transform.position = new Vector3(LocalGridPosition.x * width, LocalGridPosition.y * height, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}
