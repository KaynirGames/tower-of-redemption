using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTabUI : MenuTabUI
{
    [Header("Параметры вкладки с персонажем:")]
    [SerializeField] private Image _specIconDisplay = null;
    [SerializeField] private TextMeshProUGUI _specNameField = null;
    [SerializeField] private ResourceDisplayUI _resourceDisplay = null;
    [SerializeField] private StatDisplayUI _statDisplay = null;
    [SerializeField] private EfficacyDisplayUI _efficacyDisplay = null;
    [SerializeField] private SkillBookUI _skillBookDisplay = null;

    public override void RegisterPlayer(PlayerCharacter player)
    {
        _specIconDisplay.sprite = player.PlayerSpec.PlayerSpecIcon;
        _specIconDisplay.enabled = true;

        _specNameField.SetText(player.PlayerSpec.SpecName);
        _resourceDisplay.RegisterResources(player.Stats);
        _statDisplay.RegisterStats(player.Stats);
        _efficacyDisplay.RegisterElementEfficacies(player.Stats);
        _skillBookDisplay.RegisterSkillBook(player.SkillBook);
    }
}
