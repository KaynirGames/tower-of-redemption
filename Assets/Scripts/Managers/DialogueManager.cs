using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject _dialogueCanvas = null;
    [SerializeField] private TextMeshProUGUI _characterNameField = null;
    [SerializeField] private TextMeshProUGUI _characterLineField = null;

    private Queue<DialogueLine> _linesQueue;

    private CanvasGroup _canvasGroup;
    private Coroutine _lastTypeRoutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _canvasGroup = _dialogueCanvas.GetComponent<CanvasGroup>();
        _linesQueue = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        GameMaster.Instance.TogglePause(true);

        foreach (DialogueLine line in dialogue.DialogueLines)
        {
            _linesQueue.Enqueue(line);
        }

        ToggleDialogueCanvas(true);

        SetNextDialogueLine();
    }

    public void SetNextDialogueLine()
    {
        if (_linesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = _linesQueue.Dequeue();

        _characterNameField.SetText(line.CharacterName);

        if (_lastTypeRoutine != null)
        {
            StopCoroutine(_lastTypeRoutine);
        }

        _lastTypeRoutine = StartCoroutine(TypeLineRoutine(line.CharacterLine.ToCharArray()));
    }

    private void ToggleDialogueCanvas(bool enable)
    {
        _canvasGroup.alpha = enable ? 1 : 0;
        _canvasGroup.blocksRaycasts = enable;
        _canvasGroup.interactable = enable;
    }

    private void EndDialogue()
    {
        GameMaster.Instance.TogglePause(false);

        ToggleDialogueCanvas(false);

        if (_lastTypeRoutine != null)
        {
            StopCoroutine(_lastTypeRoutine);
        }

        _characterLineField.text = "";
        _linesQueue.Clear();
    }

    private IEnumerator TypeLineRoutine(char[] line)
    {
        _characterLineField.text = "";

        foreach (char character in line)
        {
            _characterLineField.text += character;
            yield return null;
        }
    }
}
