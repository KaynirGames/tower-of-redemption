using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [SerializeField] private TranslatedText _characterName = new TranslatedText("Dialogue.Name.ID");
    [SerializeField] private TranslatedText _characterLine = new TranslatedText("Dialogue.Line.ID");

    public string CharacterName => _characterName.Value;
    public string CharacterLine => _characterLine.Value;
}
