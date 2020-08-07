using UnityEngine;

/// <summary>
/// Набор направлений, куда может вести дверь (вверх, вправо, вниз, влево, по умолчанию - отсутствует).
/// </summary>
public enum Direction { Up, Right, Down, Left, None }

public class Door : MonoBehaviour
{
    [SerializeField] private Direction _direction = Direction.None; // Направление, в которое ведет дверь.
    [SerializeField] private DoorType _doorType = null; // Тип двери.
    [SerializeField] private float _doorwayTransferRange = 4f; // Расстояние переноса игрока при переходе между комнатами.
    [SerializeField] private GameObject _emptyDoorPrefab = null; // Объект, заменяющий убранную дверь.
    /// <summary>
    /// Направление, куда ведет дверь.
    /// </summary>
    public Direction Direction => _direction;
    /// <summary>
    /// Тип двери.
    /// </summary>
    public DoorType DoorType => _doorType;
    /// <summary>
    /// Дверной проем, ограничивающий переход между комнатами.
    /// </summary>
    private Collider2D _doorwayCollider;

    private void Awake()
    {
        _doorwayCollider = GetComponent<Collider2D>();
    }
    /// <summary>
    /// Открыть дверь.
    /// </summary>
    public void Open()
    {
        _doorwayCollider.enabled = false;
        // Анимация
        // Звук
    }
    /// <summary>
    /// Закрыть дверь.
    /// </summary>
    public void Close()
    {
        _doorwayCollider.enabled = true;
        // Анимация
        // Звук
    }
    /// <summary>
    /// Убрать дверь.
    /// </summary>
    public void Remove()
    {
        // Вместо двери устанавливаем пустой проход с коллайдером.
        GameObject emptyDoor = Instantiate(_emptyDoorPrefab, transform.position, transform.rotation, transform.parent);
        emptyDoor.transform.localScale = transform.localScale;
        emptyDoor.GetComponent<Collider2D>().offset = _doorwayCollider.offset;

        Destroy(gameObject);
    }
    /// <summary>
    /// Заменить текущую дверь на другой тип.
    /// </summary>
    public Door Swap(DoorType otherType)
    {
        Door newDoor = DatabaseManager.Instance.Doors.Find(door => door.DoorType == otherType);
        newDoor._direction = _direction;
        newDoor = Instantiate(newDoor, transform.position, transform.rotation, transform.parent);
        return newDoor;
    }
    /// <summary>
    /// Перенести игрока в соседнюю комнату.
    /// </summary>
    private void TransferPlayer(Transform player, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                player.Translate(Vector3.up * _doorwayTransferRange);
                break;
            case Direction.Right:
                player.Translate(Vector3.right * _doorwayTransferRange);
                break;
            case Direction.Down:
                player.Translate(Vector3.down * _doorwayTransferRange);
                break;
            case Direction.Left:
                player.Translate(Vector3.left * _doorwayTransferRange);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransferPlayer(other.transform, _direction);
        }
    }
}