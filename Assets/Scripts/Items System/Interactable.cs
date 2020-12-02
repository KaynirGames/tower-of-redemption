using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField] protected bool _interactOnce = false;
    [SerializeField] protected UnityEvent _onInteraction = null;

    public UnityEvent OnInteraction => _onInteraction;

    public virtual void Interact()
    {
        _onInteraction?.Invoke();

        if (_interactOnce)
        {
            Destroy(this);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerCharacter player))
        {
            Interact();
        }
    }
}
