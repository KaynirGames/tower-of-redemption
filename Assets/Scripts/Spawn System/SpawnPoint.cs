using UnityEngine;

/// <summary>
/// Точка появления объекта.
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnTable _spawnTable = null; // Таблица вероятности появления объектов.

    /// <summary>
    /// Создать объект на месте точки.
    /// </summary>
    public void Spawn()
    {
        Spawn(null);
    }
    /// <summary>
    /// Создать объект на месте точки (с присвоением родительского объекта).
    /// </summary>
    public void Spawn(Transform parent)
    {
        GameObject gameObject = _spawnTable.ChooseRandom() as GameObject;
        Instantiate(gameObject, transform.position, Quaternion.identity, parent);
    }
}
