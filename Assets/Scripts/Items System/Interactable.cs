using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField] protected UnityEvent _onInteraction = null;

    public virtual void Interact()
    {
        _onInteraction?.Invoke();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
        {
            Interact();
        }
    }

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
