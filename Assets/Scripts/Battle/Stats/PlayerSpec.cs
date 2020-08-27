using UnityEngine;

/// <summary>
/// Специализация персонажа.
/// </summary>
[CreateAssetMenu(fileName = "NewPlayerSpec", menuName = "Scriptable Objects/Battle/Specs/Player Spec")]
public class PlayerSpec : SpecBase
{
    [Header("Информация о спеке игрока:")]
    [SerializeField] private Sprite _playerSpecIcon = null;

    public Sprite PlayerSpecIcon => _playerSpecIcon;
}