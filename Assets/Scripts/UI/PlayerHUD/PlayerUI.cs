using KaynirGames.InputSystem;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }

    [SerializeField] private ResourceDisplayUI _resourceDisplay = null;
    [SerializeField] private Joystick _joystick = null;
    [SerializeField] private ScreenJoybutton _attackButton = null;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        Instance = this;

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
        ScreenJoystickHandler joystickHandler = player.GetComponent<ScreenJoystickHandler>();

        if (joystickHandler != null)
        {
            joystickHandler.SetJoystick(_joystick, _attackButton);
        }
    }
}
