using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class DialogueLine
{
    [SerializeField] private LocalizedString _characterName = null;
    [SerializeField] private LocalizedString _characterLine = null;

    public string CharacterName => _characterName.GetLocalizedString().Result;
    public string CharacterLine => _characterLine.GetLocalizedString().Result;
}
