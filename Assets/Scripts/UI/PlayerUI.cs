using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private ResourceDisplayUI _resourceDisplay = null; // Отображение ресурсов игрока.

    private void Awake()
    {
        Player.OnPlayerActive += Init;
    }
    /// <summary>
    /// Инициализировать UI игрока. 
    /// </summary>
    private void Init(Player player)
    {
        _resourceDisplay.Init(player.PlayerStats);
    }
}
