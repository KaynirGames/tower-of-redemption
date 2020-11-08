using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private DialogueLine[] _dialogueLines = null;

    public DialogueLine[] DialogueLines => _dialogueLines;
}
