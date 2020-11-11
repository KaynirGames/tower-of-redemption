using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue = null;
    [SerializeField] private bool _triggerOnce = false;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(_dialogue);

        if (_triggerOnce)
        {
            Destroy(this);
        }
    }
}
