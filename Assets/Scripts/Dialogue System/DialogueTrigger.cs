using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue = null;
    [SerializeField] private bool _isRepeatable = false;

    private bool _isTriggered;

    public void TriggerDialogue()
    {
        if (_isRepeatable || !_isTriggered)
        {
            _isTriggered = true;

            DialogueManager.Instance.StartDialogue(_dialogue);
        }
    }
}
