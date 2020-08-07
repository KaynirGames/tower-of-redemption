using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    [SerializeField] private DoorDatabase _doorDatabase = null;
    /// <summary>
    /// База данных дверей.
    /// </summary>
    public DoorDatabase Doors => _doorDatabase;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
