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
    public Player Player { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }

        Player.OnPlayerActive += RegisterPlayer;
    }
    /// <summary>
    /// Зарегистрировать активного игрока.
    /// </summary>
    private void RegisterPlayer(Player player)
    {
        Player = player;
    }
}