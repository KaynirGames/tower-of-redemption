using UnityEngine;

/// <summary>
/// Набор направлений дверей (вверх, вправо, вниз, влево, по умолчанию - отсутствует).
/// </summary>
public enum Direction { Up, Right, Down, Left }

public class Door : MonoBehaviour
{
    [SerializeField] private Direction _direction = Direction.Up;
    [SerializeField] private DoorType _doorType = null;
    [SerializeField] private float _doorwayTransferRange = 4f;
    [SerializeField] private Collider2D _doorCollider = null;
    [SerializeField] private Collider2D _playerTransferTrigger = null;
    [SerializeField] private Collider2D _closedDoorwayCollider = null;

    public Direction Direction => _direction;
    public DoorType DoorType => _doorType;

    private Animator _doorAnimator;

    public void CreateDoorGFX(DoorType doorType)
    {
        GameObject door = Instantiate(doorType.DoorGFX,
                                      transform.position,
                                      transform.rotation,
                                      transform);

        _doorAnimator = door.GetComponent<Animator>();

        ToggleDoorColliders(true);
    }

    public void ToggleDoor(bool open)
    {
        if (open)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Remove()
    {
        _closedDoorwayCollider.enabled = true;

        Destroy(gameObject);
    }

    public void SetDirection(Direction direction)
    {
        _direction = direction;
    }

    private void ToggleDoorColliders(bool enable)
    {
        _doorCollider.enabled = enable;
        _playerTransferTrigger.enabled = enable;

        _closedDoorwayCollider.enabled = !enable;
    }

    private void Open()
    {
        _doorAnimator.SetTrigger("Open");
        // Звук
        ToggleDoorColliders(true);
    }

    private void Close()
    {
        _doorAnimator.SetTrigger("Close");
        // Звук
        ToggleDoorColliders(false);
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
        if (other.GetComponent<PlayerCharacter>() != null)
        {
            TransferPlayer(other.transform, _direction);
        }
    }
}