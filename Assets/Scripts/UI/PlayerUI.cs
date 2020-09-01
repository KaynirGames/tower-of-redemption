using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private ResourceDisplayUI _resourceDisplay = null; // Отображение ресурсов игрока.

    private void Awake()
    {
        PlayerCharacter.OnPlayerActive += Init;
    }
    /// <summary>
    /// Инициализировать UI игрока. 
    /// </summary>
    private void Init(PlayerCharacter player)
    {
        _resourceDisplay.RegisterResources(player.Stats);
    }
}
