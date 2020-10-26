using UnityEngine;

/// <summary>
/// Набор направлений, куда может вести дверь (вверх, вправо, вниз, влево, по умолчанию - отсутствует).
/// </summary>
public enum Direction { Up, Right, Down, Left, None }

public class Door : MonoBehaviour
{
    [SerializeField] private Direction _direction = Direction.None;
    [SerializeField] private DoorType _doorType = null;
    [SerializeField] private float _doorwayTransferRange = 4f;
    [SerializeField] private GameObject _emptyDoorPrefab = null;

    public Direction Direction => _direction;
    public DoorType DoorType => _doorType;

    private Collider2D _doorwayCollider;

    private void Awake()
    {
        _doorwayCollider = GetComponent<Collider2D>();
    }

    public void Open()
    {
        _doorwayCollider.enabled = false;
        // Анимация
        // Звук
    }

    public void Close()
    {
        _doorwayCollider.enabled = true;
        // Анимация
        // Звук
    }

    public void Remove()
    {
        GameObject emptyDoor = Instantiate(_emptyDoorPrefab, transform.position, transform.rotation, transform.parent);
        emptyDoor.transform.localScale = transform.localScale;
        emptyDoor.GetComponent<Collider2D>().offset = _doorwayCollider.offset;

        Destroy(gameObject);
    }

    public void SetDirection(Direction direction)
    {
        _direction = direction;
    }

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