using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField] protected bool _interactOnce = false;
    [SerializeField] protected UnityEvent _onInteraction = null;

    public virtual void Interact()
    {
        _onInteraction?.Invoke();

        if (_interactOnce)
        {
            Destroy(this);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<PlayerCharacter>() != null)
        {
            Interact();
        }
    }
}
