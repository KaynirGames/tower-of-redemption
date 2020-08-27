using UnityEngine;

/// <summary>
/// Менеджер игрока.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    /// <summary>
    /// Активный игрок.
    /// </summary>
    public PlayerCharacter Player { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }

        PlayerCharacter.OnPlayerActive += RegisterPlayer;
    }
    /// <summary>
    /// Зарегистрировать активного игрока.
    /// </summary>
    private void RegisterPlayer(PlayerCharacter player)
    {
        Player = player;
    }
}