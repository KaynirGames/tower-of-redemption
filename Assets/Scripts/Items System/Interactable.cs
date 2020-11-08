using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent _onInteraction = null;

    public virtual void Interact(PlayerCharacter player)
    {
        _onInteraction?.Invoke();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCharacter player = collision.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            Interact(player);
        }
    }

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
