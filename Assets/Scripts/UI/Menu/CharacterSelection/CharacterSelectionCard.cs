using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _specNameField = null;
    [SerializeField] private TextMeshProUGUI _specDescField = null;
    [SerializeField] private Image _specImage = null;
    [SerializeField] private Button _selectButton = null;

    private PlayerCharacter _player;

    public void SetCharacter(PlayerCharacter player)
    {
        _player = player;

        _specNameField.SetText(player.PlayerSpec.SpecName);
        _specDescField.SetText(player.PlayerSpec.SpecDescription);
        _specImage.sprite = player.PlayerSpec.PlayerSpecIcon;

        _selectButton.onClick.AddListener(() => GameMaster.Instance.StartNewGame(_player));
    }
}
