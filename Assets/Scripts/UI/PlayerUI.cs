using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private ResourceDisplayUI _resourceDisplay = null;
    [SerializeField] private Joystick _joystick = null;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        PlayerCharacter.OnPlayerActive += RegisterPlayer;
    }

    public void TogglePlayerHUD(bool enable)
    {
        _canvasGroup.alpha = enable ? 1 : 0;
        _canvasGroup.blocksRaycasts = enable;
        _joystick.enabled = enable;
    }

    private void RegisterPlayer(PlayerCharacter player)
    {
        _resourceDisplay.RegisterResources(player.Stats);
    }
}
