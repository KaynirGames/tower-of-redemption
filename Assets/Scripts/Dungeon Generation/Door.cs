using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Набор направлений, куда может вести дверь (вверх, вправо, вниз, влево, по умолчанию - отсутствует).
/// </summary>
public enum Direction { Up, Right, Down, Left, None }

public class Door : MonoBehaviour
{
    [SerializeField] private Direction direction = Direction.None; // Направление, в которое ведет дверь.
    [SerializeField] private DoorType doorType = null; // Тип двери.
    [SerializeField] private Collider2D doorwayCollider = null; // Дверной проем, ограничивающий переход между комнатами.
    [SerializeField] private float doorwayTransferRange = 4f; // Расстояние переноса игрока при переходе между комнатами.

    /// <summary>
    /// Направление, в которое ведет дверь.
    /// </summary>
    public Direction Direction { get => direction; set => direction = value; }
    /// <summary>
    /// Тип двери.
    /// </summary>
    public DoorType DoorType => doorType;
    /// <summary>
    /// Открыть дверь.
    /// </summary>
    public void Open()
    {
        doorwayCollider.enabled = false;
        // Анимация
        // Звук
    }
    /// <summary>
    /// Закрыть дверь.
    /// </summary>
    public void Close()
    {
        doorwayCollider.enabled = true;
        // Анимация
        // Звук
    }
    /// <summary>
    /// Перенести игрока через соседние дверные проемы.
    /// </summary>
    /// <param name="player">Переносимый игрок.</param>
    /// <param name="direction">Направление переноса.</param>
    private void TransferPlayer(Transform player, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                player.Translate(Vector3.up * doorwayTransferRange);
                break;
            case Direction.Right:
                player.Translate(Vector3.right * doorwayTransferRange);
                break;
            case Direction.Down:
                player.Translate(Vector3.down * doorwayTransferRange);
                break;
            case Direction.Left:
                player.Translate(Vector3.left * doorwayTransferRange);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransferPlayer(other.transform, direction);
        }
    }
}